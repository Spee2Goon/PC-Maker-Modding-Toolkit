using System;
using UnityEngine;

namespace PCMaker.Services
{
    public class MicroElement : MonoBehaviour, IWorkbenchSelectableObject
    {
        public string[] Names;
        public string ContextMenuName;
        public bool CanBeDismantled = true;

        public BoardMicroElementSlot ParentSlot;
        
        [SerializeField]
        private OutlineInstance HoverOutline;

        [SerializeField]
        private Interactable HoverInteract;
        
        [SerializeField]
        private Interactable ClickInteract;

        public string SelectableObjectName => Names[0];

        private Board _parentBoard;
        private IWorkbenchService _workbenchService;
        private IPCBRepairService _repairService;
        private IWorkbenchSelectionService _workbenchSelectionService;
        
        public void Setup(Board parentBoard, IWorkbenchService workbenchService, IPCBRepairService repairService, IWorkbenchSelectionService workbenchSelectionService)
        {
            _parentBoard = parentBoard;
            _workbenchService = workbenchService;
            _repairService = repairService;
            _workbenchSelectionService = workbenchSelectionService;
            
            if (HoverOutline != null)
            {
                OnMouseHover += parentBoard.onHoverOnMicroElement;
                OnMouseStopHover += parentBoard.onStopHoverOnMicroElement;
                OnClickOnSelection += parentBoard.onClickOnMicroElement;
                OnDisposeSelectableObject += parentBoard.onDisposeSelectableMicroElement;
            }
        }

        public void OnTakeFromPool()
        {
            SetupHoverableObject();
            SetHoverableCollidersActive(false);
        }

        public void onReturnToPool()
        {
            if (_workbenchSelectionService.GetSelectedObject() == this as IWorkbenchSelectableObject)
            {
                _workbenchSelectionService.SetSelectedObject(null);
            }
            
            DisposeHoverableObject();
        }

        public void Dispose()
        {
            if (HoverOutline != null)
            {
                OnMouseHover -= _parentBoard.onHoverOnMicroElement;
                OnMouseStopHover -= _parentBoard.onStopHoverOnMicroElement;
                OnClickOnSelection -= _parentBoard.onClickOnMicroElement;
                OnDisposeSelectableObject -= _parentBoard.onDisposeSelectableMicroElement;
            }
        }
        
        public void SetupHoverableObject()
        {
            if (HoverInteract != null)
            {
                //Debug.Log($"[MicroElement] ({Names[0]}) Setup Hoverable Object", gameObject);
                
                HoverInteract.OnInteract += onMouseHover;
                ClickInteract.OnInteract += onClickOnSelection;
                TickRateManager.OnTickLateUpdate += UpdateHoverRequrement;
            }
        }
        
        public void DisposeHoverableObject()
        {
            if (HoverInteract != null)
            {
                //Debug.Log($"[MicroElement] ({Names[0]}) Dispose Hoverable Object", gameObject);
                
                HoverInteract.OnInteract -= onMouseHover;
                ClickInteract.OnInteract -= onClickOnSelection;
                TickRateManager.OnTickLateUpdate -= UpdateHoverRequrement;
            }
        }

        public void OnSelect()
        {
            
        }

        public void SetOutlineActive(bool active)
        {
            Debug.Log($"[MicroElement] ({Names[0]}) Set Outline Active: {active}", gameObject);
            
            if (active)
            {
                HoverOutline.SetupOutline();
            }
            else
            {
                HoverOutline.ClearOutline();
            }
        }

        public void FillWorkbenchContextMenu(IWorkbenchContextMenuPanel panel)
        {
            panel.Build(new WorkbenchContextMenuOptions()
            {
                DeviceName = ContextMenuName,
                AllowFlipObject = true,
                AllowSpecifications = true,
                AllowDemontage = CanBeDismantled,
                AllowFocus = true,
            });
            
            panel.OnClose +=
                delegate
                {
                    panel.OnSelectDemontage -= OnSelectDemontagePartOption;
                    panel.OnSelectFlip -= OnSelectFlipPartOption;
                };

            panel.OnSelectDemontage += OnSelectDemontagePartOption;
            panel.OnSelectFlip += OnSelectFlipPartOption;
            panel.OnSelectFocus += OnSelectFocusPartOption;
        }

        private void OnSelectDemontagePartOption()
        {
            //Debug.Log("DemontagePart", gameObject);
        }
        private void OnSelectFlipPartOption()
        {
            //Debug.Log("FlipPart", gameObject);
            _repairService.GetCurrentBoard().MicroElementRequreFlip();
        }
        private void OnSelectFocusPartOption()
        {
            //Debug.Log("FocusOnPart", gameObject);
            _workbenchService.GetActiveBench().SetCameraCenterPosition(transform.position);
        }
        
        public void SetHoverableCollidersActive(bool active)
        {
            HoverInteract?.gameObject.SetActive(active);
            ClickInteract?.gameObject.SetActive(active);
        }
        
        private void UpdateHoverRequrement()
        {
            if (_isMouseOverPartInCurrentFrame)
            {
                _isMouseOverPartInCurrentFrame = false;
            }
            else if (_isMouseOverPart)
            {
                _isMouseOverPart = false;
                OnMouseStopHover?.Invoke(this);
            }
        }

        public BoardMicroElementSlot GetParentSlot() => ParentSlot;
        
        #if UNITY_EDITOR
        
        [Obsolete("Editor only")]
        public void SetClickInteract(Interactable interact) => ClickInteract = interact;
        [Obsolete("Editor only")]
        //public bool HasHoverable() => HoverInteract != null;
        
        #endif
        
        public event Action<IWorkbenchSelectableObject> OnMouseHover;
        public event Action<IWorkbenchSelectableObject> OnMouseStopHover;
        public event Action<IWorkbenchSelectableObject> OnClickOnSelection;
        public event Action<IWorkbenchSelectableObject> OnDisposeSelectableObject;
        
        protected bool _isMouseOverPart;
        protected bool _isMouseOverPartInCurrentFrame;
        
        public virtual void onMouseHover()
        {
            //Debug.Log("[MicroElement] OnMouseHover", gameObject);
            
            _isMouseOverPart = true;
            _isMouseOverPartInCurrentFrame = true;
            
            
            OnMouseHover?.Invoke(this);
        }
        
        public virtual void onClickOnSelection() { OnClickOnSelection?.Invoke(this); }
    }
}