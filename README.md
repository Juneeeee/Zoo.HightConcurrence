# Restriction-Request

## 为控制站点请求上限而设计的程序

#### 基本思路
利用redis的set集合创建一个```QueueManager```的缓存，存储业务中的唯一键，处理完成业务后移除键值。
set集合设置上限，当超过上限时不在处理当前请求

```
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

```
