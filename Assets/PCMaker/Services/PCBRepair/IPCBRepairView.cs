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
        
        event Action OnRequireInspectionMode;
        
        event Action OnRequireShowPins;
        
        event Action OnRequireHidePins;
        
        
        bool isPointerOverScrollView { get; }
    }
}