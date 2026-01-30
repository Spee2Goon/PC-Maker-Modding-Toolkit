using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Profiling;

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

        
        public void SetDependencies(PCPartDependencies dependencies)
        {
            PCPartsService = dependencies.PCPartsService;
            PCPartsInstallService = dependencies.PCPartsInstallService;
            InventoryService = dependencies.InventoryService;
            PCBObjectPoolService = dependencies.PCBObjectPoolService;
            LocalizationService = dependencies.LocalizationService;
            WorkbenchService = dependencies.WorkbenchService;
            WorkbenchSelectionService = dependencies.WorkbenchSelectionService;
            MoneyService = dependencies.MoneyService;
            ThermalInterfacesService = dependencies.ThermalInterfacesService;
        }
        
        
        
        public virtual bool AllowLoad { get => true; }

        public string GUID { get; private set; }

        public void SetGUID(string guid)
        {
            GUID = guid;
            
            Debug.Log($"[PCPart] Set Part ({SaveKey}) guid : {GUID}");
        }

        
        public void SetNewRandomGUID() => GUID = Guid.NewGuid().ToString();

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

        public virtual ObjectSaveData CapturePart()
        {
            Dictionary<string, string> data = new Dictionary<string, string>();
            
            data["SaveKey"] = SaveKey;
            
            return new ObjectSaveData(GUID, data);
        }
        
        public virtual PCPartObject CreatePartObject(Transform parentTransform, IPCParentObject parentObject, PCPartObjectSpawnArguments arguments)
        {
            Debug.Log($"[PCPart] CreatePartObject : {SaveKey}");
            
            Profiler.BeginSample($"Create Part Object : {SaveKey}");
            
            PCPartObject spawnedPart = GameObject.Instantiate(GetPartPrefab(), parentTransform);

            PCPartsService.InjectPCPartObject(spawnedPart);
            
            spawnedPart.ConstructObject(arguments, parentObject, this);

            Profiler.EndSample();
            
            return spawnedPart;
        }

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