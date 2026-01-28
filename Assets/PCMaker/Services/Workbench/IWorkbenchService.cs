using System;

namespace PCMaker.Services
{
    public interface IWorkbenchService
    {
        IWorkbenchController GetActiveBench();
        bool isAnyBenchActive { get; }
        void DisposeWorkbenchesByGameScene();
        void CreateIndicator();
        void DisposeIndicator();
        bool isIndicatorActive();
        
        event Action<IWorkbenchController> OnEnterOnBench;
        event Action<IWorkbenchController> OnExitFromBench;
    }
}