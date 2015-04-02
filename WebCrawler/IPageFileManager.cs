namespace WebCrawler
{
    /**
     * <p>Хранилище сохраненных страниц</p>
     */

    public interface IPageFileManager
    {
        /**
        * <p>Сохранить загруженную страницу</p>
        * @param link Ссылка
        * @param pageFile Имя файла
        * @return true если удалось сохранить страницу
        */

        bool TrySavePage(string link, string pageFile);

        /**
         * <p>Получить содержимое ранее сохранненной страницы</p>
         *
         * @param link Ссылка
         * @return содержимое страницы, null если страница не была ранее сохранена
         */

        string GetPage(string link);

        /**
         * <p>Получить список ссылок ранее сохранненных страниц</p>
         *
         * @return массив ссылок
         */

        string[] ListPages();
    }
}
