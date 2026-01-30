using System;

namespace PCMaker.Services
{
    public abstract class Screw
    {
        void SetupScrew() { }
        void SetScrewInteractActive(bool active) { }
        void SetScrewed(bool screwed, ScrewAnimationType animationType = ScrewAnimationType.Default) { }

        public event Action OnClick;
    }
}