using System;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using VContainer;

namespace PCMaker.ModAPI
{
    public class ModBehaviour : MonoBehaviour
    {
        private IMod mod;

        //MonoBehaviour
        private IAwakeableMod awakeable;
        private IStartableMod startable;
        private IUpdatableMod updatable;
        private IFixedUpdatableMod fixedUpdatable;
        private ILateUpdatableMod lateUpdatable;

        //Game
        public IWorkOnMainMenuMod workOnMainMenu;
        public IWorkOnGameplayMod workOnGameplay;

        public void SetMod(IMod mod)
        {
            this.mod = mod;

            awakeable = mod as IAwakeableMod;
            startable = mod as IStartableMod;
            updatable = mod as IUpdatableMod;
            fixedUpdatable = mod as IFixedUpdatableMod;
            lateUpdatable = mod as ILateUpdatableMod;

            workOnMainMenu = mod as IWorkOnMainMenuMod;
            workOnGameplay = mod as IWorkOnGameplayMod;
        }

        public async Task LoadModResources()
        {
            await mod.LoadModResources();
        }

        public void InitializeMod(IObjectResolver resolver)
        {
            resolver.Inject(mod);

            try
            {
                mod.Initialize(this);
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }

            Debug.Log($"[Mod] {mod.GetType().Name} initialized");
        }


        private void Awake() => InvokeModAction(() => awakeable?.Awake(), nameof(Awake));

        private void Start() => InvokeModAction(() => startable?.Start(), nameof(Start));

        private void Update() => InvokeModAction(() => updatable?.Update(), nameof(Update));

        private void FixedUpdate() => InvokeModAction(() => fixedUpdatable?.FixedUpdate(), nameof(FixedUpdate));

        private void LateUpdate() => InvokeModAction(() => lateUpdatable?.LateUpdate(), nameof(LateUpdate));


        private void InvokeModAction(Action action, string modActionName)
        {
            try { action?.Invoke(); }
            catch (Exception e) { Debug.LogError($"[Mod Error] {mod.GetType().Name}:{modActionName} - {e}"); }
        }



        public Coroutine StartCoroutineByMod(IEnumerator coroutine)
        {
            return StartCoroutine(coroutine);
        }

        public void StopCoroutineByMod(IEnumerator coroutine)
        {
            StopCoroutine(coroutine);
        }

        public void StopCoroutineByMod(string coroutineName)
        {
            StopCoroutine(coroutineName);
        }
    }
}