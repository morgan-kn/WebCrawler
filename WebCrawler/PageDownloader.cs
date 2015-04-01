namespace WebCrawler
{
    /**
     * <p>Выполняет загрузку HTML страницы по ссылке.</p>
     */

    public interface PageDownloader
    {
        /**
       * <p>Загружает HTML страницу по ссылке.</p>
       *
       * @param link Ссылка
       * @param outputFile Имя выходного файла
       * @return true если удалось выполнить загрузку HTML страницы
       */
        bool TryDownloadHtml(string link, string outputFile);
    }
}
