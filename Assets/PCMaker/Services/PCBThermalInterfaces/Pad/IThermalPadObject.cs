using System;
using UnityEngine;

namespace PCMaker.Services
{
    public interface IThermalPadObject
    {
        PCPartObject GetPartObject();
        
        void SetupThermalPad(Vector2 sizeMM, SO_OutlineNormalsData customPadNormals);

        
        event Action OnEnablePadOutline;
        
        event Action OnDisablePadOutline;
    }
}