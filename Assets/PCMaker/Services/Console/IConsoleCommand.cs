namespace PCMaker.Services
{
    public interface IConsoleCommand
    {
        string Command { get; }
        
        string Description { get; }
        
        bool Execute(string[] args, out string exeption);
    }
}