using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace PCMaker.Services
{
    public interface IPlaceableObject
    {
        string SaveKey { get; }
        Sprite Icon { get; }
        float Price { get; }
        bool AllowLoad { get; }
        float MaxPlaceDistance { get; }
        PlaceableObjectTag[] Tags { get; }
        bool AllowAlternativePlaceing { get; }
        PlacementCategoryType Category { get; }
        Color MainObjectColor { get; }
        
        
        Task<bool> LoadResourcesAsync();
        void UnloadResources();
        public void Initialize();
        void BeginPlacement();
        bool CanBePlacedHere(IGridSurface gridSurface);
        bool UpdatePlacementPreview(IGridSurface gridSurface, Vector3 position);
        void HidePlacementPreview();
        bool ConfirmPlacement(out IPlaceableObjectInstance spawnedInstance);
        void BreakPlacement();
        
        
        IPlaceableObjectInstance RestoreInstanceByState(Dictionary<string, string> state);
    }
}