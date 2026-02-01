using System;
using UnityEngine;

namespace PCMaker.Services
{
    public interface IWorkbenchContextMenuPanel
    {
        void Setup(Transform blurParent, ILocalizationService localizationService);
        
        void Build(WorkbenchContextMenuOptions options);
        
        
        event Action OnSelectDemontage;
        
        event Action OnSelectFlip;
        
        event Action OnSelectWorkWithBoard;
        
        event Action OnSelectWorkWithThermalInterface;
        
        event Action OnSelectReplace;
        
        event Action OnSelectSpecifications;
        
        event Action OnSelectFocus;
        
        event Action OnClose;
    }
}