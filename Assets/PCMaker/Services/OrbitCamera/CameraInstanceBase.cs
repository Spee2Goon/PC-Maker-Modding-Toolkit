using System;
using UnityEngine;

namespace PCMaker.Services
{
    public abstract class CameraInstanceBase : MonoBehaviour
    {
        public void Create() { }
        public void Dispose() { }

        public abstract OrbitCameraSettings GetCameraSettings();
        public event Action OnDestroyInstance;
    }
}