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
                // 此处应考虑写入的值在当前进程中及时被消费掉，否则造成队列资源浪费


                QueueManager.Instance.AddQueue(data.No);
                Console.WriteLine($"[{DateTime.Now.ToString()}]加入数据[No:{data.No}]");

                //处理当前请求中数据业务 
                Thread.Sleep(100);
                QueueManager.Instance.RemoveQueue(data.No);
                Console.WriteLine($"[{DateTime.Now.ToString()}]已处理数据[No:{data.No}]");
            }
            else
                Console.WriteLine($"[{DateTime.Now.ToString()}]超出限制，无法处理数据，[No:{data.No}]");
        }

    }
}
