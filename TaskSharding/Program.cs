using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BenchmarkIt;

namespace TaskSharding
{
    class Program
    {
        static void Main(string[] args)
        {
            int taskCount = 16;
            int iterations = 1000000;

            ICounter unsynchronized = new UnsynchronizedCounter();
            ICounter locking = new LockingCounter();
            ICounter interlocked= new InterlockedCounter();
            ICounter sharded = new ShardedCounter();

            Benchmark.This("UnsynchronizedCounter", () => RunBenchmark(1, taskCount * iterations, unsynchronized))
                .Against.This("LockingCounter", () => RunBenchmark(taskCount, iterations, locking))
                .Against.This("InterlockedCounter", () => RunBenchmark(taskCount, iterations, interlocked))
                .Against.This("ShardedCounter", () => RunBenchmark(taskCount, iterations, sharded))
                .WithWarmup(10)
                .For(5).Iterations()
                //.For(3).Seconds()
                .PrintComparison();

            Console.ReadKey();
        }

        private static bool RunBenchmark(int taskCount, int iterations, ICounter counter)
        {
            //Parallel.Invoke(new ParallelOptions{MaxDegreeOfParallelism = taskCount }, () => counter.Increase(1));

            var startingCount = counter.Count;

            Task[] tasks = new Task[taskCount];
            for (int i = 0; i < tasks.Length; ++i)
            {
                tasks[i] = Task.Run(() =>
                {
                    for (int j = 0; j < iterations; ++j)
                    {
                        counter.Increase(1);
                    }
                });
            }

            Task.WaitAll(tasks);

            return (startingCount + (taskCount * iterations)) == counter.Count;
        }
    }


    public interface ICounter
    {
        void Increase(long amount);
        long Count { get; }
    }

    public class UnsynchronizedCounter : ICounter
    {
        private long _count = 0;
        public long Count => _count;

        public void Increase(long amount)
        {
            _count += amount;
        }
    }
    public class LockingCounter : ICounter
    {
        private long _count = 0;
        private readonly object _thisLock = new object();

        public long Count
        {
            get
            {
                lock (_thisLock)
                {
                    return _count;
                }
            }
        }

        public void Increase(long amount)
        {
            lock (_thisLock)
            {
                _count += amount;
            }
        }
    }

    public class InterlockedCounter : ICounter
    {
        private long _count = 0;

        public long Count => Interlocked.CompareExchange(ref _count, 0, 0);

        public void Increase(long amount)
        {
            Interlocked.Add(ref _count, amount);
        }
    }

    public class ShardedCounter : ICounter
    {
        // Protects _deadShardSum and _shards.
        private readonly object _thisLock = new object();

        // The total sum from the shards from the threads which have terminated.
        private long _deadShardSum = 0;

        // The list of shards.
        private List<Shard> _shards = new List<Shard>();

        // The thread-local slot where shards are stored.
        private readonly LocalDataStoreSlot _slot = Thread.AllocateDataSlot();

        public long Count
        {
            get
            {
                // Sum over all the shards, and clean up dead shards at the
                // same time.
                long sum = _deadShardSum;
                List<Shard> livingShards_ = new List<Shard>();
                lock (_thisLock)
                {
                    foreach (Shard shard in _shards)
                    {
                        sum += shard.Count;
                        if (shard.Owner.IsAlive)
                        {
                            livingShards_.Add(shard);
                        }
                        else
                        {
                            _deadShardSum += shard.Count;
                        }
                    }
                    _shards = livingShards_;
                }
                return sum;
            }
        }

        public void Increase(long amount)
        {
            // Increase counter for this thread.
            Shard counter = Thread.GetData(_slot) as Shard;
            if (null == counter)
            {
                counter = new Shard()
                {
                    Owner = Thread.CurrentThread
                };
                Thread.SetData(_slot, counter);
                lock (_thisLock) _shards.Add(counter);
            }
            counter.Increase(amount);
        }

        private class Shard : UnsynchronizedCounter
        {
            public Thread Owner { get; set; }
        }
    }
}