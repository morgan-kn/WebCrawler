using System.Runtime.InteropServices;

namespace WebCrawler
{
    /**
     * <p>Выполняет определение кодировоки файла.</p>
     * Делается частотным анализом
     */

    public interface EncodingDetector
    {
        /**
        * <p>Выполнить определение кодировоки файла.</p>
        *
        * @param file Имя исходного файла
        * @return Кодировка файла, null если не удалось определить кодировку
        */
        CharSet DetectEncoding(string file);
    }
}
