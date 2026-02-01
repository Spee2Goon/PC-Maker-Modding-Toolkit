using System;

namespace PCMaker.Services
{
    public interface IGameSceneDisposeService
    {
        void Dispose();
        
        string[] GetLog();

        event Action OnDispose;
    }
}