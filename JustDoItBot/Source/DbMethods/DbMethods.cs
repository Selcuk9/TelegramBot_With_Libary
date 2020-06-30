using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BotControllerProj;
using BotControllerProj.DataBase.Context;
using BotControllerProj.DataBase.Models;
using BotLibrary.Classes;
using BotLibrary.Classes.Bot;
using BotLibrary.Classes.Helpers;
using BotLibrary.Classes.StateControl;
using JustDoItBot.Source.Constants;
using Telegram.Bot;
using Telegram.Bot.Types.ReplyMarkups;

namespace JustDoItBot.Source.DbMethods
{
    public class DbMethods
    {

        /// <summary>
        /// Перед обработкой сообщения проверяем, что пользователь есть в базе данных.
        /// </summary>
        /// <param name="chatId"></param>
        public static void CheckUserBeforeMessageProcessig(long chatId)
        {
            using (BotDbContext db = new BotDbContext(HelperDataBase.DB_OPTIONS))
            {
                if (IsUserInDatabase(db, chatId) == false)
                {
                    AddUserToDb(db, chatId);
                }
            }
        }


        /// <summary>
        /// Метод получения текущего состояния чата по ChatId
        /// </summary>
        /// <param name="chatId"></param>
        /// <returns></returns>
        public static State GetCurrentUserState(long chatId)
        {
            if (InitStates.BotStates?.Count == 0) return null;

            using (BotDbContext db = new BotDbContext(HelperDataBase.DB_OPTIONS))
            {
                //Выбираем состояние чата.
                ChatState chatState = 
                    (from state in db.ChatState
                    where state.ChatId == chatId
                    select state).FirstOrDefault();

                string stateString = chatState.State;

                //Если состояние чата равно null
                if (string.IsNullOrEmpty(stateString?.Trim(' ')) == true)
                {
                    //То записать в пустую ячейку состояния имя первого состояния.
                    //ToDo Пока берем просто первый элемент из состояний, но нужно самому определять первый элемент.
                    stateString = InitStates.BotStates.FirstOrDefault().Name;

                    //ToDo изменить состояние(установить первое состояние, отправить приветствие, отправить клавиатуру через метод ChangeCurrentStateAndMakeHop
                    //Изменить состояние в базе данных и сохранить изменения
                    ChangeCurrentChatState(chatId, HopType.RootLevelHope, stateString);
                    //chatState.State = stateString;
                    //db.SaveChanges();
                }
                //Найти в общем пуле состояний состояние с таким же именем.
                StateController stateCtrl = new StateController(stateString);
                return InitStates.GetStateByName(stateCtrl.GetCurrentStateName());
            }

        }

        /// <summary>
        /// Поменять состояние.
        /// </summary>
        /// <param name="bot"></param>
        /// <param name="state"></param>
        /// <param name="hop"></param>
        public static async void ChangeCurrentStateAndMakeHop(TelegramBotClient bot, long chatId, State state, Hop hop)
        {
            if (bot == null ||
                hop == null || 
                string.IsNullOrEmpty(hop.NextStateName)) return;

            //Отправить AnswerOnSuccess.
            if(state != null && string.IsNullOrEmpty(state.AnswerOnSuccess) == false)
            {
                await bot.SendTextMessageAsync(chatId, state.AnswerOnSuccess);
            }

            //Поменяем текущее состояние
            ChangeCurrentChatState(chatId, hop.Type, hop.NextStateName);

            //Когда состояние в базе поменяли, тогда нужно выслать приветствие в новом состоянии и клавиатуру нового состояния.
            State st = InitStates.GetStateByName(hop.NextStateName);

            //Формируем приветственное сообщение (не забываем подключить клавиатуру)
            OutboxMessage outMes = new OutboxMessage(st.IntroductionString);
            if (st.ReplyKeyboard != null)
            {
                outMes.ReplyMarkup = st.ReplyKeyboard;
            }
            else
            {
                outMes.ReplyMarkup = new ReplyKeyboardRemove();
            }

            bot.SendOutboxMessage(chatId, outMes);
            
        }


        /// <summary>
        /// Поменять текущее состояние в базе данных или содать это состояние.
        /// </summary>
        /// <param name="chatId"></param>
        /// <param name="hopType"></param>
        /// <param name="newStateName"></param>
        private static void ChangeCurrentChatState(long chatId, HopType hopType, string newStateName)
        {
            using (BotDbContext db = new BotDbContext(HelperDataBase.DB_OPTIONS))
            {
                //Возьмем значение состояния из базы
                ChatState chatState =
                    (from s in db.ChatState
                        where s.ChatId == chatId
                        select s).FirstOrDefault();

                string stateString = chatState?.State;

                //Если ячейка в базе пустая, то заполним её чем-нибудь и поменяем тип хопа чтобы потом переписать несусветицу.
                if (string.IsNullOrEmpty(stateString?.Trim(' ')))
                {
                    stateString = "___";
                    hopType = HopType.RootLevelHope;
                }

                //Поменять состояние в базе данных.
                StateController stateCtrl = new StateController(stateString);

                //Будем изменять значение состояния в зависимости от типа перехода состояния.
                switch (hopType)
                {
                    case HopType.NextLevelHope:
                        stateCtrl.AddStateAsNextState(newStateName);
                        break;
                    case HopType.CurrentLevelHope:
                        stateCtrl.ChangeCurrentStateName(newStateName);
                        break;
                    case HopType.RootLevelHope:
                        stateCtrl.SetRootState(newStateName);
                        break;
                }

                //получаем измененную строку состояния и сохраняем в базу
                string newStateString = stateCtrl.State;
                chatState.State = newStateString;
                db.SaveChanges();
            }
        }


        /// <summary>
        /// Проверка, есть ли пользователь в базе данных.
        /// </summary>
        /// <param name="db"></param>
        /// <param name="chatId"></param>
        /// <returns></returns>
        public static bool IsUserInDatabase(BotDbContext db, long chatId)
        {
            var user =  
                (from u in db.Users
                where u.ChatId == chatId
                select u)?.FirstOrDefault();

            if (user == null)
            {
                return false;
            }

            return true;
        }


        /// <summary>
        /// Добавить пользователя в базу данных.
        /// Добавить все зависимые сущности, чтобы не проверять их потом на null.
        /// Аккуратно добавить все зависимости, внешние IDшники.
        /// </summary>
        /// <param name="db"></param>
        /// <param name="chatId"></param>
        public static void AddUserToDb(BotDbContext db, long chatId)
        {
            //Создадим пользователя.
            User user = new User()
            {
                ChatId = chatId,
                Role = "user",
            };
            db.Users.Add(user);

            //Создадим сущность UserInfo
            UserInfo uInfo = new UserInfo()
            {
                ChatId = chatId,
                User = user,
                UserId = user.Id,
            };
            db.UserInfo.Add(uInfo);

            //Привяжем UserInfo к пользователю
            user.Info = uInfo;

            //Создадим сущность ChatState
            ChatState chatState = new ChatState()
            {
                ChatId = chatId,
                UserId = user.Id,
                User = user,
            };
            db.ChatState.Add(chatState);

            //Привяжем ChatState к пользователю
            user.ChatState = chatState;

            //Создадим сущность Goal
            Goal goal = new Goal()
            {
                ChatId = chatId,
                User = user,
                UserId = user.Id,
            };
            db.Goals.Add(goal);

            //Привяжем Goal к пользователю
            user.Goal = goal;

            //Сохраняем все изменения и все добавленные сущности.
            db.SaveChanges();

        }


    }
}
