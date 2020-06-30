using System;
using System.Reflection;
using System.Windows.Forms;
using BotLibrary.Classes.Controller;
using BotLibrary.Classes.Controller;
using JustDoItBot.Source.DbMethods;
using Telegram.Bot;


namespace JustDoItBot
{
    public partial class Form1 : Form
    {
        private BotController botController;

        public Form1()
        {
            InitializeComponent();
            TelegramBotClient bot = new TelegramBotClient("token");
            ReflectionInfo info = new ReflectionInfo(typeof(Form1).Assembly, "JustDoItBot.Source.ChatStates");
            PullMethods methods = new PullMethods(DbMethods.GetCurrentUserState,
                DbMethods.CheckUserBeforeMessageProcessig,
                DbMethods.ChangeCurrentStateAndMakeHop);

            this.botController = new BotController(bot, info, methods);
        }

        private void StartBotButton_Click(object sender, EventArgs e)
        {
            this.botController.StartBot();
        }

        private void StopBotButton_Click(object sender, EventArgs e)
        {
            this.botController.StopBot();
        }
    }
}
