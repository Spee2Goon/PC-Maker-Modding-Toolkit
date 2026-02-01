using System;
using System.Threading.Tasks;
using UnityEngine;

namespace PCMaker.Services
{
    public abstract class PCPart
    {
        public virtual string SaveKey => string.Empty;
        
        public event Action<PCPartObject> OnCreatePartObject;

        protected IPCPartsService PCPartsService;
        protected IPCPartsInstallService PCPartsInstallService;
        protected IInventoryService InventoryService;
        protected IPCBObjectPoolService PCBObjectPoolService;
        protected ILocalizationService LocalizationService;
        protected IWorkbenchService WorkbenchService;
        protected IWorkbenchSelectionService WorkbenchSelectionService;
        protected IMoneyService MoneyService;
        protected IPCBThermalInterfacesService ThermalInterfacesService;
        
        
        public virtual bool AllowLoad { get => true; }

        public string GUID { get; private set; }

        public virtual ConnectorType[] TargetSlotTypes => new ConnectorType[0];
        
        public virtual async Task<bool> LoadResourcesAsync()
        {
            await Task.Delay(50);
            return true;
        }

        public virtual void UnloadResources() { }
        
        public virtual PCPartObject GetPartPrefab()
        {
            throw new NotImplementedException($"Override component prefab please!, key: {SaveKey}");
        }
        public virtual Board GetBoardPrefab()
        {
            throw new NotImplementedException($"Override component board please!, key: {SaveKey}");
        }

        public virtual void AddDefaultParts() { }

        public virtual ObjectSaveData CapturePart() => null;

        public virtual PCPartObject CreatePartObject(Transform parentTransform, IPCParentObject parentObject, PCPartObjectSpawnArguments arguments) => null;

        public virtual bool TryMakePartInstance(ObjectSaveData saveData, out PCPart part)
        {
            part = null;
            return false;
        }

        public virtual PCPartObjectSpawnArguments GetCreationArguments()
        {
            return new PCPartObjectSpawnArguments(true, PCPartObject_SpawnEnvironment.Workbench, null);
        }
    }
}