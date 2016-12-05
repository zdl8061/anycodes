/*----------------------------------------------------------------
 *  Copyright (C) 2016 天下商机（txooo.com）版权所有
 * 
 *  文 件 名：OrderTest
 *  所属项目：
 *  创建用户：张德良
 *  创建时间：2016/12/5 星期一 下午 17:15:56
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

namespace ZDL.AnyCodes
{
    public class OrderTest
    {
       public event EventHandler OnPaySuccess;
        /// <summary>
        /// 产品图片
        /// </summary>
        public string ProductImg { get; set; }
        /// <summary>
        /// 产品单价
        /// </summary>
        public double ProductPrices { get; set; }
        /// <summary>
        /// 订单数量
        /// </summary>
        public int ProductCount { get; set; }
        /// <summary>
        /// 运费
        /// </summary>
        public double Postage { get; set; }

        /// <summary>
        /// 订单用户id
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// 是否是虚拟产品
        /// </summary>
        public int IsVirtual { get; set; }

       

        /// <summary>
        /// 支付成功，处理订单信息
        /// 此操作必须在支付信息已经记录入库之后调用
        /// </summary>
        /// <param name="orderSN"></param>
        /// <returns></returns>
        public bool PayOK()
        {
            if (OnPaySuccess != null)
                OnPaySuccess(this, new EventArgs());
            return false;

        }
        
    }
}
