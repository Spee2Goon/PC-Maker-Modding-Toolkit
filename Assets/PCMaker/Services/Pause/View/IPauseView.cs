namespace PCMaker.Services
{
    public interface IPauseView
    {
        void EnablePause(PauseAnimationType animationType);
        void DisablePause(PauseAnimationType animationType);
        void OpenPage(PausePageType targetPage, PausePageAnimationType animationType);
        void DisposeView();
    }
}