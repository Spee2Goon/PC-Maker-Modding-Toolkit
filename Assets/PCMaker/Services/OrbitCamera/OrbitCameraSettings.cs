using System;
using UnityEngine;

namespace PCMaker.Services
{
    [Serializable]
    public class OrbitCameraSettings
    {
        public Transform Target;
        public bool AllowZoom;
        public float DefaultZoomDistance = 0.5f;
        public float MinZoomDistance = 0.0425f;
        public float MaxZoomDistance = 1.5f;
        public float ZoomSpeed = 4;
        public bool AllowRotatation = true;
        public float RotationSpeed = 4;
        public bool ClampVertical = true;
        public Vector2 ClampAngles = new Vector2(-89, 89);
        public Vector2 InitialRotation = new Vector2(0, 35f);
        public bool AllowOffset = true;
        public float OffsetSpeed = 2.5f;
        public Vector3 maximumOffset = new Vector3(0.35f, 0.35f, 0.35f);
        public bool AllowCollision = true;
        public LayerMask CollisionLayers = 1 << 0;
        public float CollisionOffset = 0.01f;
    }
}