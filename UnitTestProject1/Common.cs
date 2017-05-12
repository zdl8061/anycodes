﻿/*----------------------------------------------------------------
 *  Copyright (C) 2015 天下商机（txooo.com）版权所有
 * 
 *  文 件 名：Class1
 *  所属项目：
 *  创建用户：张德良
 *  创建时间：2017/5/12 星期五 下午 14:48:50
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
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Txooo;

namespace UnitTestProject1
{
    public class Common
    {
        public static PageDataView<T> GetPageData<T>(string dbNode, PageCriteria criteria, object param = null)
        {
            using (var conn = TxDataHelper.GetDataHelper(dbNode).DataConnection)
            {
                var p = new DynamicParameters();
                string proName = "ProcGetPageData";
                p.Add("TableName", criteria.TableName);
                p.Add("PrimaryKey", criteria.PrimaryKey);
                p.Add("Fields", criteria.Fields);
                p.Add("Condition", criteria.Condition);
                p.Add("CurrentPage", criteria.CurrentPage);
                p.Add("PageSize", criteria.PageSize);
                p.Add("Sort", criteria.Sort);
                p.Add("RecordCount", dbType: DbType.Int32, direction: ParameterDirection.Output);
               
                var pageData = new PageDataView<T>();
                pageData.Items = conn.Query<T>(proName, p, commandType: CommandType.StoredProcedure).ToList();              
                pageData.TotalNum = p.Get<int>("RecordCount");
                pageData.TotalPageCount = Convert.ToInt32(Math.Ceiling(pageData.TotalNum * 1.0 / criteria.PageSize));
                pageData.CurrentPage = criteria.CurrentPage > pageData.TotalPageCount ? pageData.TotalPageCount : criteria.CurrentPage;
                return pageData;
            }
        }
    }

    public class PageDataView<T>
    {
        private int _TotalNum;
        public PageDataView()
        {
            this._Items = new List<T>();
        }
        public int TotalNum
        {
            get { return _TotalNum; }
            set { _TotalNum = value; }
        }
        private IList<T> _Items;
        public IList<T> Items
        {
            get { return _Items; }
            set { _Items = value; }
        }
        public int CurrentPage { get; set; }
        public int TotalPageCount { get; set; }
    }

    public class PageCriteria
    {
        private string _TableName;
        public string TableName
        {
            get { return _TableName; }
            set { _TableName = value; }
        }
        private string _Fileds = "*";
        public string Fields
        {
            get { return _Fileds; }
            set { _Fileds = value; }
        }
        private string _PrimaryKey = "ID";
        public string PrimaryKey
        {
            get { return _PrimaryKey; }
            set { _PrimaryKey = value; }
        }
        private int _PageSize = 10;
        public int PageSize
        {
            get { return _PageSize; }
            set { _PageSize = value; }
        }
        private int _CurrentPage = 1;
        public int CurrentPage
        {
            get { return _CurrentPage; }
            set { _CurrentPage = value; }
        }
        private string _Sort = string.Empty;
        public string Sort
        {
            get { return _Sort; }
            set { _Sort = value; }
        }
        private string _Condition = string.Empty;
        public string Condition
        {
            get { return _Condition; }
            set { _Condition = value; }
        }
        private int _RecordCount;
        public int RecordCount
        {
            get { return _RecordCount; }
            set { _RecordCount = value; }
        }
    }
}
