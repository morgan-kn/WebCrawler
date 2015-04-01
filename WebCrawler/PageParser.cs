namespace WebCrawler
{
    /**
     * <p>Операции над HTML страницами</p>
     */

    public interface IPageParser
    {
            //Убрать html-теги из файла
            //параметры: htmlFile (входной), textFile (выходной)
            //вернуть true, если удалось сохранить выходной файл

        bool TryStripTags(string htmlFile, string textFile);
        
            //получить список ссылок, находящихся на html-странице
            //вернуть массив ссылок

        string[] GetLinks(string htmlFile);
    }
}
