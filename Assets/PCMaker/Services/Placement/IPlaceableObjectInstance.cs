using System;
using System.Collections.Generic;
using UnityEngine;

namespace PCMaker.Services
{
    public interface IPlaceableObjectInstance
    {
        void Initialize(IPlaceableObject placeableObject);
        void SetPhysicalCollidersActive(bool active);
        void SetModelType(PlacebleObjectModelType modelType);
        bool CanBeDestructed();
        void SetDestructionHighlight(bool highlight);
        bool Destruct();
        void Dispose();
        Dictionary<string, string> CaptureState();
        void RestoreState(Dictionary<string, string> state);
        string GetParentSurfaceGUID();
        string GetSelfGUID();
        IGridSurface GetParentSurface();
        void SetParentSurface(IGridSurface surface);
        
        void ApplyInstall();

        event Action OnApplyInstallObject;
        event Action OnDestroyObject;
        
        IPlaceableObject objectInfo { get; set; }
        Transform transform { get; }        
        GameObject gameObject { get; }
    }
}