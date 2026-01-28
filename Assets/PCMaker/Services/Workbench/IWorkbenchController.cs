using UnityEngine;

namespace PCMaker.Services
{
    public interface IWorkbenchController
    {
        void StopWorkingOnTemporalObject();
        bool isWorkingOnTemporalObject { get; }
        bool isWorkingOnPC { get; }
        bool isWorkingOnTemporalObjectOrPC { get; }
        void SetCameraCenterPosition(Vector3 worldPosition, bool force = false);
    }
}