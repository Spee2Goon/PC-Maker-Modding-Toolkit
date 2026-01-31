## GameContext

The `GameContext` class provides global access points to the game state and several services used by the game and mods

### Code:

```csharp
public static class GameContext
{
    public static bool isAssetsLoaded = false;

    public static SaveFileInfo CurrentSaveFileInfo;

    public static List<PCMakerMod> LoadedMods = new List<PCMakerMod>();

    public static IObjectResolver LoaderSceneResolver;
    public static IObjectResolver MenuSceneResolver;
    public static IObjectResolver GameSceneResolver;

    public static IOutlineService OutlineService;
}
```

---

## Field descriptions

* `isAssetsLoaded` — Indicates whether the game has finished loading assets.

* `CurrentSaveFileInfo` — A reference to `SaveFileInfo`. This class contains data about the current save (name, creation date, file path, etc.)

* `LoadedMods` —  A list of mods that are currently loaded by the game

* `LoaderSceneResolver` — A dependency resolver created during asset loading and kept for the lifetime of the game. Used to obtain objects/services that become available after assets are loaded

* `MenuSceneResolver` — Resolver initialized when the main menu is loaded; available in the context of the main menu scene

* `GameSceneResolver` — Resolver initialized when the game scene is loaded; available in the context of the game scene

* `OutlineService` — Global reference to an `IOutlineService` implementation

---

## Lifecycle and initialization order

1. After the game has loaded all services and assets, `isAssetsLoaded` is set to `true`. The game then builds everything into a container. Once the container is built, `LoaderSceneResolver` is assigned the container's `IObjectResolver`
2. When loading to the main menu scene, `MenuSceneResolver` is initialized and `GameSceneResolver` is set to null
3. When loading to the game scene, `GameSceneResolver` is initialized and `MenuSceneResolver` is set to null
4. `CurrentSaveFileInfo` is set by the game when a save is loaded, before GameScene is loaded
5. `LoadedMods` is populated by the mod loader during asset loading
6. `OutlineService` is set by the game after the `LoaderSceneResolver` container has been built