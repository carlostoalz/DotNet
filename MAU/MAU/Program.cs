using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace MAU
{
    class Program
    {
        static void Main(string[] args)
        {
            Task<int[]> parent = Task.Run(() =>
            {
                int[] results = new int[3];
                TaskFactory tf = new TaskFactory(TaskCreationOptions.AttachedToParent, TaskContinuationOptions.ExecuteSynchronously);
                tf.StartNew(() => results[0] = 0);
                tf.StartNew(() => results[1] = 1);
                tf.StartNew(() => results[2] = 2);

                return results;
            });

            Task finalTask = parent.ContinueWith(
                parentTask =>
                {
                    foreach (int i in parent.Result)
                    {
                        Console.WriteLine(i);
                    }
                }
            );

            CancellationToken token = new CancellationToken();
            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

            Task task = Task.Run(() => 
            { 
                while(!token.IsCancellationRequested)
                {
                    Console.Write("*");
                    Thread.Sleep(1000);
                }
            }, token);

            Console.WriteLine("Press enter to stop task");
            Console.ReadLine();
            cancellationTokenSource.Cancel();
            Console.WriteLine("Press enter to end application");
            Console.ReadLine();


            //finalTask.Wait();

            //var a = DownloadContent().Result;
        }

        public static async Task<string> DownloadContent()
        {
            using (HttpClient client = new HttpClient())
            {
                string result = await client.GetStringAsync("http://www.microsoft.com");
                return result;
            }
        }
    }
}
