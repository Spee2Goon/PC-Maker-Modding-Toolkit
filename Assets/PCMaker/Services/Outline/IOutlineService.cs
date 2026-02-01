using UnityEngine;

namespace PCMaker.Services
{
    public interface IOutlineService
    {
        void SetupOutline(OutlineInstance outlineInstance);
        
        void SetupOutline(OutlineInstance outlineInstance, Vector3[] normalsOverride);
        
        void ClearOutline(OutlineInstance outlineInstance);
    }
}