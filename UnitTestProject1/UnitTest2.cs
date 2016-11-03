using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest2
    {
        [TestMethod]
        public void TestMethod2()
        {

            //var _industryList = MainClassCache.MainClassList.Where(c => c.ParentId == 1);

            //var _brandQuery = from i in _industryList
            //                  join t in TagMapCache.TagMapList
            //                  on i.ClassId equals t.ParentId
            //                  group t by new { t.ParentId, t.Parentname, t.Aliasname } into g
            //                  select new { ClassId = g.Key.ParentId, ClassName = g.Key.Parentname, AliasName = g.Key.Aliasname, brandCount = g.Count() };
            //var _industryBrandCount = _brandQuery.OrderByDescending(a => a.brandCount);





            //var _lyQuery = from i in _industryList
            //               join t in TagMapCache.TagMapList
            //               on i.ClassId equals t.ParentId
            //               join b in BrandCache.BrandList
            //               on t.BrandId equals b.BrandId
            //               select new { t.ParentId, t.Parentname, t.Aliasname, b.LyCount };
            //var _industryLyCount = (from t in _lyQuery
            //                        group t by new { t.ParentId, t.Parentname, t.Aliasname } into g
            //                        select new { ClassId = g.Key.ParentId, ClassName = g.Key.Parentname, AliasName = g.Key.Aliasname, lyCount = g.Sum(r => r.LyCount) })
            //                       .OrderByDescending(t => t.lyCount);
        }

    }
}
