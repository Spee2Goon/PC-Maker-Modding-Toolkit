using System;
using UnityEngine;

namespace PCMaker.Services
{
    public interface IWorkbenchController
    {
        void WorkOn(PCPart part, PCPartObjectSpawnArguments buildArguments, out PCPartObject spawnedObject);
        
        void StopWorkingOnTemporalObject();
        
        void SetCameraActive(bool active);
        
        void SetInteractActive(bool active);
        
        public IPC PlacePCCase(IPCCase PCCase, PCPartObjectSpawnArguments arguments);
        
        PCPartObject GetObjectWorkingOn();
        
        IPC GetPC();
        
        string GetGUID();
        
        void SetCameraCenterPosition(Vector3 worldPosition, bool force = false);
        
        WorkbenchSaveData CaptureState();
        
        void RestoreState(WorkbenchSaveData saveData);
        
        void Dispose();

        
        bool isWorkingOnTemporalObject { get; }
        
        bool isWorkingOnPC { get; }
        
        bool isWorkingOnTemporalObjectOrPC { get; }
        
        float EmptyBenchCameraCenterY { get; }
        
        
        event Action<IWorkbenchController> OnLookOnBench;
        
        event Action<IWorkbenchController> OnStopLookOnBench;
    }
}