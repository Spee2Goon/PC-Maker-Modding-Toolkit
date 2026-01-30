using System;
using UnityEngine;

namespace PCMaker.Services
{
    public class Interactable : MonoBehaviour
    {
        [SerializeField]
        private Collider InteractCollider;

        [SerializeField]
        private int HitCooldown = 8;
        
        [SerializeField]
        private InteractType InteractType;

        
#if UNITY_EDITOR
        [SerializeField]
        private bool Message;

        [SerializeField]
        private string LogMessage;
#endif

        
        public event Action OnInteract;

        public InteractType TargetInteractType { get => InteractType; }

        public bool IsOnCooldown { get; }
    }
}