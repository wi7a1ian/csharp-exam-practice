using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;



namespace IAsyncResultMixWithTAP
{
    /// <summary>
    /// http://msdn.microsoft.com/en-us/library/hh873178(v=vs.110).aspx
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            //Task<int> ts = Stream.ReadTask()
        }
    }

    internal static class ExtensionMethods {
        /// <summary>
        /// Interoop Option #1
        /// </summary>
        public static Task<int> ReadAsync(this Stream stream, byte [] buffer, int offset, int count)
        {
            if (stream == null) throw new ArgumentNullException("stream");
            return Task<int>.Factory.FromAsync(stream.BeginRead, stream.EndRead, buffer, offset, count, null);
        }

        /// <summary>
        /// Interoop Option #2
        /// </summary>
        public static Task<int> ReadAsync2(this Stream stream, byte[] buffer, int offset, int count)
        {
            if (stream == null) throw new ArgumentNullException("stream");
            var tcs = new TaskCompletionSource<int>();
            stream.BeginRead(buffer, offset, count, iar =>
            {
                try { tcs.TrySetResult(stream.EndRead(iar)); }
                catch (OperationCanceledException) { tcs.TrySetCanceled(); }
                catch (Exception exc) { tcs.TrySetException(exc); }
            }, null);
            return tcs.Task;
        }

        /// <summary>
        /// And vice versa:
        /// TAP -> APM (asynchronous programming model)
        /// </summary>
        /// <example>
        /// public IAsyncResult BeginDownloadString(
        ///    Uri url, AsyncCallback callback, object state)
        /// {
        ///     return DownloadStringAsync(url).AsApm(callback, state);
        /// }
        /// 
        /// public string EndDownloadString(IAsyncResult asyncResult)
        /// {
        ///     return ((Task<string>)asyncResult).Result;
        /// }
        /// </example>
        public static IAsyncResult AsApm<T>(this Task<T> task, AsyncCallback callback, object state)
        {
            if (task == null) throw new ArgumentNullException("task");
            var tcs = new TaskCompletionSource<T>(state);
            task.ContinueWith(t =>
            {
                if (t.IsFaulted) tcs.TrySetException(t.Exception.InnerExceptions);
                else if (t.IsCanceled) tcs.TrySetCanceled();
                else tcs.TrySetResult(t.Result);

                if (callback != null) callback(tcs.Task);
            }, TaskScheduler.Default);
            return tcs.Task;
        }

    }
}
