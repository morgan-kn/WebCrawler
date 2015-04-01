using System;
using System.Threading;

namespace WebCrawler
{
       class Program
    {
        static void Main(string[] args)
        {
            Thread myThread = new Thread(func); //Создаем новый объект потока (Thread)

            myThread.Start(); //запускаем поток

            for (int i = 0; i < 10; i++ )
            {
                Console.WriteLine("Поток 1 выводит " + i);
                Thread.Sleep(0);
            }

            Console.Read(); //Приостановим основной поток

        }

        //Функция запускаемая из другого потока
           private static void func()
           {
               for (var i = 0; i < 10; i++)
               {
                   Console.WriteLine("Поток 2 выводит " + i);
                   Thread.Sleep(0);
               }
           }
    }
}
