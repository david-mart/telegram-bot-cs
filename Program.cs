using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
namespace denis
{
  class Program
  {
    static void Main(string[] args)
    {
      MemBot memBot = new MemBot();
      memBot.TestApiAsync();
      Console.ReadLine();
    }
  }

  class MemBot
  {
    private string token = "509677873:AAHpzUEL2TL6rVGlZ5RB8Uy_iB7GCn-0nZM";
    static Telegram.Bot.TelegramBotClient Bot;
    public async void TestApiAsync()
    {
      try
      {
        Bot = new Telegram.Bot.TelegramBotClient(token);
        var me = await Bot.GetMeAsync();
        Console.WriteLine("Hello my name is " + me.FirstName);
        Thread newThread = new Thread(MemBot.ReceiveMessage);
        newThread.Start();
      }
      catch (Exception ex)
      {
        Console.WriteLine("Hello my name is " + ex.Message);
      }
    }
    public class Message
    {
      public string request;
      public string response;
    }

    private static async void ReceiveMessage()
    {

      using (StreamReader r = new StreamReader("messages.json"))
      {
        string json = r.ReadToEnd();
        List<Message> items = JsonConvert.DeserializeObject<List<Message>>(json);

        var lastMessageId = 0;

        while (true)
        {
          var messages = await Bot.GetUpdatesAsync();
          if (messages.Length > 0)
          {
            var last = messages[messages.Length - 1];
            if (lastMessageId != last.Id)
            {
              lastMessageId = last.Id;
              Console.WriteLine(last.Message.Text);
              foreach (Message value in items)
              {
                if (last.Message.Text.Contains(value.request))
                {
                  await Bot.SendTextMessageAsync(last.Message.From.Id, value.response);
                }
              }
            }
          }
          Thread.Sleep(200);
        }
      }
    }
  }
}