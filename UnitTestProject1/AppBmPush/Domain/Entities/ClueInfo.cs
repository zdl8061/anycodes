using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestProject1.AppBmPush
{
    public class ClueInfo
    {
        public int OutId { get; set; }

        public int ClueId { get; set; }

        public string ClueName { get; set; }

        public int OutType { get; set; }

        public DateTime OutTime { get; set; }

        
        public bool IsPush() {
            return false;
        }

        public string ToMobileMsg()
        {
            return "";
        }
    }
}
