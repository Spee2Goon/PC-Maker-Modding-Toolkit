using System;

namespace PCMaker.Services
{
    public interface IWorkbenchSelectableObject
    {
        void SetupHoverableObject();
        void SetOutlineActive(bool active);
        void FillWorkbenchContextMenu(IWorkbenchContextMenuPanel panel);
        void SetHoverableCollidersActive(bool active);

        void OnSelect();
        
        string SelectableObjectName { get; }
        
        event Action<IWorkbenchSelectableObject> OnMouseHover;
        event Action<IWorkbenchSelectableObject> OnMouseStopHover;
        event Action<IWorkbenchSelectableObject> OnClickOnSelection;
        event Action<IWorkbenchSelectableObject> OnDisposeSelectableObject;
    }
}