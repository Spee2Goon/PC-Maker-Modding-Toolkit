using System;
using UnityEngine;

namespace PCMaker.Services
{
    [Serializable]
    public class ConsoleMessage
    {
        public ConsoleMessage(string textMessage, ConsoleMessageType type)
        {
            this.messageText = textMessage;
            this.messageType = type;
            this.messageColor = Color.white;
            this.messagePrintTime = DateTime.Now;

            switch (this.messageType)
            {
                case ConsoleMessageType.LOG:
                    typeColor = Color.white;
                    break;
                case ConsoleMessageType.WARNING:
                    typeColor = Color.orange;
                    break;case ConsoleMessageType.ERROR:
                    typeColor = Color.red;
                    break;
                case ConsoleMessageType.SERVICE:
                    typeColor = Color.blue;
                    break;
            }
        }

        public string messageText { get; }
        public Color messageColor { get; }
        public ConsoleMessageType messageType { get; }
        public DateTime messagePrintTime { get; }
        public Color typeColor { get; }
    }
}