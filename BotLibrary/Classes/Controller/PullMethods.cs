using System;
using System.Collections.Generic;
using System.Text;
using BotLibrary.Classes.Bot;
using JetBrains.Annotations;
using Telegram.Bot;

namespace BotLibrary.Classes.Controller
{
    public class PullMethods
    {
        private Func<long, State> _getUserCurrentChatState;
        private Action<long> _checkUserBeforeMessageProcess;
        private Action<TelegramBotClient, long, State, Hop> _changeCurrentState;

        public PullMethods([NotNull] Func<long, State> getCurrentUserState,
            [NotNull] Action<long> checkUserBeforeMessageProcessing,
            [NotNull] Action<TelegramBotClient, long, State, Hop> changeCurrentChatState)
        {
            this._getUserCurrentChatState = getCurrentUserState;
            this._checkUserBeforeMessageProcess = checkUserBeforeMessageProcessing;
            this._changeCurrentState = changeCurrentChatState;
        }

        public State GetUserCurrentChatState(long chatId)
        {
            return this._getUserCurrentChatState(chatId) ?? null;
        }

        public void CheckUserBeforeMessageProcess(long chatId)
        {
            this._checkUserBeforeMessageProcess(chatId);
        }

        public void ChangeCurrentChatState(TelegramBotClient bot, long chatId, State currentState, Hop hop)
        {
            this._changeCurrentState(bot, chatId, currentState, hop);
        }

    }
}
