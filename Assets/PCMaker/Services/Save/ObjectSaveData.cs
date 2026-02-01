using System;
using System.Collections.Generic;
using UnityEngine;

namespace PCMaker.Services
{
    [Serializable]
    public class ObjectSaveData
    {
        public ObjectSaveData(string key, Dictionary<string, string> data) { }
        
        public ObjectSaveData(ObjectSaveData data) { }

        [SerializeField]
        public string Key;
        
        public Dictionary<string, string> Data;
    }
}