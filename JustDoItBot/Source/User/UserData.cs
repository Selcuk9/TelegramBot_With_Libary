using System;
using System.Collections.Generic;
using System.Text;
using Telegram.Bot.Types;

namespace BotControllerProj.Source.User
{
    public class UserData
    {
        public string State;
        public ChatId ChatId;
        public string Role;

        public UserData(ChatId chatId, string state)
        {
            this.ChatId = chatId;
            this.State = state;
        }
    }
}
