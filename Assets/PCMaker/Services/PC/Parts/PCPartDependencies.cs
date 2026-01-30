namespace PCMaker.Services
{
    public class PCPartDependencies
    {
        public IPCPartsService PCPartsService;
        public IPCPartsInstallService PCPartsInstallService;
        public IInventoryService InventoryService;
        public IPCBObjectPoolService PCBObjectPoolService;
        public ILocalizationService LocalizationService;
        public IWorkbenchService WorkbenchService;
        public IWorkbenchSelectionService WorkbenchSelectionService;
        public IMoneyService MoneyService;
        public IPCBThermalInterfacesService ThermalInterfacesService;
    }
}