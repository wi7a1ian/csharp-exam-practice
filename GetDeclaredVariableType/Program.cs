using System;
using System.Collections.Generic;

static class Program
{
    public static Type GetDeclaredType<T>(T x)
    {
        return typeof(T);
    }

    static string GetName<T>(T item) where T : class
    {
        var properties = typeof(T).GetProperties();
        return properties[0].Name;
    }

    // Demonstrate how GetDeclaredType works
    static void Main(string[] args)
    {
        IList<string> iList = new List<string>();
        List<string> list = null;

        Console.WriteLine(GetDeclaredType(iList).Name);
        Console.WriteLine(GetDeclaredType(list).Name);
        Console.WriteLine("Name is '{0}'", GetName(new { args }));
    }
}