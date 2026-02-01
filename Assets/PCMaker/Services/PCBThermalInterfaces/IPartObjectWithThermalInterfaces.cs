using UnityEngine;

namespace PCMaker.Services
{
    public interface IPartObjectWithThermalInterfaces : IWorkbenchSelectableObject
    {
        void SetThermalInterfacesHoverable(bool active);
        
        void BuildThermalInterfacesMenu(Transform optionsParent, ThermalInterfacesMenuTitle titlePrefab, ThermalPadMenuOption optionPrefab);
    }
}