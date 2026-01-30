using System;
using System.Collections.Generic;
using UnityEngine;

namespace PCMaker.Services
{
    //PCPartObject is instance of pc part. (prefab). contain object information like camera center and other staf
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

        public void SetDependencies(PCPartObjectDependencies dependencies)
        {
            InventoryService = dependencies.InventoryService;
            PCPartsService = dependencies.PCPartsService;
            WorkbenchService = dependencies.WorkbenchService;
            WorkbenchSelectionService = dependencies.WorkbenchSelectionService;
            pcbRepairService = dependencies.pcbRepairService;
            pcbThermalInterfaceService = dependencies.pcbThermalInterfaceService;
            configService = dependencies.configService;
            PartsInstallService = dependencies.PartsInstallService;
            PartsRemoveService = dependencies.PartsRemoveService;
            PCBPoolService = dependencies.PCBPoolService;
            LocalizationService = dependencies.LocalizationService;

            isInjected = true;
        }

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

            validate();
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

            for (int i = 0; i < Renderers.Length; i++)
            {
                Renderers[i].gameObject.SetActive(visible);
            }
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
            if (guid == PartReference.GUID)
            {
                parent = this;
                return true;
            }

            parent = null;
            return false;
        }

        protected virtual void OnSelectFlipPartOption()
        {
            Debug.Log("[PCPartObject] Flip Part", gameObject);
            OnRequireFlip?.Invoke();
        }


        protected virtual void validate()
        {
            Debug.Log($"[PCPartObject] Validate: {PartReference.SaveKey}", gameObject);

            if (isDestroyed)
            {
                Debug.LogError("[PCPartObject] isDestroyed!", gameObject);
            }

            if (PartReference == null)
            {
                Debug.LogError("[PCPartObject] PartReference is null!", gameObject);
            }

            if (PartReference.SaveKey == string.Empty)
            {
                Debug.LogError("[PCPartObject] SaveKey is empty!", gameObject);
            }

            if (!isInjected)
            {
                Debug.LogError("[PCPartObject] Part not injected!", gameObject);
            }
        }
    }
}