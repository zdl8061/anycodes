﻿/*----------------------------------------------------------------
 *  Copyright (C) 2016 天下商机（txooo.com）版权所有
 * 
 *  文 件 名：QrHelper
 *  所属项目：
 *  创建用户：张德良
 *  创建时间：2017/3/23 星期四 下午 13:43:39
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
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using ThoughtWorks.QRCode.Codec;

namespace ZDL.AnyCodes
{
    public class QrHelper
    {      
        /// <summary>
        /// 生成二维码
        /// </summary>
        /// <param name="Content">内容文本</param>
        /// <param name="QRCodeEncodeMode">二维码编码方式</param>
        /// <param name="QRCodeErrorCorrect">纠错码等级</param>
        /// <param name="QRCodeVersion">二维码版本号 0-40</param>
        /// <param name="QRCodeScale">每个小方格的预设宽度（像素），正整数</param>
        /// <param name="size">图片尺寸（像素），0表示不设置</param>
        /// <param name="border">图片白边（像素），当size大于0时有效</param>
        /// <returns></returns>
        public static System.Drawing.Image CreateQRCode(string Content,
            QRCodeEncoder.ENCODE_MODE QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE,
            QRCodeEncoder.ERROR_CORRECTION QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.M,
            int QRCodeVersion = 0,
            int QRCodeScale = 5,
            int size = 200,
            int border = 10)
        {
            QRCodeEncoder qrCodeEncoder = new QRCodeEncoder();
            qrCodeEncoder.QRCodeEncodeMode = QRCodeEncodeMode;
            qrCodeEncoder.QRCodeErrorCorrect = QRCodeErrorCorrect;
            qrCodeEncoder.QRCodeScale = QRCodeScale;
            qrCodeEncoder.QRCodeVersion = QRCodeVersion;
            System.Drawing.Image image = qrCodeEncoder.Encode(Content);

            #region 根据设定的目标图片尺寸调整二维码QRCodeScale设置，并添加边框
            if (size > 0)
            {
                //当设定目标图片尺寸大于生成的尺寸时，逐步增大方格尺寸
                #region 当设定目标图片尺寸大于生成的尺寸时，逐步增大方格尺寸
                while (image.Width < size)
                {
                    qrCodeEncoder.QRCodeScale++;
                    System.Drawing.Image imageNew = qrCodeEncoder.Encode(Content);
                    if (imageNew.Width < size)
                    {
                        image = new System.Drawing.Bitmap(imageNew);
                        imageNew.Dispose();
                        imageNew = null;
                    }
                    else
                    {
                        qrCodeEncoder.QRCodeScale--; //新尺寸未采用，恢复最终使用的尺寸
                        imageNew.Dispose();
                        imageNew = null;
                        break;
                    }
                }
                #endregion

                //当设定目标图片尺寸小于生成的尺寸时，逐步减小方格尺寸
                #region 当设定目标图片尺寸小于生成的尺寸时，逐步减小方格尺寸
                while (image.Width > size && qrCodeEncoder.QRCodeScale > 1)
                {
                    qrCodeEncoder.QRCodeScale--;
                    System.Drawing.Image imageNew = qrCodeEncoder.Encode(Content);
                    image = new System.Drawing.Bitmap(imageNew);
                    imageNew.Dispose();
                    imageNew = null;
                    if (image.Width < size)
                    {
                        break;
                    }
                }
                #endregion

                //如果目标尺寸大于生成的图片尺寸，则为图片增加白边
                #region 如果目标尺寸大于生成的图片尺寸，则为图片增加白边
                if (image.Width <= size)
                {
                    //根据参数设置二维码图片白边的最小宽度
                    #region 根据参数设置二维码图片白边的最小宽度
                    if (border > 0)
                    {
                        while (image.Width <= size && size - image.Width < border * 2 && qrCodeEncoder.QRCodeScale > 1)
                        {
                            qrCodeEncoder.QRCodeScale--;
                            System.Drawing.Image imageNew = qrCodeEncoder.Encode(Content);
                            image = new System.Drawing.Bitmap(imageNew);
                            imageNew.Dispose();
                            imageNew = null;
                        }
                    }
                    #endregion

                    //当目标图片尺寸大于二维码尺寸时，将二维码绘制在目标尺寸白色画布的中心位置
                    if (image.Width < size)
                    {
                        //新建空白绘图
                        System.Drawing.Bitmap panel = new System.Drawing.Bitmap(size, size);
                        System.Drawing.Graphics graphic0 = System.Drawing.Graphics.FromImage(panel);
                        int p_left = 0;
                        int p_top = 0;
                        if (image.Width <= size) //如果原图比目标形状宽
                        {
                            p_left = (size - image.Width) / 2;
                        }
                        if (image.Height <= size)
                        {
                            p_top = (size - image.Height) / 2;
                        }

                        //将生成的二维码图像粘贴至绘图的中心位置
                        graphic0.DrawImage(image, p_left, p_top, image.Width, image.Height);
                        image = new System.Drawing.Bitmap(panel);
                        panel.Dispose();
                        panel = null;
                        graphic0.Dispose();
                        graphic0 = null;
                    }
                }
                #endregion
            }
            #endregion
            return image;
        }

        public static void RenderQr(System.Drawing.Image image, HttpResponse response)
        {
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            image.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
            HttpContext.Current.Response.ClearContent();
            HttpContext.Current.Response.ContentType = "image/Png";
            HttpContext.Current.Response.OutputStream.Write(ms.ToArray(), 0, ms.ToArray().Length);
            HttpContext.Current.Response.End();

            ms.Close();
            ms = null;
            image.Dispose();
            image = null;
        }
    }
}
