using System.Threading.Tasks;
using UnityEngine;
using VContainer;

namespace PCMaker.Services
{
    public abstract class ModBehaviour : MonoBehaviour
    {
        /// <summary>
        /// Called during the initial loading phase.
        /// The game will wait for this task to complete before proceeding.
        /// </summary>
        public virtual async Task LoadResourcesAsync() { }


        /// <summary>
        /// Called after the dependency injection container is created.
        /// <para>Use <paramref name="resolver"/> to access game services.</para>
        /// </summary>
        /// <param name="resolver">The DI container to resolve dependencies.</param>
        public virtual void Initialize(IObjectResolver resolver) { }


        /// <summary>
        /// Called after all services and mods have loaded resources and initialized.
        /// <para>This is the final step before the Main Menu is loaded.</para>
        /// </summary>
        public virtual void OnAllAssetsLoaded() { }


        // --- Menu Scene Events ---

        /// <summary>
        /// Called when the Main Menu scene has been fully loaded and initialized.
        /// </summary>
        public virtual void OnMainMenuLoaded() { }

        /// <summary>
        /// Called when the Main Menu scene is about to be disposed.
        /// </summary>
        public virtual void OnMainMenuUnloading() { }


        // --- Game Scene Events ---

        /// <summary>
        /// Called when the Game Scene has been fully loaded and initialized.
        /// </summary>
        public virtual void OnGameSceneLoaded() { }

        /// <summary>
        /// Called when the Game Scene is about to be disposed.
        /// </summary>
        public virtual void OnGameSceneUnloading() { }
    }
}