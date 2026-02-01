namespace PCMaker.Services
{
    public interface ICircleMenuService
    {
        ICircleMenuView CreateCircleMenu();
        
        void OpenCircleMenu(ICircleMenuView menu, CircleMenuAnimationType withAnimation);
        
        void CloseCircleMenu(ICircleMenuView menu, CircleMenuAnimationType withAnimation, bool applySelection = true);
    }
}