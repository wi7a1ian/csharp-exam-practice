// Specify /d:DEBUG when compiling. 

using System;
using System.Data;
using System.Diagnostics;

class Test
{
    static void Main()
    {
        Debug.Listeners.Add(new TextWriterTraceListener(Console.Out));
        Debug.AutoFlush = true;
        Debug.Indent();
        Debug.WriteLine("Entering Main");
        Console.WriteLine("Hello World.");
        Debug.WriteLine("Exiting Main");
        Debug.Unindent();

        Debug.Assert(1 == 2, "Expected Debug Msg");

        Trace.Listeners.Add(new TextWriterTraceListener(Console.Out));
        Trace.AutoFlush = true;
        Trace.Indent();
        Trace.WriteLine("Entering Main");
        Console.WriteLine("Hello World.");
        Trace.WriteLine("Exiting Main");
        Trace.Unindent();
        Trace.Assert(1 == 2, "Expected Trace Msg");
    }
}