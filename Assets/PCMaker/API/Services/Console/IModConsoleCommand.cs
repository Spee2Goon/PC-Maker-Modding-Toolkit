namespace PCMaker.ModAPI
{
    public interface IModConsoleCommand
    {
        string Command { get; }
        string Description { get; }
        bool Execute(string[] args, out string exeption);
    }
}