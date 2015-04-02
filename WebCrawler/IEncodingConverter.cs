namespace WebCrawler
{
    /**
     * <p>Выполняет трансформацию кодировок.</p>
     */
    public interface IEncodingConverter
    {

        /**
         * <p>Выполнить копирование указанного текстового файла, выполняя трансформацию кодировки.</p>
         *
         * @param inputFile Имя исходного файла
         * @param outputFile Имя выходной файла
         * @param sourceEncoding  Имя исходной кодировки
         * @param destEncoding  Имя выходной  кодировки
         * @return true если выходной файл успешно сохранен
         */

        bool TryConvert(string inputFile, string outputFile, string sourseEncoding, string destEncoding);
    }
}
