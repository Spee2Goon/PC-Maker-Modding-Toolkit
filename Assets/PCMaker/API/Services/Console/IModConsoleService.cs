using System.Collections.ObjectModel;

namespace PCMaker.ModAPI
{
    public interface IModConsoleService
    {
        void OpenConsole(bool withAnimation);
        void CloseConsole(bool withAnimation);
        void RegisterCommand(IModConsoleCommand command);
        void Print(string message, ConsoleMessageType type);
        void ClearPrintedCommands();
        void RepeatLastEnteredCommand();
        void ExecuteCommand(string input);
        bool isConsoleInputFieldFocused();
        void SetInputFieldFocus(bool focus);
        bool isConsoleOpen();
        ReadOnlyDictionary<string, IModConsoleCommand> GetAllCommands();
    }
}