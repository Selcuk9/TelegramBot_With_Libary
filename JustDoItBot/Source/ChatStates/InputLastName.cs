using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BotLibrary.Classes;
using BotLibrary.Classes.Bot;
using BotLibrary.Classes.Controller;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;

namespace JustDoItBot.Source.ChatStates
{
    public class InputLastName : BaseState
    {
        public InputLastName(State state) : base(state)
        {

        }

        public override Hop ProcessMessage(TelegramBotClient bot, InboxMessage mes)
        {
            switch (mes.Type)
            {
                case MessageType.Text:
                    bot.SendTextMessageAsync(mes.ChatId, State.AnswerOnSuccess);
                    return State.HopOnSuccess;
                    break;
                default:

                    break;
            }

            return null;
        }
    }
}
