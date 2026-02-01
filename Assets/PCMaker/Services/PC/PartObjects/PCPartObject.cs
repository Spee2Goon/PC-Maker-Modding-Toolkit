using System;
using UnityEngine;

namespace PCMaker.Services
{
    public class PCPartObject : MonoBehaviour, IPCParentObject
    {
        public Transform CameraCenter;

        public float DefaultCameraDistance = 1;

        public PCPart PartReference;
        public IPCParentObject ParentObject;
        protected PCPartObjectSpawnArguments CurrentObjectSpawnArguments;

        [SerializeField]
        protected MeshRenderer[] Renderers;

        public event Action<PCPartObject> OnFinishInstall;
        public event Action<PCPartObject> OnClickOnInstall;
        public event Action<PCPartObject> OnBreakInstall;
        public event Action<PCPartObject> OnFinishDemontage;
        public event Action<PCPartObject> OnBreakDemontage;
        public event Action OnRequireFlip;

        protected IInventoryService InventoryService;
        protected IPCPartsService PCPartsService;
        protected IWorkbenchService WorkbenchService;
        protected IWorkbenchSelectionService WorkbenchSelectionService;
        protected IPCBRepairService pcbRepairService;
        protected IPCBThermalInterfacesService pcbThermalInterfaceService;
        protected IConfigService configService;
        protected IPCPartsInstallService PartsInstallService;
        protected IPCPartsRemoveService PartsRemoveService;
        protected IPCBObjectPoolService PCBPoolService;
        protected ILocalizationService LocalizationService;

        protected bool isInjected;
        protected bool isDestroyed;
        
        
        public virtual void ConstructObject(PCPartObjectSpawnArguments args, IPCParentObject parentObject, PCPart sourcePart)
        {
            Debug.Log($"[PCPartObject] ConstructObject ({sourcePart.SaveKey}), args: {args}", gameObject);

            if (args == null)
            {
                Debug.LogError("[PCPartObject] Arguments cannot be null", gameObject);
            }

            if (parentObject == null && this is not IPCCaseObject && args.ObjectSpawnEnvironment != PCPartObject_SpawnEnvironment.Workbench)
            {
                Debug.LogError("[PCPartObject] ParentObject cannot be null", gameObject);
            }

            CurrentObjectSpawnArguments = args;
            ParentObject = parentObject;
            PartReference = sourcePart;
        }

        public virtual void DestroyObject()
        {
            Debug.Log($"[PCPartObject] Destroy Object : {PartReference.SaveKey}", gameObject);

            isDestroyed = true;
        }

        //Connect
        public virtual bool CanConnect(PCPart part) => true; //Check if we can connect other part to this part

        public virtual bool CanRemove() => true;

        public virtual PCPartObject[] Connect(PCPart part, PCPartObjectSpawnArguments arguments)
        {
            return null;
        }

        //Break connect other part
        public virtual void BreakInstall()
        {
            OnBreakInstall?.Invoke(this);
            StopSomethingCoroutine();
            DestroyObject();
        }

        public virtual void FinishInstall()
        {
            Debug.Log($"[PCPartObject] ({PartReference.SaveKey}) Finish Install", gameObject);

            OnFinishInstall?.Invoke(this);
        }

        //Demontage
        public virtual void BeginDemontage()
        {
            Debug.Log($"[PCPartObject] ({PartReference.SaveKey}) Begin Demontage", gameObject);
        }

        public virtual void BreakDemontage()
        {
            Debug.Log($"[PCPartObject] ({PartReference.SaveKey}) Break Demontage", gameObject);

            OnBreakDemontage?.Invoke(this);
        }

        public virtual void FinishDemontage()
        {
            Debug.Log($"[PCPartObject] ({PartReference.SaveKey}) Finish Demontage", gameObject);

            CallOnFinishDemontageEvent();

            DestroyObject();
        }


        protected virtual void onClickOnInstall()
        {
            Debug.Log($"[PCPartObject] ({PartReference.SaveKey}) On Click On Install", gameObject);

            OnClickOnInstall?.Invoke(this);
        }

        public virtual void SetObjectVisible(bool visible)
        {
            Debug.Log($"[PCPartObject] ({PartReference.SaveKey}) Set Object Visible: {visible}", gameObject);
        }

        public virtual float GetDistanceToTarget()
        {
            return DefaultCameraDistance;
        }

        protected Coroutine waitForSomethingCoroutine;

        protected void StopSomethingCoroutine()
        {
            if (waitForSomethingCoroutine != null)
            {
                StopCoroutine(waitForSomethingCoroutine);
                waitForSomethingCoroutine = null;
            }
        }

        public PCPart GetPartReference() => PartReference;
        public IPC GetPartPC() => CurrentObjectSpawnArguments.ParentPC;
        public virtual PCPartObject GetObject() => this;
        public string GetGUID() => PartReference.GUID;

        protected void CallOnFinishDemontageEvent() => OnFinishDemontage?.Invoke(this);

        public virtual bool TryGetPCPartByGUID(string guid, out IPCParentObject parent)
        {
            parent = null;
            return false;
        }

        protected virtual void OnSelectFlipPartOption() { }
    }
}