using System;
using UnityEngine;

namespace PCMaker.Services
{
    public interface IWorkbenchContextMenuController
    {
        IWorkbenchContextMenuPanel ShowMenu(Vector3 position, IWorkbenchSelectableObject targetObject);
        void HideMenu();
        
        event Action<IWorkbenchContextMenuPanel> OnShowContextMenu;
        event Action<IWorkbenchContextMenuPanel> OnHideContextMenu;
    }
}