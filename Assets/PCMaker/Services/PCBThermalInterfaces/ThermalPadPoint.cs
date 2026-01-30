using System;
using UnityEngine;

namespace PCMaker.Services
{
    public class ThermalPadPoint : MonoBehaviour
    {
        [SerializeField]
        protected Sprite IconWithoutPad;

        [SerializeField]
        protected Sprite IconWithPad;
        
        [SerializeField]
        protected Sprite IconWithSelectedPad;

        [SerializeField]
        protected SO_OutlineNormalsData CustomPadNormals;

        [Space]

        [SerializeField]
        protected Vector2 PadSize;
        
        public event Action OnPadStateChange;

        
        public virtual void Setup(PCPartObject parentObject)
        {
            
        }

        public virtual void Dispose()
        {
            
        }

        public virtual void ProcessPad(IThermalPadObject pad)
        {
            
        }
        
        public virtual void SetPad(IThermalPadObject pad)
        {
            
        }
        
        public virtual bool CanConnectThermalPad(IThermalPad thermalPad)
        {
            return true;
        }

        public virtual bool CanUseThermalPadsPack(IThermalPadPack pack)
        {
            return false;
        }
        

        public virtual Sprite GetActualIcon()
        {
            return IconWithoutPad;
        }
        
        public bool HasPad() => false;
        public IThermalPadObject GetPad() => null;
        public Vector2 GetPadSize() => PadSize;
    }
}