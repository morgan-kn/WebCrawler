using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;

namespace WebCrawler
{
    class WebCrawlerImpl : IWebCrawler
    {
        private readonly PageTaskManager pageTaskManager;
        private readonly PageDownloader pageDownloader;
        private readonly EncodingDetector encodingDetector;
        private readonly EncodingConverter encodingConverter;
        private readonly PageParser pageParser;
        private readonly PageFileManager pageFileManager;
        private static readonly CharSet utf8Charset = CharSet.Unicode; // private static final Charset utf8Charset = Charset.forName("utf-8");

        public WebCrawlerImpl(PageTaskManager pageTaskManager, PageDownloader pageDownloader, EncodingDetector encodingDetector, 
            EncodingConverter encodingConverter, PageParser pageParser, PageFileManager pageFileManager)
        {
            this.pageTaskManager = pageTaskManager;
            this.pageDownloader = pageDownloader;
            this.encodingDetector = encodingDetector;
            this.encodingConverter = encodingConverter;
            this.pageParser = pageParser;
            this.pageFileManager = pageFileManager;
        }

        public void Run()
        {
            while (true)
            {
                string link = pageTaskManager.GetTask();
                try
                {
                    if (link != null)
                    {
                        if (!TryRunLink(link))
                        {
                            pageTaskManager.UncompleteTask(link);
                        }
                    }
                }
                catch (Exception e)
                {
                    pageTaskManager.UncompleteTask(link);
                    throw e;
                }

                try
                {
                    Thread.Sleep(10);
                }
                catch (ThreadInterruptedException e)
                {
                    e.StackTrace.ToString();
                }
            }
        }

        private bool TryRunLink(string link)
        {
            string htmlFile = GetTempFileName();
            if (!pageDownloader.TryDownloadHtml(link, htmlFile))
            {
                return false;
            }

            CharSet pageEncoding = encodingDetector.DetectEncoding(htmlFile);
            
            if (!pageEncoding.Equals(utf8Charset))
            {
                string convertedFile = GetTempFileName();
                if (
                    !encodingConverter.TryConvert(htmlFile, convertedFile, pageEncoding.ToString(), utf8Charset.ToString()))
                {
                    DeleteFile(htmlFile);
                    return false;
                }


                DeleteFile(htmlFile);
                htmlFile = convertedFile;
            }

            string[] nextLinks = pageParser.GetLinks(htmlFile);
            string textFile = GetTempFileName();
            if (!pageParser.TryStripTags(htmlFile, textFile))
            {
                DeleteFile(htmlFile);
                return false;;
            }

            DeleteFile(htmlFile);
            if (!pageFileManager.TrySavePage(link, textFile))
            {
                DeleteFile(textFile);
                return false;
            }

            pageTaskManager.CompleteTask(link, nextLinks);
            return true;
        }

        private static void DeleteFile(string fileName)
        {
            if(!File.Exists(fileName))
                throw new Exception("Can not delete file " + fileName);
            File.Delete(fileName);
        }

        private static string GetTempFileName()
        {
            return Guid.NewGuid().ToString();
        }
    }
}
