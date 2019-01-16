using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Zoo.HightConcurrence
{
    /// <summary>
    /// https://github.com/durow/TestArea/blob/master/AsyncTest/ConcurrenceTest/MainWindow.xaml.cs
    /// </summary>
    class ConcurrenceTools
    {
        public IEnumerable<RequestData> CreateRequestData(int requestCount)
        {
            for (int i = 0; i < requestCount; i++)
            {
                var request = new RequestData()
                {
                    No = Guid.NewGuid().ToString()
                };
                yield return request;
            }
        }

        private void ThreadCallback(object o)
        {
            var task = o as RequestData;
            while (true)
            {
                
            }
        }
    }
}
