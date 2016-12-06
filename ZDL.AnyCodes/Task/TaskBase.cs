/*----------------------------------------------------------------
 *  Copyright (C) 2016 天下商机（txooo.com）版权所有
 * 
 *  文 件 名：ITask
 *  所属项目：
 *  创建用户：张德良
 *  创建时间：2016/12/6 星期二 下午 13:40:21
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
    public abstract class TaskBase
    {
        public abstract event EventHandler OnHandin;
        public abstract event EventHandler OnFinish;

        public abstract void Handin();
        public abstract void Excute();
    }
}
