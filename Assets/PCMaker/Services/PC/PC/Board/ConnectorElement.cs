using System;
using System.Collections.Generic;
using UnityEngine;

namespace PCMaker.Services
{
    public class ConnectorElement : MicroElement, IPCParentObject
    {
        [SerializeField]
        private ConnectorType[] ConnectorTypes;

        [SerializeField]
        private Transform PartPoint;
        
        private PCPartObject ParentObject;
        
        private PCPartObject _connectedPart;
        private string _GUID;
        
        public void Setup(PCPartObject parentObject)
        {
            ParentObject = parentObject;
            
            SetNewRandomGUID();
        }

        public void Dispose()
        {
            ParentObject = null;
            _GUID = string.Empty;
        }
        
        public bool CanConnectPart(PCPart part)
        {
            return false;
        }

        public PCPartObject[] Connect(PCPart part, PCPartObjectSpawnArguments arguments)
        {
            Debug.Log($"Connect object to connector: {part.SaveKey}");
            
            List<PCPartObject> spawnedPartsForInstall = new List<PCPartObject>();
            
            return spawnedPartsForInstall.ToArray();
        }

        private void OnInstallPart(PCPartObject part)
        {
            _connectedPart = part;
            _connectedPart.OnFinishDemontage += OnRemovePartInConnector;
        }

        private void OnRemovePartInConnector(PCPartObject part)
        {
            _connectedPart.OnFinishDemontage -= OnRemovePartInConnector;
            
            _connectedPart = null;
        }

        public PCPart GetPartReference() => ParentObject.PartReference;
        public PCPartObject GetObject() => ParentObject;
        public PCPartObject GetConnectedPart() => _connectedPart;
        public bool HasConnectedPart() => _connectedPart != null;
        public string GetGUID() => _GUID;

        public void SetGUID(string guid)
        {
            Debug.Log($"[ConnectorElement] Set connector ({Names[0]}) guid: {guid}");
            
            _GUID = guid;
        }

        public void SetNewRandomGUID()
        {
            _GUID = Guid.NewGuid().ToString();
        }
    }
}