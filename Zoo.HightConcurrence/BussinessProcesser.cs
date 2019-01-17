using System;
using System.Threading;

namespace HighConcurrence.App
{
    public class BussinessProcesser
    {
        private int limit = 10;
        public void Process()
        {
            var data = new RequestData() { No = Guid.NewGuid().ToString() };
            long storeLength = QueueManager.Instance.QueueLength();
            if (storeLength < limit)
            {
                try
                {
                    QueueManager.Instance.AddQueue(data.No);
                    Console.WriteLine($"[{DateTime.Now.ToString()}]加入数据[No:{data.No}]");


                    //处理当前请求中数据业务 
                    Thread.Sleep(100);

                }
                catch 
                {
                    
                }
                finally
                {
                    QueueManager.Instance.RemoveQueue(data.No);
                    Console.WriteLine($"[{DateTime.Now.ToString()}]已处理数据[No:{data.No}]");
                }
            }
            else
                Console.WriteLine($"[{DateTime.Now.ToString()}]超出限制，无法处理数据，[No:{data.No}]");
        }

    }
}
