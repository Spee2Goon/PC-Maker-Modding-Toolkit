using System;
using UnityEngine;

namespace PCMaker.Services
{
    public interface IGridSurface
    {
        void Setup();
        
        GridSurfaceType SurfaceType { get; }

        void OnPlayerLook(Vector3 position);
        void OnPlayerStopLook();
        Transform ObjectsParent { get; }
        float SnappingRoundScale { get; }
        Collider[] GetPlacementColliders();
        Collider[] GetSurfaceColliders();
        public void RegisterObjectOnGrid(IPlaceableObjectInstance placedInstance);
        public void UnregisterObjectOnGrid(IPlaceableObjectInstance placedInstance);
        bool HasAnyObjectOnGrid { get; }
        Action<IPlaceableObjectInstance> OnPlaceAnyObject { get; set; }
        void SetGridGuid(string guid);
        string GetGridGuid();
        string GetGridName();
    }
}