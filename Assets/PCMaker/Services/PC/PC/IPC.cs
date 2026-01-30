namespace PCMaker.Services
{
    public interface IPC
    {
        void Dispose();
        
        void RegisterSelectableObject(IWorkbenchSelectableObject selectable);
        void UnregisterSelectableObject(IWorkbenchSelectableObject selectable);

        IPCCaseObject GetCase();
        void SetCase(IPCCaseObject caseObject);
        
        void SetAllHoverCollidersActive(bool active);
    }
}