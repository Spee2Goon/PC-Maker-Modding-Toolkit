namespace PCMaker.Services
{
    public interface IWorkbenchCondition
    {
        bool CanEnterOnWorkbench { get; }
        bool CanExitFromBench { get; }
    }
}