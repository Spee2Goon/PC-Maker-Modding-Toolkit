using UnityEngine;

namespace PCMaker.Services
{
    public interface IConsoleView
    {
        public void InitializeView(Camera MainCamera);
        public void EnableConsole(ConsoleAnimationType animationType);
        public void DisableConsole(ConsoleAnimationType animationType);
        public void SetConsoleTransform(Vector3 localPosition, Vector2 sizeDelta, Vector2 anchoredPosition);
        public void SetInputFieldFocus(bool focus);
        public void SetInputFieldText(string text);
        public IMessageText PrintTextOnView(ConsoleMessage message, int fontSize);
        public void MoveScrollToBottom();
        public void PlayOpenAnimation();
        public void ClearInputField();
        public void ClearPrintedMessages();
        public bool isInputFieldFocused();
        public bool isPointerOverConsole();
        
        
        public GameObject gameObject { get; }
    }
}