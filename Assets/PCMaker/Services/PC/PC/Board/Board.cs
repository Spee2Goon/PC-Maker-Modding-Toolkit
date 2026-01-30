using System;
using UnityEngine;
using UnityEngine.Profiling;
using VContainer;

namespace PCMaker.Services
{
    public class Board : MonoBehaviour, IWorkbenchSelectableObject
    {
        [SerializeField]
        private Transform CameraCenter;
        
        [SerializeField]
        protected OutlineInstance[] Outlines;

        [Header("Selection")]
        
        [SerializeField]
        protected Interactable[] HoverInteractables;

        [SerializeField]
        protected Interactable[] ClickInteractables;

        [Header("MicroElements")]
        
        [SerializeField]
        private BoardMicroElementSlot[] Slots;

        public NetPathBuilder NetPathBuilder;
        
        [Header("Pins")]
        [SerializeField]
        private Transform PinsParent;

        [SerializeField]
        private BoardPin[] Pins;

        [SerializeField]
        private BoardMicroElementSlot[] ConnectorsSlots;
        
        [SerializeField]
        private ConnectorElement[] Connectors;

        [Inject]
        public IWorkbenchService workbenchService;

        [Inject]
        public IPCBRepairService pcbRepairService;

        [Inject]
        public IWorkbenchSelectionService workbenchSelectionService;


        protected IWorkbenchContextMenuController _contextMenuController;
        protected PCPartObject _parentObject;
        protected bool _requireHighlighted;
        protected bool _isHighlighted;
        protected bool _isDisposed;
        protected bool _isPinsShowing;

        public event Action OnTakeFromPool;
        public event Action OnReturnToPool;

        public virtual void OnCreateInPool()
        {
            _isPinsShowing = true;
            HidePins();
            
            IWorkbenchService workbenchService = GameGlobalContext.LoaderSceneResolver.Resolve<IWorkbenchService>();
            IPCBRepairService repairService = GameGlobalContext.LoaderSceneResolver.Resolve<IPCBRepairService>();
            IWorkbenchSelectionService workbenchSelectionService = GameGlobalContext.LoaderSceneResolver.Resolve<IWorkbenchSelectionService>();
            
            for (int i = 0; i < Slots.Length; i++)
            {
                Slots[i].ElementInSlot.Setup(this, workbenchService, repairService, workbenchSelectionService);
            }
        }

        public virtual void onTakeFromPool()
        {
            Profiler.BeginSample("OnTakeFromPool");
            
            Debug.Log("[Board] On Take From Pool", gameObject);
            
            SetupHoverableObject();
            
            
            for (int i = 0; i < Slots.Length; i++)
            {
                Slots[i].ElementInSlot.OnTakeFromPool();
            }
            
            OnTakeFromPool?.Invoke();
            
            
            Profiler.EndSample();
        }

        public virtual void SetupBoardForPCPart(PCPartObject parentObject)
        {
            _parentObject = parentObject;
            for (int i = 0; i < Connectors.Length; i++)
            {
                Connectors[i].Setup(parentObject);
            }
        }
        

        public virtual void onReturnToPool()
        {
            Profiler.BeginSample("OnReturnToPool");
            
            Debug.Log($"[Board] ({SelectableObjectName}) On Return To Pool", gameObject);
            
            _parentObject = null;
            
            HidePins();
            
            DisposeHoverableObject();
            
            for (int i = 0; i < Slots.Length; i++)
            {
                Slots[i].ElementInSlot.onReturnToPool();
            }
            
            OnReturnToPool?.Invoke();
            
            Profiler.EndSample();
        }

        public virtual void Dispose()
        {
            for (int i = 0; i < Slots.Length; i++)
            {
                Slots[i].ElementInSlot.Dispose();
            }
            
            _isDisposed = true;
        }

        public ConnectorElement[] GetAllConnectors() => Connectors;
        public BoardMicroElementSlot[] GetAllSlots() => Slots;
        
        public BoardMicroElementSlot[] GetAllConnectorsSlots() => ConnectorsSlots;
        public PCPartObject GetParentObject() => _parentObject;
        

        private void OnDestroy()
        {
            if (!_isDisposed)
            {
                Debug.LogWarning($"Board destroyed, type: {GetType()}");
            }
        }
        
        public virtual string SelectableObjectName => "Basic board";

        public virtual void SetupHoverableObject()
        {
            Debug.Log($"[Board] Setup Hoverable Object: {GetType()}");

            _contextMenuController = workbenchService.GetView().GetContextMenuController();

            for (int i = 0; i < HoverInteractables.Length; i++)
            {
                HoverInteractables[i].OnInteract += onMouseHover;
            }

            for (int i = 0; i < ClickInteractables.Length; i++)
            {
                ClickInteractables[i].OnInteract += onClickOnSelection;
            }

            TickRateManager.OnTickLateUpdate += UpdateHoverRequrement;

            OnMouseHover += workbenchSelectionService.OnHoverOnObject;
            OnMouseStopHover += workbenchSelectionService.OnStopHoverOnObject;
            OnClickOnSelection += workbenchSelectionService.OnClickOnObjectSelection;
            OnDisposeSelectableObject += workbenchSelectionService.OnDisposeSelectableObject;
            
            OnHoverOnMicroElement += workbenchSelectionService.OnHoverOnObject;
            OnStopHoverOnMicroElement += workbenchSelectionService.OnStopHoverOnObject;
            OnClickOnMicroElement += workbenchSelectionService.OnClickOnObjectSelection;
            OnDisposeSelectableMicroElement += workbenchSelectionService.OnDisposeSelectableObject;
        }

        public void DisposeHoverableObject()
        {
            Debug.Log($"[Board] Dispose Hoverable Object: {GetType()}");
            
            _contextMenuController = null;
            
            for (int i = 0; i < HoverInteractables.Length; i++)
            {
                HoverInteractables[i].OnInteract -= onMouseHover;
            }

            for (int i = 0; i < ClickInteractables.Length; i++)
            {
                ClickInteractables[i].OnInteract -= onClickOnSelection;
            }
            
            TickRateManager.OnTickLateUpdate -= UpdateHoverRequrement;
            
            SetOutlineActive(false);
            
            
            OnMouseHover -= workbenchSelectionService.OnHoverOnObject;
            OnMouseStopHover -= workbenchSelectionService.OnStopHoverOnObject;
            OnClickOnSelection -= workbenchSelectionService.OnClickOnObjectSelection;
            OnDisposeSelectableObject -= workbenchSelectionService.OnDisposeSelectableObject;
            
            
            OnHoverOnMicroElement -= workbenchSelectionService.OnHoverOnObject;
            OnStopHoverOnMicroElement -= workbenchSelectionService.OnStopHoverOnObject;
            OnClickOnMicroElement -= workbenchSelectionService.OnClickOnObjectSelection;
            OnDisposeSelectableMicroElement -= workbenchSelectionService.OnDisposeSelectableObject;
            
            
            if (workbenchSelectionService.GetSelectedObject() == this as IWorkbenchSelectableObject)
            {
                workbenchSelectionService.SetSelectedObject(null);
            }
        }
        
        public void SetHoverableCollidersActive(bool active)
        {
            Debug.Log($"[Board] Set Hoverable Colliders Active: {active}");
            
            for (int i = 0; i < HoverInteractables.Length; i++)
            {
                HoverInteractables[i].gameObject.SetActive(active);
            }

            for (int i = 0; i < ClickInteractables.Length; i++)
            {
                ClickInteractables[i].gameObject.SetActive(active);
            }
        }

        protected virtual void UpdateHoverRequrement()
        {
            if (_requireHighlighted)
            {
                _requireHighlighted = false;
            }
            else if (_isHighlighted)
            {
                _isHighlighted = false;
                OnMouseStopHover?.Invoke(this);
            }
        }

        protected bool isSelectionOutlineEnabled;
        public virtual void SetOutlineActive(bool active)
        {
            if (isSelectionOutlineEnabled == active) { return; }
            
            Debug.Log($"[Board] Set Outline Active: {active}", gameObject);

            if (active)
            {
                for (int i = 0; i < Outlines.Length; i++)
                {
                    Outlines[i].SetupOutline();
                }
            }
            else
            {
                for (int i = 0; i < Outlines.Length; i++)
                {
                    Outlines[i].ClearOutline();
                }
            }


            _isHighlighted = active;
            isSelectionOutlineEnabled = active;
        }
        
        public void OnSelect()
        {
            
        }

        protected virtual void onMouseHover()
        {
            _requireHighlighted = true;

            OnMouseHover?.Invoke(this);
        }

        protected virtual void onClickOnSelection()
        {
            OnClickOnSelection?.Invoke(this);
        }

        public virtual void FillWorkbenchContextMenu(IWorkbenchContextMenuPanel panel)
        {
            panel.Build(new WorkbenchContextMenuOptions()
            {
                DeviceName = "Board",
                AllowWorkWithBoard = true,
                AllowFlipObject = true,
                AllowSpecifications = true,
                AllowFocus = true,
            });

            panel.OnSelectFlip += OnSelectFlipPartOption;
            panel.OnSelectWorkWithBoard += OnSelectWorkWithBoard;
            panel.OnSelectFocus += OnSelectFocusPartOption;
        }

        protected virtual void OnSelectFlipPartOption()
        {
            Debug.Log("[Board] Flip Part");
            OnRequireFlip?.Invoke();
        }

        protected virtual void OnSelectWorkWithBoard()
        {
            Debug.Log("[Board] Work With Board");
            OnRequireWorkWithBoard?.Invoke();
        }
        

        protected virtual void OnSelectReplaceTermalPadsOption()
        {
            Debug.Log("[Board] Work With Thermal Interface");
            OnRequireWorkWithThermalInterface?.Invoke();
        }
        
        protected void OnSelectFocusPartOption()
        {
            Debug.Log("[Board] Focus On Part", gameObject);
            workbenchService.GetActiveBench().SetCameraCenterPosition(CameraCenter.position);
        }

        public void ShowPins()
        {
            if (!_isPinsShowing)
            {
                PinsParent.gameObject.SetActive(true);
                
                _isPinsShowing = true;
            }
        }

        public void HideNets()
        {
            NetPathBuilder.ClearNet();
        }
        
        public void HidePins()
        {
            if (_isPinsShowing)
            {
                PinsParent.gameObject.SetActive(false);
                
                _isPinsShowing = false;
            }
        }

        public void SetMicroelementsHoverable(bool hoverable)
        {
            for (int i = 0; i < Slots.Length; i++)
            {
                Slots[i].SetHoverable(hoverable);
            }
        }
        
        public void MicroElementRequreFlip() => OnRequireFlip?.Invoke();

        public void onHoverOnMicroElement(IWorkbenchSelectableObject obj)
        {
            OnHoverOnMicroElement?.Invoke(obj);
        }
        public void onStopHoverOnMicroElement(IWorkbenchSelectableObject obj) => OnStopHoverOnMicroElement?.Invoke(obj);
        public void onClickOnMicroElement(IWorkbenchSelectableObject obj) => OnClickOnMicroElement?.Invoke(obj);
        public void onDisposeSelectableMicroElement(IWorkbenchSelectableObject obj) => OnDisposeSelectableMicroElement?.Invoke(obj);

        public event Action<IWorkbenchSelectableObject> OnMouseHover;
        public event Action<IWorkbenchSelectableObject> OnMouseStopHover;
        public event Action<IWorkbenchSelectableObject> OnClickOnSelection;
        public event Action<IWorkbenchSelectableObject> OnDisposeSelectableObject;
        
        public event Action<IWorkbenchSelectableObject> OnHoverOnMicroElement;
        public event Action<IWorkbenchSelectableObject> OnStopHoverOnMicroElement;
        public event Action<IWorkbenchSelectableObject> OnClickOnMicroElement;
        public event Action<IWorkbenchSelectableObject> OnDisposeSelectableMicroElement;
        
        public event Action OnRequireFlip;
        public event Action OnRequireWorkWithBoard;
        public event Action OnRequireWorkWithThermalInterface;
    }
}