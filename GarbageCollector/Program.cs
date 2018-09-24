using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GarbageCollector
{
    class Program
    {
        static void Main(string[] args)
        {

        }
    }
    class SomeGCTest
    {
        void Test()
        {
            string everyString = "EndsInInternPool!";

            // Requests that the common language runtime not call the finalizer for the specified object.
            GC.SuppressFinalize(this);
            //Requests that the system call the finalizer for the specified object for which SuppressFinalize has previously been called.
            GC.ReRegisterForFinalize(this); // Place a reference to this object back in the finalization queue.
            // References the specified object, which makes it ineligible for garbage collection from the start of the current routine to the point where this method is called.
            GC.KeepAlive(this);

            // Force a garbage collection.
            GC.Collect();
            // Suspends the current thread until the thread that is processing the queue of finalizers has emptied that queue.
            GC.WaitForPendingFinalizers();

            // Returns, in a specified time-out period, the status of a registered notification for determining whether a full, blocking garbage collection by the common language runtime is imminent.
            GC.WaitForFullGCApproach(1000);

            // Informs the runtime of a large allocation of unmanaged memory that should be taken into account when scheduling garbage collection.
            GC.AddMemoryPressure(64 * 1024 * 1024); // We will allocate 64MB of unmanaged memo on the heap
            GC.RemoveMemoryPressure(64 * 1024 * 1024); // release

            // Returns the current generation number of the specified object.
            GC.GetGeneration(this);

            // Retrieves the number of bytes currently thought to be allocated. A parameter indicates whether this method can wait a short interval before returning, to allow the system to collect garbage and finalize objects.
            GC.GetTotalMemory(false);
        }
    }
}
