using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;

namespace TranslatorBot
{
    internal class Program
    {
        public static TelegramBotClient botClient;

        [Obsolete]
        static void Main(string[] args)
        {

            string token = new Claves().botToken;

            //Starting the bot
            botClient = new TelegramBotClient(token);
            var meBot = botClient.GetMeAsync().Result;

            Console.WriteLine($"Hola, mid id es {meBot.Id} y mi nombre: {meBot.FirstName}");

            //Checks every new messaje
            botClient.OnMessage += BotClient_OnMessage;
            botClient.StartReceiving();
            Console.ReadKey();
            botClient.StopReceiving();
        }

        [Obsolete]
        private static void BotClient_OnMessage(object sender, Telegram.Bot.Args.MessageEventArgs e)
        {

            var id = e.Message.Chat.Id;
            var text = e.Message.Text;
            var from = e.Message.From.FirstName;

            Console.WriteLine($"He recidibo: {text} de {from}");

            string message = "";

            //Switch with the different commands and instructions
            switch (text)
            {
                case "/patata":
                    message = "Hola patata";
                    botClient.SendTextMessageAsync(id, message);
                    break;

                case "/start":
                    message = $"Hola {from}, ¡Bienvenido al bot traductor!";
                    botClient.SendTextMessageAsync(id, message);
                    Console.WriteLine(message);
                    break;

                case "/help":
                    message = $"Mi funcionamiento es simple, envía el mensaje que quieras en español y lo traduciré a inglés :)";
                    botClient.SendTextMessageAsync(id, message);
                    Console.WriteLine(message);
                    break;

                //If it is not a command the text is translated and returned
                default:
                    var traductor = new Translator();
                    var translation = traductor.translate(text);
                    botClient.SendTextMessageAsync(id, translation.Result);
                    break;
            }
        }
    }
}
