using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DotNet.Utilities.Test
{
    [TestClass]
    public class PatternTest1
    {
        [TestMethod]
        public void TestMethod1()
        {

        }
    }

    public class EmployeeInfo
    {
        public string Name { get; set; }

        public string Phone { get; set; }

        public void Befire(Visitor visitor)
        {
            //visitor.Visit(this);
        }
    }

    public class Manager : Visitor
    {
        public void Visit(Manager manage)
        {

        }
    }

    public class Visitor
    {
       //abstract void Visit()
    }
}
