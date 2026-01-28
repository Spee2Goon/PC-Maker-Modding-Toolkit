namespace PCMaker.Services
{
    public interface IOrbitCameraService
    {
        IOrbitCamera CreateCamera(CameraInstanceBase orbitCameraInstance);
        void RequestBlockCamera(object requester);
        void ReleaseBlockCamera(object requester);
        bool IsCameraBlocked();
        string[] GetLog();
    }
}