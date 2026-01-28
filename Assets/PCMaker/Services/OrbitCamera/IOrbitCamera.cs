using UnityEngine;

namespace PCMaker.Services
{
    public interface IOrbitCamera
    {
        void Setup(CameraInstanceBase instance);
        void Dispose();

        void EnableCamera();
        void DisableCamera();

        bool CanMoveCamera();
        
        void SetCameraTransformToDefault(bool force);
        public void SetDistanceToTarget(float distance);

        public void ClearOffset();
        public Vector3 GetOffset();

        public void SetOffset(Vector3 offset, bool force = false);
    }
}