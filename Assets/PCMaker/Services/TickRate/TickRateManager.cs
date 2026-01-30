using System;
using UnityEngine;

namespace PCMaker.Services
{
    public class TickRateManager : MonoBehaviour
    {
        //Update
        public static event Action OnTickUpdate;
        public static event Action OnTickUpdateHalf;

        //FixedUpdate
        public static event Action OnTickFixedUpdate;
        public static event Action OnTickFixedUpdateHalf;

        //LateUpdate
        public static event Action OnTickLateUpdate;
        public static event Action OnTickLateUpdateHalf;
        
        //ElectricityUpdate
        public static event Action OnElectricityUpdate;
        public static event Action OnElectricityUpdateHalf;
    }
}