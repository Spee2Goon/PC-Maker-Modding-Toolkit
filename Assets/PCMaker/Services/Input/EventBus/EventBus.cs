using System;

namespace PCMaker.Services
{
    public class EventBus
    {
        public void Subscribe<T>(Action<T> handler) where T : InputEvent { }

        public void Unsubscribe<T>(Action<T> handler) where T : InputEvent { }

        public void Publish<T>(T evt) where T : InputEvent { }

        public string[] GetLog() => null;
    }
}
