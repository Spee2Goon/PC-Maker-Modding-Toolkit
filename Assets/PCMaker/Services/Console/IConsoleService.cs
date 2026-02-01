using System;
using System.Collections.ObjectModel;
using UnityEngine;

namespace PCMaker.Services
{
    public interface IConsoleService
    {
        IConsoleView CreateConsole(Camera mainCamera);
        
        bool IsConsoleViewCreated();
        
        IConsoleView GetConsoleView();
        
        void ClearViewInfo();
        
        void OpenConsole(ConsoleAnimationType animationType);
        
        void CloseConsole(ConsoleAnimationType animationType);
        
        bool isConsoleOpen();
        
        void Print(string message);
        
        void Print(string message, ConsoleMessageType type);
        
        void SpawnConsoleMessage(ConsoleMessage message);
        
        void ClearPrintedCommands();
        
        void RepeatLastEnteredCommand();
        
        void RegisterCommand(IConsoleCommand command);
        
        void ExecuteCommand(string input);
        
        bool isConsoleInputFieldFocused();
        
        bool isPointerOverConsole();
        
        void SetInputFieldFocus(bool focus);
        
        void SetInputFieldText(string text);
        
        ReadOnlyDictionary<string, IConsoleCommand> GetAllCommands();

        bool TryParseBool(string input, out bool value);

        
        event Action OnPintAnyText;
        
        event Action<IConsoleCommand> OnExecuteAnyCommand;
    }
}