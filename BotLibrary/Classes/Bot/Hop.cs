using System;
using System.Collections.Generic;
using System.Text;
using JetBrains.Annotations;

namespace BotLibrary.Classes.Bot
{
    /// <summary>
    /// Содержит информацию о переходе к следующему состоянию.
    /// </summary>
    public class Hop
    {
        [CanBeNull] public string NextStateName { get; set; }
        public HopType Type = HopType.RootLevelHope;
    }

    public enum HopType
    {
        RootLevelHope,
        NextLevelHope,
        CurrentLevelHope
    }
}