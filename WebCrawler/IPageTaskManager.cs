namespace WebCrawler
{
    /**
     * <p>Менеджер задач.</p>
     */

    public interface IPageTaskManager
    {
        // Получить задачу для обработки, 
        // Вернуть ссылку для скачивания

        string GetTask();

        // Вернуть задачу обратно в очередь, параметр - ссылка

        void UncompleteTask(string link);
        // Пометить задачу как завершенную. Положить список новых ссылок в очередь
        // параметр - ссылка
        // вернуть список новых ссылок
        
        void CompleteTask(string link, string[] newLinks);
    }
}
