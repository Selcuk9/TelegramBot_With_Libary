using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BotLibrary.Classes.Bot;

namespace JustDoItBot.Source.Constants
{
    public class InitStates
    {
        /// <summary>
        /// Возвращаем состояние по имени из BotStates
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static State GetStateByName(string name)
        {
            if (BotStates?.Count == 0) return null;

            foreach (var state in BotStates)
            {
                if (string.Equals(state.Name, name, StringComparison.CurrentCultureIgnoreCase) == true)
                {
                    return state;
                }
            }

            return null;
        }

        public static List<State> BotStates = new List<State>()
        {
            new State("Introduction")
            {
                Hops = new List<Hop>()
                {
                    new Hop(){NextStateName = "Default"},
                    new Hop(){NextStateName = "InputLastName"}
                },

                IntroductionString = "Привет, введи своё имя!",
                HopOnSuccess = new Hop(){NextStateName = "InputLastName"},
                //ReplyKeyboard = Keyboards.InputNameKeyboard.Value,
            },

            new State("InputLastName")
            {
                Hops = new List<Hop>()
                {
                    new Hop(){NextStateName = "Default"},
                    new Hop(){NextStateName = "InputCity"}
                },

                IntroductionString = "Введи фамилию!",
                HopOnSuccess = new Hop(){NextStateName = "Introduction"},

                ReplyKeyboard = Keyboards.InputLastName.Value,
            },

            new State("InputCity")
            {
                Hops = new List<Hop>()
                {
                    new Hop(){NextStateName = "Default"},
                },

                IntroductionString = "Введи город/населенный пункт!",
                HopOnSuccess = new Hop(){NextStateName = "Default"},
            },

            new State("Default")
            {
                Hops = new List<Hop>()
                {
                    new Hop(){NextStateName = "Default"},
                    new Hop(){NextStateName = "InputCity"}
                },

                IntroductionString = "Главное меню",

                ReplyKeyboard = Keyboards.DefaultKeyboard.Value,
            },
        };
    }
}
