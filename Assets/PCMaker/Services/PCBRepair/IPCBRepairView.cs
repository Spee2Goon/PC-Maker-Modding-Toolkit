using System;

namespace PCMaker.Services
{
    public interface IPCBRepairView
    {
        void Setup();
        void Dispose();
        void SetPCBInfo(Board board);
        void ClearPCBInfo();
        void EnableView();
        void DisableView();
        void EnableInspectionMode();
        void DisableInspectionMode();

        bool isPointerOverScrollView { get; }
        
        event Action OnRequireInspectionMode;
        event Action OnRequireShowPins;
        event Action OnRequireHidePins;
    }
}