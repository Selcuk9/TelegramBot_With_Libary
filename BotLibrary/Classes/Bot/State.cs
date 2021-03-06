﻿using System;
using System.Collections.Generic;
using System.Text;
using JetBrains.Annotations;
using Telegram.Bot.Types.ReplyMarkups;

namespace BotLibrary.Classes.Bot
{
    /// <summary>
    /// Класс состояния бота.
    /// Описывает определенное состояние;
    /// </summary>
    public class State
    {
        public int Id { get; set; }
        public string Name { get; set; }
        
        [CanBeNull] public string IntroductionString { get; set; }
        [CanBeNull] public string AnswerOnSuccess { get; set; }
        [CanBeNull] public string AnswerOnFail { get; set; }

        [CanBeNull]
        public IReplyMarkup ReplyKeyboard { get; set; }

        [CanBeNull]
        public List<Hop> Hops;

        [CanBeNull] public Hop HopOnSuccess;
        [CanBeNull] public Hop HopOnFailure;

        /// <summary>
        /// Конструктор
        /// </summary>
        private State()
        {
            //ToDo Сделать генерацию ID по-лучше
            this.Id = this.GetHashCode();

            this.Hops = new List<Hop>();
        }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="name"></param>
        public State(string name):this()
        {
            this.Name = name;
        }


    }
}
