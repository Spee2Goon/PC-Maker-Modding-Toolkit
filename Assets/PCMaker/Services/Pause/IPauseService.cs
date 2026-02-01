using System;

namespace PCMaker.Services
{
    public interface IPauseService
    {
        IPauseView CreatePause();
        
        void OpenPause(PauseAnimationType animationType);
        
        void ClosePause(PauseAnimationType animationType);
        
        void OpenPage(PausePageType targetPage, PausePageAnimationType animationType);
        
        PausePageType GetCurrentPageType();
        
        bool isPauseOpen();

        
        event Action OnPauseOpen;
        
        event Action OnPauseClose;
    }
}