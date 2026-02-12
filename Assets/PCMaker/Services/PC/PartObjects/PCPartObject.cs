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
        
        
        public virtual void ConstructObject(PCPartObjectSpawnArguments args, IPCParentObject parentObject, PCPart sourcePart) { }

        public virtual void DestroyObject() { }

        //Connect
        public virtual bool CanConnect(PCPart part) => true; //Check if we can connect other part to this part

        public virtual bool CanRemove() => true;

        public virtual PCPartObject[] Connect(PCPart part, PCPartObjectSpawnArguments arguments)
        {
            return null;
        }

        //Break connect other part
        public virtual void BreakInstall() { }

        public virtual void FinishInstall() { }

        //Demontage
        public virtual void BeginDemontage() { }

        public virtual void BreakDemontage() { }

        public virtual void FinishDemontage() { }

        protected virtual void onClickOnInstall() { }

        public virtual void SetObjectVisible(bool visible) { }

        public virtual float GetDistanceToTarget() => 1;

        protected Coroutine waitForSomethingCoroutine;

        protected void StopSomethingCoroutine() { }

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