namespace PCMaker.Services
{
    public interface IPCBRepairService
    {
        IPCBRepairView CreateView();
        void WorkWithBoard(Board board);
        void StopWork();
        
        Board GetCurrentBoard();

        void SetNetViewMode(bool showNets);
        
        bool isWorkingOnBoard { get; }
    }
}