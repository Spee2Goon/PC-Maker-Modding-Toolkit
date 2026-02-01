namespace PCMaker.Services
{
    public interface ICircleMenuView
    {
        void EnableCircleMenu(CircleMenuAnimationType animationMode);
        
        void DisableCircleMenu(CircleMenuAnimationType animationMode, bool applySelection = true);
        
        bool IsCircleMenuOpen();
        
        bool IsCircleMenuClosed();
        
        void Dispose();
    }
}