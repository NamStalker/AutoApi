using System;
using System.Threading;
using System.Threading.Tasks;
using System.Net.Http;
using System.Collections.Generic;

namespace BulkApiCall
{
    class Program
    {
        static void Main(string[] args)
        {
            Values newProg = new Values();

            int iteration = 0;
            int count = 0;
            List<Task> tList = new List<Task>();

            while(count < newProg.Length / 2)
            {
                for (int i = 0; i < 1000; i++)
                {
					// Run in batches and wait for each call to complete 
					// to avoid bogging down the api
                    if (count >= newProg.Length / 2)
                        break;
                    tList.Add(new Task(() => newProg.RunValues(newProg.GetValue(count))));
                    tList[count - (1000 * iteration)].Start();
                    tList[count - (1000 * iteration)].Wait();
                    count++;
                }
                iteration++;
                tList.Clear();
                Console.WriteLine("Batch Done.");
            }

            while(true)
                Thread.Sleep(50000);
        }
    }
}
