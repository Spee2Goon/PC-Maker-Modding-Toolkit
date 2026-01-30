namespace PCMaker.Services
{
    public interface IWorkbenchView
    {
        void Setup();
        void RefreshView();
        void Enable();
        void Disable();

        IWorkbenchContextMenuController GetContextMenuController();
    }
}