namespace PCMaker.Services
{
    public class PCPartObjectDependencies
    {
        public IInventoryService InventoryService;
        public IPCPartsService PCPartsService;
        public IWorkbenchService WorkbenchService;
        public IWorkbenchSelectionService WorkbenchSelectionService;
        public IPCBRepairService pcbRepairService;
        public IPCBThermalInterfacesService pcbThermalInterfaceService;
        public IConfigService configService;
        public IPCPartsInstallService PartsInstallService;
        public IPCPartsRemoveService PartsRemoveService;
        public IPCBObjectPoolService PCBPoolService;
        public ILocalizationService LocalizationService;
    }
}