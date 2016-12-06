/*----------------------------------------------------------------
 *  Copyright (C) 2016 天下商机（txooo.com）版权所有
 * 
 *  文 件 名：TaskA
 *  所属项目：
 *  创建用户：张德良
 *  创建时间：2016/12/6 星期二 下午 13:45:24
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

namespace ZDL.AnyCodes.Task
{
    public class TaskA : TaskBase
    {
        public override event EventHandler OnFinish;
        public override event EventHandler OnHandin;

        public override void Excute()
        {
            bool _isOk = true;

            if (_isOk)
            {
                if(OnFinish!=null)
                OnFinish(this, new EventArgs());
            }
        }

        public override void Handin()
        {
            throw new NotImplementedException();
        }
    }


}
