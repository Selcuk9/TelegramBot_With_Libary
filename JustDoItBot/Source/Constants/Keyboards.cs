using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BotLibrary.Classes;
using Telegram.Bot.Types.ReplyMarkups;

namespace JustDoItBot.Source.Constants
{
    public class Keyboards
    {
        public static MarkupWrapper<ReplyKeyboardMarkup> DefaultKeyboard = new MarkupWrapper<ReplyKeyboardMarkup>(true)
            .NewRow()
            .Add("1")
            .Add("2")
            .Add("3");


        public static MarkupWrapper<ReplyKeyboardMarkup> InputNameKeyboard =
            new MarkupWrapper<ReplyKeyboardMarkup>(true)
                .NewRow()
                .Add("InputName");

        public static MarkupWrapper<ReplyKeyboardMarkup> InputLastName = new MarkupWrapper<ReplyKeyboardMarkup>(true)
            .NewRow()
            .Add("InputLastName");

       
    }
}
