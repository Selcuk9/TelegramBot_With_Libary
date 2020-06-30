using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace BotLibrary.Classes.StateControl
{
    /// <summary>
    /// Контроллер состояний.
    /// </summary>
    public class StateController
    {
        private List<String> ListStates;

        private string _state;
        
        /// <summary>
        /// Строка состояния.
        /// Эта строка будет в виде ххх/хх/хххх/ххх
        /// Таким образом мы можем хранить предыдущие состояния.
        /// Проще осуществлять переход на предыдущие состояния, контроль состояний
        /// </summary>
        public string State
        {
            get
            {
                return _state;
            }
            private set { _state = value; }
        }

        /// <summary>
        /// Конструктор
        /// </summary>
        public StateController()
        {
            this.ListStates = new List<string>();
        }

        public StateController(string currentState):this()
        {
            currentState = this.ProcessStateString(currentState);
            this.State = "/" + currentState;
            this.SetList();
        }

        private void SetList()
        {
            this.ListStates = this.State?.Trim('/')?.Split('/')?.ToList();
        }

        /// <summary>
        /// Проверка строки состояний на пустую строку.
        /// </summary>
        /// <returns></returns>
        private bool IsEmptyStateString()
        {
            if (this.ListStates == null || this.ListStates.Count == 0)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Приведем строку в единый вид.
        /// </summary>
        /// <param name="stateToProcess"></param>
        /// <returns></returns>
        private string ProcessStateString(string stateToProcess)
        {
            var res =  stateToProcess.Trim('/', ' ');

            if (string.IsNullOrEmpty(res))
            {
                throw new Exception("Передана пустая строка состояния!");
            }

            return res;
        }

        /// <summary>
        /// Устанавливает состояние, как корневое.
        /// </summary>
        /// <param name="rootState">имя корневого параметра</param>
        public void SetRootState(string rootState)
        {
            rootState = ProcessStateString(rootState);

            this.State = "/" + rootState;

            this.ListStates.Clear();
            this.ListStates.Add(rootState);
        }

        /// <summary>
        /// добавляем состояние к строке состояний, как состояние следующего уровня.
        /// </summary>
        /// <param name="stateNextLevel"></param>
        public void AddStateAsNextState(string stateNextLevel)
        {
            stateNextLevel = ProcessStateString(stateNextLevel);

            this.State += "/" + stateNextLevel;
        }
       
        /// <summary>
        /// удаляем текущее состояние, или переходим к предыдущему состоянию
        /// </summary>
        public void RemoveCurrentState()
        {
            if(IsEmptyStateString() == true) return;

            var lastSlashIndex = this.State.LastIndexOf('/');
            this.State = this.State.Remove(lastSlashIndex);

            this.ListStates?.Remove(this.ListStates?.LastOrDefault());
        }

        /// <summary>
        /// Поменять имя текущего состояния.
        /// </summary>
        public void ChangeCurrentStateName(string stateName)
        {
            stateName = this.ProcessStateString(stateName);

            if(IsEmptyStateString() == false)
            {
                this.RemoveCurrentState();
            }

            this.AddStateAsNextState(stateName);
        }

        /// <summary>
        /// Возвращаем название текущего состояния
        /// </summary>
        /// <returns></returns>
        public string GetCurrentStateName()
        {
            if (IsEmptyStateString() == true) return null;

            return this.ListStates.LastOrDefault();

        }


        /// <summary>
        /// Показать на каком уровне текущее состояние
        /// т.е. ааа/ббб/ххх (текущий уровень 3)
        /// </summary>
        /// <returns></returns>
        public int GetCurrentStateLevel()
        {
            if (IsEmptyStateString() == true)
            {
                return 0;
            }

            return this.ListStates.Count;
        }

    }
}
