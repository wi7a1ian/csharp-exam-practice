using System;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Threading;

namespace TraceSourceApp
{
    /// <summary>
    /// http://msdn.microsoft.com/en-us/library/ms228993(v=vs.110).aspx
    ///  TraceSource is intended to function as an enhanced tracing system and can be used in place of the static methods of the older Trace and Debug tracing classes.
    ///  The familiar Trace and Debug classes still exist, but the recommended practice is to use the TraceSource class for tracing.
    /// </summary>
    class Program
    {
        private static TraceSource mySource = new TraceSource("TraceSourceApp", SourceLevels.Verbose);
        private static TraceSource myActivitySource = new TraceSource("TraceActivity", SourceLevels.ActivityTracing);

        static void Main(string[] args)
        {
            // Activity guid
            Guid activityGuid = Guid.NewGuid();
            
            // Log activities and some other event in a CSV file
            using (DelimitedListTraceListener log1 = new DelimitedListTraceListener("./Error1.csv", "some csv listener"))
            {
                myActivitySource.Listeners.Add(log1);
                myActivitySource.TraceEvent(TraceEventType.Start, 1);

                log1.Delimiter = ", ";
                log1.TraceOutputOptions = TraceOptions.Callstack | TraceOptions.DateTime | TraceOptions.ProcessId | TraceOptions.Timestamp;
                log1.TraceEvent(new TraceEventCache(), "Lol", TraceEventType.Error, 2);
                log1.TraceEvent(new TraceEventCache(), "SomeSource", TraceEventType.Error, 1, "Some error message");

                myActivitySource.TraceEvent(TraceEventType.Stop, 1);
                myActivitySource.Close();

                log1.Flush();
            }

            using (XmlWriterTraceListener log2 = new XmlWriterTraceListener("./Error2.log"))
            {
                log2.TraceOutputOptions = TraceOptions.LogicalOperationStack;
                log2.TraceData(new TraceEventCache(), "TraceSourceApp", TraceEventType.Stop, 2, new int[] { 1, 2, 3 });
                log2.TraceEvent(new TraceEventCache(), "SomeSource", TraceEventType.Error, 1, "Some error message");
                // The default TraceTransfer method in the base TraceListener class calls the 
                // TraceListener.TraceEvent(TraceEventCache, String, TraceEventType, Int32, String) method to process the call, 
                // setting eventType to TraceEventType.Transfer and appending a string representation of the relatedActivityId GUID to message
                log2.TraceTransfer(new TraceEventCache(), "TraceSourceApp", 1, "Lololo", activityGuid);
            }

            Trace.WriteLineIf(1 == 1, "Lol");
            //Trace.Assert(1 != 1, "Lol"); // Will throw a popup in Release and Debug
            //Debug.Assert(1 != 1, "Lol"); // Will throw a popup in Debug

            Activity1();
            Activity2();
            Activity3();

            Console.ReadKey();

            mySource.Close();
            return;
        }
        static void Activity1()
        {
            mySource.TraceEvent(TraceEventType.Error, 1,
                "Error message.");
            mySource.TraceEvent(TraceEventType.Warning, 2,
                "Warning message.");
        }
        static void Activity2()
        {
            mySource.TraceEvent(TraceEventType.Critical, 3,
                "Critical message.");
            mySource.TraceInformation("Informational message.");
        }
        static void Activity3()
        {
            mySource.TraceEvent(TraceEventType.Error, 4,
                "Error message.");
            mySource.TraceInformation("Informational message.");
        }
    }
}