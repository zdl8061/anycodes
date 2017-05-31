/*----------------------------------------------------------------
 *  Copyright (C) 2015 天下商机（txooo.com）版权所有
 * 
 *  文 件 名：ChatInfo
 *  所属项目：
 *  创建用户：张德良
 *  创建时间：2017/5/31 星期三 下午 15:06:45
 *  
 *  功能描述：
 *          1、
 *          2、 
 * 
 *  修改标识：  
 *  修改描述：
 *  待 完 善：
 *          1、 
----------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoDBDemo
{
    public class ChatInfo
    {
        public IList<MsgBodyInfo> MsgBody { get; set; }
        public string CallbackCommand { get; set; }
        public string From_Account { get; set; }
        public string To_Account { get; set; }
        public int MsgTime { get; set; }

        public class MsgContentInfo
        {
            public string Text { get; set; }
            public int? Index { get; set; }
            public string Data { get; set; }
        }

        public class MsgBodyInfo
        {
            public string MsgType { get; set; }
            public MsgContentInfo MsgContent { get; set; }
        }
    }
}
