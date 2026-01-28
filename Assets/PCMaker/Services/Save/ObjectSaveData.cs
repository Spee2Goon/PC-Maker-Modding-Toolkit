using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace PCMaker.Services
{
    [Serializable]
    public class ObjectSaveData
    {
        public ObjectSaveData(string key, Dictionary<string, string> data)
        {
            Key = key;

            Metadata = new List<DataStructure>();
            SetMetadata(data);
        }
        
        public ObjectSaveData(ObjectSaveData data)
        {
            Key = data.Key;

            Metadata = new List<DataStructure>();
            SetMetadata(data.Data);
        }

        [SerializeField]
        public string Key;
        
        [SerializeField]
        private List<DataStructure> Metadata;
        
        [Serializable]
        private struct DataStructure
        {
            public string Key;
            public string Value;
        }
        
        private void SetMetadata(Dictionary<string, string> dict)
        {
            Metadata.Clear();

            if (dict != null)
            {
                foreach (var kvp in dict)
                {
                    Metadata.Add(new DataStructure { Key = kvp.Key, Value = kvp.Value });
                }
            }
        }

        private Dictionary<string, string> GetMetadata()
        {
            var dict = new Dictionary<string, string>();

            foreach (var entry in Metadata)
            {
                dict[entry.Key] = entry.Value;
            }
            
            return dict;
        }

        
        private Dictionary<string, string> _data;
        public Dictionary<string, string> Data
        {
            get
            {
                if (_data != null)
                {
                    return _data;
                }
                else
                {
                    _data = GetMetadata();
                    return _data;
                }
            }
        }

        public override string ToString()
        {
            string str = string.Empty;

            str += "PartSaveData:\n";
            
            for (int i = 0; i < Data.Count; i++)
            {
                str += $"[{i}] {Data.ElementAt(i).Key} - {Data.ElementAt(i).Value}\n";
            }
            
            return str;
        }
    }
}