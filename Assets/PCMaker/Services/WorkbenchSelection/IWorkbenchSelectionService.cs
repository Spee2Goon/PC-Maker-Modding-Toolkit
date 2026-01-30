namespace PCMaker.Services
{
    public interface IWorkbenchSelectionService
    {
        public void OnHoverOnObject(IWorkbenchSelectableObject obj);
        public void OnStopHoverOnObject(IWorkbenchSelectableObject obj);
        public void OnClickOnObjectSelection(IWorkbenchSelectableObject obj);
        public void OnDisposeSelectableObject(IWorkbenchSelectableObject obj);
        
        void SetContextMenuController(IWorkbenchContextMenuController controller);

        IWorkbenchSelectableObject GetSelectedObject();
        void SetSelectedObject(IWorkbenchSelectableObject selectableObject);
        void SetWorkbenchService(IWorkbenchService workbenchService);

        
        bool HasSelectedObject { get; }
    }
}