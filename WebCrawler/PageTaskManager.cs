using System.Collections.Generic;
using System.IO;

namespace WebCrawler
{
    public class PageTaskManager : IPageTaskManager
    {
        private List<string> queue;
        private HashSet<string> completedTasks;

        public PageTaskManager()
        {
            var completed = new string[0];
            if (File.Exists("completedTasks.txt"))
            {
                completed = File.ReadAllLines("completedTasks.txt");
            }

            var current = new string[0];
            if (File.Exists("queue.txt"))
            {
                current = File.ReadAllLines("queue.txt");
            }

            queue = new List<string>(current);
            completedTasks = new HashSet<string>(completed);
        }

        public string GetTask()
        {
            if (queue.Count == 0)
                return null;
            var task = queue[0];
            queue.RemoveAt(0);
            File.WriteAllLines("queue.txt", queue);
            return task;
        }

        public void UncompleteTask(string link)
        {
            queue.Add(link);
        }

        public void CompleteTask(string link, string[] newLinks)
        {
            completedTasks.Add(link);
            File.AppendAllLines("completedTasks.txt", new [] {link});
            foreach (var uncheckedLink in newLinks)
            {
                if (!completedTasks.Contains(uncheckedLink))
                {
                    queue.Add(uncheckedLink);
                    File.AppendAllLines("queue.txt", newLinks);
                }
            }
        }
    }
}