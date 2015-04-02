using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WebCrawler.Tests
{
    [TestClass]
    public class IPageTaskManagementTest
    {
        [TestMethod]
        public void GetTaskReturnLink()
        {
            IPageTaskManager iPageTaskManager = null;
            Task gotTask = null;
            var link = iPageTaskManager.GetTask();
            Assert.IsTrue(link.GetType() == typeof(string));
            Assert.IsTrue(gotTask.GetType() == typeof(Task));
        }
    }

    public class Task
    {
        
    }
}
