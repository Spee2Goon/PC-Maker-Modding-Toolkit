namespace PCMaker.Services
{
    public interface IPCBThermalInterfacesView
    {
        void Setup();
        void Dispose();
        void SetPartInfo(PCPartObject targetPart);
        void EnableView();
        void DisableView();
        void ClearBoardInfo();

        bool isPointerOverScrollView { get; }
    }
}