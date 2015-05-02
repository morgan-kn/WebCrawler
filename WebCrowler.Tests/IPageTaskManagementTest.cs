using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WebCrawler.Tests
{
    [TestClass]
    public class IPageTaskManagementTest
    {
        private IPageTaskManager pageTaskManager ;
    
        [TestInitialize]
        public void SetUp()
        {
            File.Delete("completedTasks.txt");
            File.Delete("queue.txt");
            pageTaskManager = new PageTaskManager();
        }

        [TestMethod]
        public void TestAllTheWay()
        {
            pageTaskManager.CompleteTask(null, new[] {"http://yandex.ru", "http://google.com"});
            var link = pageTaskManager.GetTask();
            Assert.AreEqual("http://yandex.ru", link);
            var link2 = pageTaskManager.GetTask();
            Assert.AreEqual("http://google.com", link2);

            pageTaskManager.CompleteTask("http://google.com", new[] {"http://e1.ru"});
            pageTaskManager.UncompleteTask("http://yandex.ru");

            var link3 = pageTaskManager.GetTask();
            Assert.AreEqual("http://e1.ru", link3);

            var link4 = pageTaskManager.GetTask();
            Assert.AreEqual("http://yandex.ru", link4);

            Assert.AreEqual(null, pageTaskManager.GetTask());
        }

        [TestMethod]
        public void TestEmpty()
        {
            var link = pageTaskManager.GetTask();
            Assert.AreEqual(null, link);
        }

        [TestMethod]
        public void TestRepeatedTasks()
        {
            pageTaskManager.CompleteTask(null, new [] {"http://yandex.ru"});
            var link = pageTaskManager.GetTask();
            Assert.AreEqual(link, "http://yandex.ru");
            pageTaskManager.CompleteTask("http://yandex.ru", new []{"http://yandex.ru", "http://google.com"});
            Assert.AreEqual("http://google.com", pageTaskManager.GetTask());

            Assert.IsTrue(pageTaskManager.GetTask() == null);
        }

        [TestMethod]
        public void TestSaveState()
        {
            Assert.AreEqual(pageTaskManager.GetTask(), null);
            pageTaskManager.CompleteTask("http://yandex.ru", new[] { "http://yandex1.ru", "http://google1.ru" });
            Assert.AreEqual(pageTaskManager.GetTask(), "http://yandex1.ru");
            Assert.AreEqual(pageTaskManager.GetTask(), "http://google1.ru");
            pageTaskManager.CompleteTask("http://yandex1.ru", new[] { "http://e1.ru" });
            pageTaskManager.CompleteTask("http://google1.ru", new string[0]);
            pageTaskManager = new PageTaskManager();
            Assert.AreEqual("http://e1.ru", pageTaskManager.GetTask());
        }
    }
}


//todo на этапе инициализации понять, что в файлике хлам и не класть в очередь