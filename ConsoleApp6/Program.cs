using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace MyApp 
{
    internal class Program
    {

        static void Main()
        {
            var a = new DummyRequestHandler();
            Console.WriteLine("Программа начала работу");
            Console.WriteLine("Введите текст запроса. Для остановки программы напишите /exit");
            //сообщение
            string command = Console.ReadLine();

            while (command != "/exit")
            {
                //ввод аргументов
                var arguments = new string[100];
                Console.WriteLine("Введите аргументы запроса. Для остановки добавления аргументов напишите /end");
                var argument = Console.ReadLine();
                var i = 0;
                while (argument!="/end")
                {
                    arguments[0] = argument;
                    Console.WriteLine("Введите следующий аргумент запроса. Для остановки добавления аргументов напишите /end");
                    argument = Console.ReadLine();
                    i += 1;
                }
                //запуск потока
                //new Thread(() => Go(a, command, arguments)).Start();
                ThreadPool.QueueUserWorkItem(callBack => Go(a, command, arguments));
                Console.WriteLine("Введите текст запроса. Для остановки программы напишите /exit");
                command = Console.ReadLine();

            }
            Console.WriteLine("Программа завершила работу.");
            //чтобы потоки успели закончить работу
            Thread.Sleep(10_000);
        }

        public static void Go(DummyRequestHandler a, string command, string[] arguments)
        {
            var id = Guid.NewGuid().ToString("D");
            Console.WriteLine($"Было отправлено сообщение '{command}'. Присвоен индефикатор {id}");
            try
            {
                var id1 = a.HandleRequest(command, arguments);
                Console.WriteLine($"Сообщение с индефикатором {id} получило ответ {id1}");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Сообщение с индефикатором {id} упало с ошибкой: {e.Message} ");
            }
        }
        
    }

}