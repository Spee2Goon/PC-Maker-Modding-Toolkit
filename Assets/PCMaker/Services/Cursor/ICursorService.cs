namespace PCMaker.Services
{
    public interface ICursorService
    {
        void RequestCursorShowing(object requester);
        
        void ReleaseCursorShowing(object requester);
        
        void RequestCursorFree(object requester);
        
        void ReleaseCursorFree(object requester);
        
        void UpdateCursorShowing();
        
        void UpdateCursorLookState();
        
        void UpdateCursorView();
        
        bool CanScroll();
        
        string[] GetLog();
    }
}