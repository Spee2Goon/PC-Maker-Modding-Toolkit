using System.Collections.Generic;

namespace PCMaker.Services
{
    public class WorkbenchContextMenuOptions
    {
        public string DeviceName;
        public bool AllowDemontage;
        public bool AllowFlipObject;
        public bool AllowWorkWithBoard;
        public bool AllowWorkWithThermalInterface;
        public bool AllowReplace;
        public bool AllowSpecifications;
        public bool AllowFocus;
        
        public List<WorkbenchContextMenuCustomButton> CustomButtons = new();
    }
}