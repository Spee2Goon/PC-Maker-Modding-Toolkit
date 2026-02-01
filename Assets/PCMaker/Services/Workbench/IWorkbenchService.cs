using System;
using UnityEngine;

namespace PCMaker.Services
{
    public interface IWorkbenchService
    {
        void RegisterBench(IWorkbenchController bench);
        
        void UnregisterBench(IWorkbenchController bench);
        
        void EnterOnBench(IWorkbenchController bench);
        
        void ExitFromBench();
        
        IWorkbenchController GetActiveBench();
        
        void SetView(IWorkbenchView view);
        
        IWorkbenchView GetView();
        
        void SetSelectionService(IWorkbenchSelectionService selectionService);
        
        void SetPartsInstallService(IPCPartsInstallService partsInstallService);
        
        void SetRepairService(IPCBRepairService repairService);
        
        void DisposeWorkbenchesByGameScene();
        
        void CreateIndicator();
        
        void DisposeIndicator();
        
        bool isIndicatorActive();
        
        void ShowIndicatorAt(Vector3 position, IGridSurface surface);
        
        void DisableIndicator();
        
        
        bool isAnyBenchActive { get; }
        
        
        event Action<IWorkbenchController> OnEnterOnBench;
        
        event Action<IWorkbenchController> OnExitFromBench;
    }
}