using System;
using Unity.Cinemachine;
using UnityEngine;

namespace PCMaker.Services
{
    public interface IPlayer
    {
        void Initialize();
        
        Camera GetMainCamera();
        
        CinemachineBrain GetCinemachineBrain();
        
        CinemachineBrainEvents GetCinemachineBrainEvents();
        
        CinemachineCamera GetCinemachineCamera();
        
        float GetCameraPitch();
        
        void SetCameraPitch(float pitch);
        
        Quaternion GetPlayerRotation();
        
        float GetPlayerRotationX();
        
        void SetPlayerRotation(float rotationX);
        
        Vector3 GetPlayerPosition();
        
        void SetPlayerPosition(Vector3 position);
        
        Vector3 GetPlayerForwardVector();
        
        float GetPlayerSensativity();
        
        void SetCapsuleHeight(float height);
        
        float GetCapsuleHeight();
        
        void SetCapsuleRadius(float radius);
        
        float GetCapsuleRadius();
        
        void SetCapsuleCenter(Vector3 center);
        
        Vector3 GetCapsuleCenter();
        
        Collider GetCapsuleCollider();
        
        void SetPlayerSensativity(float sensativity);
        
        void UpdateVisible();
        
        CharacterController GetCharacterController();
        
        Animator GetAnimatorComponent();
        
        Transform GetCameraRoot();
        
        void Dispose();
        
        void SetNoclip(bool noclip);

        void SetCrouch(bool crouch);
        
        
        
        bool isCrouching { get; }

        
        
        event Action OnPlayerEnable;
        
        event Action OnPlayerDisable;
        
        event Action OnSitUp;
        
        event Action OnSitDown;
        
        event Action OnEnterNoclip;
        
        event Action OnExitNoclip;
    }
}