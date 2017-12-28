using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
namespace TelegramBot
{
    class Program
    {
        static void Main(string[] args)
        /// <summary>
        ///  Создаем объект,который вызвыает метод TestApiAsync.
        /// </summary>
        {
            MemBot memBot = new MemBot();
            memBot.TestApiAsync();
            Console.ReadLine();
        }
    }
    class MemBot
    {
        private string token = "509677873:AAHpzUEL2TL6rVGlZ5RB8Uy_iB7GCn-0nZM";//токен бота.
        static Telegram.Bot.TelegramBotClient Bot;
        public async void TestApiAsync()
        {
            try
            {   /// <summary>
                ///  Создаем объект API.
                /// </summary>
                Bot = new Telegram.Bot.TelegramBotClient(token);
                /// <summary>
                ///  Бот вызывает метод GetMeAsync(возвращает информации о приложение токен которого мы вставили).
                /// </summary>
                var me = await Bot.GetMeAsync();
                Console.WriteLine("Hello my name is " + me.FirstName);//выводит имя бота на консоль.
                /// <summary>
                ///  Запрос сообщения.
                /// </summary>
                Thread newThread = new Thread(MemBot.ReceiveMessage);
                newThread.Start();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Hello my name is " + ex.Message);
            }
        }
        /// <summary>
        ///  Поток запроса сообщений.
        /// </summary>
        private static async void ReceiveMessage()
        {
            var lastMessageId = 0;
            /// <summary>
            ///  Цикл для получения сообщений.
            /// </summary>
            while (true)
            {   /// <summary>
                ///  Получаем сообщение.
                /// </summary>
                var messages = await Bot.GetUpdatesAsync();
                /// <summary>
                ///  Проверка на сообщение.
                /// </summary>
                if (messages.Length > 0)
                {
                    var last = messages[messages.Length - 1];
                    if (lastMessageId != last.Id)//если последнее сообщение не равно предпоследнему выводим его
                    {
                        lastMessageId = last.Id;
                        Console.WriteLine(last.Message.Text);
                        if (last.Message.Text.Contains("Привет"))
                        {
                            await Bot.SendTextMessageAsync(last.Message.From.Id, "И тебе привет");//если последнее сообщение Привет ,выводиться эта строка.
                        }
                        if (last.Message.Text.Contains("Факт 0"))
                        {
                            await Bot.SendTextMessageAsync(last.Message.From.Id, "В мире всего 7 % левшей");
                        }
                        if (last.Message.Text.Contains("Факт 1"))
                        {
                            await Bot.SendTextMessageAsync(last.Message.From.Id, "Емкость мозга человека превышает 4 терабайта");
                        }
                        if (last.Message.Text.Contains("Факт 2"))
                        {
                            await Bot.SendTextMessageAsync(last.Message.From.Id, "Наш череп состоит из 29 различных костей");
                        }
                        if (last.Message.Text.Contains("Факт 3"))
                        {
                            await Bot.SendTextMessageAsync(last.Message.From.Id, "Сердце человека перекачивает 182 млн литров крови за свою жизнь");
                        }
                        if (last.Message.Text.Contains("Факт 4"))
                        {
                            await Bot.SendTextMessageAsync(last.Message.From.Id, "Зародыш приобретает отпечатки пальцев в возрасте от 3 месяцев");
                        }
                        if (last.Message.Text.Contains("Факт 5"))
                        {
                            await Bot.SendTextMessageAsync(last.Message.From.Id, "Женские сердца бьются быстрее, чем мужские");
                        }
                        if (last.Message.Text.Contains("Факт 6"))
                        {
                            await Bot.SendTextMessageAsync(last.Message.From.Id, "Праворукие люди живут в среднем на 9 лет дольше, чем левши");
                        }
                        if (last.Message.Text.Contains("Факт 7"))
                        {
                            await Bot.SendTextMessageAsync(last.Message.From.Id, "Человек забывает 90 % своих снов");
                        }
                        if (last.Message.Text.Contains("Факт 8"))
                        {
                            await Bot.SendTextMessageAsync(last.Message.From.Id, "К концу жизни человек запоминает в среднем 150 трлн бит информации");
                        }
                        if (last.Message.Text.Contains("Факт 9"))
                        {
                            await Bot.SendTextMessageAsync(last.Message.From.Id, "Люди — единственные существа, которые спят на спине");
                        }
                        
                    }
                }
                Thread.Sleep(200);
            }
        }
    }
}
