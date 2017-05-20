using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestProject1.AppBmPush
{
    public class PushConfig
    {
        public int BrandId { get; set; }

        public bool IsEnable { get; set; }

        public int NexId { get; set; }

        public string OutTypes { get; set; }

        public string ReceiveUsers { get; set; }

        public int StartHour { get; set; }

        public int EndHour { get; set; }
    }
}
