using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace EventBasedAsyncPatterMixWithTAP
{
    /// <summary>
    /// http://msdn.microsoft.com/en-us/library/hh873178(v=vs.110).aspx
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
        }

        public static Task<string> DownloadStringAsync(Uri url)
        {
            var tcs = new TaskCompletionSource<string>();
            var wc = new WebClient();
            wc.DownloadStringCompleted += (s, e) =>
            {
                if (e.Error != null) tcs.TrySetException(e.Error);
                else if (e.Cancelled) tcs.TrySetCanceled();
                else tcs.TrySetResult(e.Result);
            };
            wc.DownloadStringAsync(url);
            return tcs.Task;
        }
    }
}
