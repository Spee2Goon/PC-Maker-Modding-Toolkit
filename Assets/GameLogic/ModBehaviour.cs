using System.Collections;
using UnityEngine;

public class ModBehaviour : MonoBehaviour
{
    private IMod mod;

    public IAwakeableMod awakeable;
    public IStartableMod startable;
    public IUpdatableMod updatable;
    public IFixedUpdatableMod fixedUpdatable;

    public void Initialize(IMod mod)
    {
        this.mod = mod;
        if (awakeable is IAwakeableMod) { awakeable = mod as IAwakeableMod; }
        if (mod is IStartableMod) { startable = mod as IStartableMod; }
        if (mod is IUpdatableMod) { updatable = mod as IUpdatableMod; }
        if (mod is IFixedUpdatableMod) { fixedUpdatable = mod as IFixedUpdatableMod; }

        mod.Initialize(this);
    }

    public void Awake()
    {
        awakeable?.Awake();
    }

    public void Start()
    {
        startable?.Start();
    }

    public void Update()
    {
        updatable?.Update();
    }

    public void FixedUpdate()
    {
        fixedUpdatable?.FixedUpdate();
    }

    public Coroutine StartCoroutineByMod(IEnumerator coroutine)
    {
        return StartCoroutine(coroutine);
    }
}