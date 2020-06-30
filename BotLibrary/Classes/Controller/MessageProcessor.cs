using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using BotLibrary.Classes;
using BotLibrary.Classes.Bot;
using BotLibrary.Classes.Controller;
using JetBrains.Annotations;


namespace BotLibrary.Classes.Controller
{
    public class MessageProcessor
    {
        private PullMethods Methods;
        private ReflectionInfo Reflection;

        public MessageProcessor([NotNull] ReflectionInfo reflection, [NotNull] PullMethods methods)
        {
            this.Methods = methods;
            this.Reflection = reflection;
        }

        /// <summary>
        /// Обработчик сообщения.
        /// </summary>
        /// <param name="mes"></param>
        public void ProcessCurrentMessage(InboxMessage mes)
        {
            //Проверим пользователя если вдруг его нет в базе, то добавить его.
            this.Methods.CheckUserBeforeMessageProcess(mes.ChatId);

            //запускаем метод из PullMethods для получения текущего состояния пользователя
            State currentChatState = Methods.GetUserCurrentChatState(mes.ChatId);

            //Получим тип состояния.
            string typeName = this.Reflection.StatesNamespace.Trim(' ', '.') + "." + currentChatState.Name;
            Type type = this.Reflection.Assembly.GetType(typeName, false);
            if (type == null)
            {
                throw new NullReferenceException($"Не найден тип состояния [{typeName}]");
            }

            //Создадим экземпляр класса состояния и вызовем метод обработки сообщения у экземпляра класса
            object instance = Activator.CreateInstance(type, currentChatState);

            //ProcessMessage - название абстрактного метода в классе BaseState
            MethodInfo method = type.GetMethod("ProcessMessage");

            //Запускаем метод обработки входящего сообщения, получаем переход после обработки. Переход может быть нулевым.
            Hop hop = method?.Invoke(instance, new object[] {mes.Bot, mes}) as Hop;

            if (hop != null && string.IsNullOrEmpty(hop?.NextStateName?.Trim(' ')) == false)
            {
                this.Methods.ChangeCurrentChatState(mes.Bot, mes.ChatId, currentChatState, hop);
            }
        }
    }
}