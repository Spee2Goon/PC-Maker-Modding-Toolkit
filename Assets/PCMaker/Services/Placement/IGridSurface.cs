using System;
using UnityEngine;

namespace PCMaker.Services
{
    public interface IGridSurface
    {
        void Setup();
        
        void OnPlayerLook(Vector3 position);
        
        void OnPlayerStopLook();
        
        Collider[] GetPlacementColliders();
        
        Collider[] GetSurfaceColliders();
        
        public void RegisterObjectOnGrid(IPlaceableObjectInstance placedInstance);
        
        public void UnregisterObjectOnGrid(IPlaceableObjectInstance placedInstance);
        
        void SetGridGuid(string guid);
        
        string GetGridGuid();
        
        string GetGridName();
        
        
        GridSurfaceType SurfaceType { get; }
        
        Transform ObjectsParent { get; }
        
        float SnappingRoundScale { get; }
        
        bool HasAnyObjectOnGrid { get; }
        
        
        Action<IPlaceableObjectInstance> OnPlaceAnyObject { get; set; }
    }
}