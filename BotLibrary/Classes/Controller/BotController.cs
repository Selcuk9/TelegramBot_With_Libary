using BotLibrary.Classes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using BotLibrary.Classes.Controller;
using JetBrains.Annotations;
using Telegram.Bot;
using Telegram.Bot.Args;

namespace BotLibrary.Classes.Controller
{
    public class BotController
    {
        private BaseBot Bot;
        private PullMethods Methods;
        private ReflectionInfo Reflection;
        private MessageProcessor proc;

        public BotController([NotNull] TelegramBotClient bot,[NotNull] ReflectionInfo reflectionInfo,[NotNull] PullMethods pullMethods)
        {
            this.Bot = new BaseBot(bot);
            this.Methods = pullMethods;
            this.Reflection = reflectionInfo;

            proc = new MessageProcessor(this.Reflection, this.Methods);
            Init();
        }

        private void Init()
        {
            this.Bot.TelegramClient.OnMessage -= ProcessMessage;
            this.Bot.TelegramClient.OnMessage += ProcessMessage;
        }

        /// <summary>
        /// Обработчик события сообщения.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void ProcessMessage(object sender, MessageEventArgs args)
        {
            InboxMessage mes = new InboxMessage(this.Bot.TelegramClient, args.Message);
            proc.ProcessCurrentMessage(mes);
        }

        public void StartBot()
        {
            this.Bot.Start();
        }

        public void StopBot()
        {
            this.Bot.Stop();
        }


        /// <summary>
        /// Получаем токен из json файла
        /// </summary>
        private string GetTokenFromFile()
        {
            if (File.Exists(Directory.GetCurrentDirectory()+ "\\" + "token.txt") == false)
            {
                throw new Exception("Файл токена не существует!");
            }

            return File.ReadAllText("token.txt");
        }

    }
}
