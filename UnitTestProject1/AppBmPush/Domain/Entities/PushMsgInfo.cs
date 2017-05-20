using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestProject1.AppBmPush
{
    public class PushMsgInfo
    {
        public int MsgId { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public DateTime SendTime { get; set; }

        public int UserId { get; set; }

        public int BrandId { get; set; }

        public string MsgType { get; set; }

        public int Category { get; set; }
    }
}
