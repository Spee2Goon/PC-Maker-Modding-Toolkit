using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace PCMaker.Services
{
    public interface IPlaceableObject
    {
        Task<bool> LoadResourcesAsync();
        
        void UnloadResources();
        
        public void Initialize();
        
        void BeginPlacement();
        
        void BreakPlacement();
        
        bool CanBePlacedHere(IGridSurface gridSurface);
        
        bool UpdatePlacementPreview(IGridSurface gridSurface, Vector3 position);
        
        void HidePlacementPreview();
        
        bool ConfirmPlacement(out IPlaceableObjectInstance spawnedInstance);
        
        IPlaceableObjectInstance RestoreInstanceByState(Dictionary<string, string> state);
        
        
        string[] SaveKeys { get; }
        
        Sprite Icon { get; }
        
        float Price { get; }
        
        bool AllowLoad { get; }
        
        float MaxPlaceDistance { get; }
        
        PlaceableObjectTag[] Tags { get; }
        
        bool AllowAlternativePlaceing { get; }
        
        PlacementCategoryType Category { get; }
        
        Color MainObjectColor { get; }
    }
}