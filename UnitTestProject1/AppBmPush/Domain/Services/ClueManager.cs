/*----------------------------------------------------------------
 *  Copyright (C) 2015 天下商机（txooo.com）版权所有
 * 
 *  文 件 名：ClueManager
 *  所属项目：
 *  创建用户：张德良
 *  创建时间：2017/5/25 星期四 上午 11:36:19
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
using UnitTestProject1.AppBmPush.Persistence;

namespace UnitTestProject1.AppBmPush.Domain.Services
{
    public class ClueManager
    {
        IRepository<ClueInfo> m_repository = new ClueRepository();      
        public ClueInfo GetClueById(int id)
        {
            return m_repository.FindBy(id.ToString());
        }
    }
}
