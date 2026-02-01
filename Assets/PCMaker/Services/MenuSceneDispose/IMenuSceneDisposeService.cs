using System;

namespace PCMaker.Services
{
    public interface IMenuSceneDisposeService
    {
        void Dispose();
        
        string[] GetLog();

        event Action OnDispose;
    }
}