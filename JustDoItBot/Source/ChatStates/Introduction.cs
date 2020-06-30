using BotLibrary.Classes;
using BotLibrary.Classes.Bot;
using BotLibrary.Classes.Controller;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;

namespace JustDoItBot.Source.ChatStates
{
    public class Introduction : BaseState
    {
        public Introduction(State state) : base(state)
        {

        }

        public override Hop ProcessMessage(TelegramBotClient bot, InboxMessage mes )
        {
            switch (mes.Type)
            {
                case MessageType.Text:
                    return this.State.HopOnSuccess;
                    break;
            }

            return null;

            //base.ProcessMessageForState(db, mes, user);
        }

       
    }
}
