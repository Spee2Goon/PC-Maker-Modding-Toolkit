using System.Collections.Generic;
using UnityEngine;

namespace PCMaker.Services
{
    public class GameConfig
    {
        public GameConfig()
        {
            GameplaySettings = new GameGameplaySettings();
            GraphicsSettings = new GameGraphicsSettings();
            ControllSettings = new GameControllSettings();
        }

        public GameGameplaySettings GameplaySettings;
        public GameGraphicsSettings GraphicsSettings;
        public GameControllSettings ControllSettings;
    }

    public class GameGameplaySettings
    {
        public bool AllowMods;
        
        public bool ReceiveEngineLogs;

        public bool ForceAutoScrew;
        
        public bool isDisclaimerHasBeenRead;
        
        public CurrencyType Currency;
        
        public LocalizationLanguageType TargetLanguageType;
        
        public PlayerHandType TargetPlayerHand;

        public float WorkbenchActionDelayScale;

        public Dictionary<string, bool> ModsActivity = new Dictionary<string, bool>();
        
        public GameGameplaySettings()
        {
            AllowMods = true;
            ReceiveEngineLogs = false;
            ForceAutoScrew = false;
            Currency = CurrencyType.None;
            TargetLanguageType = GetDeviceLanguage();
            TargetPlayerHand = PlayerHandType.Right;
            WorkbenchActionDelayScale = 0.5f;
            
            ModsActivity = new Dictionary<string, bool>();
        }

        
        private LocalizationLanguageType GetDeviceLanguage()
        {
            switch (Application.systemLanguage)
            {
                case SystemLanguage.English:
                    return LocalizationLanguageType.English;
                case SystemLanguage.Russian:
                    return LocalizationLanguageType.Russian;
                case SystemLanguage.German:
                    return LocalizationLanguageType.Germany;
                case SystemLanguage.Polish:
                    return LocalizationLanguageType.Polish;
                case SystemLanguage.Ukrainian:
                    return LocalizationLanguageType.Ukrainian;
                case SystemLanguage.French:
                    return LocalizationLanguageType.French;
                case SystemLanguage.Italian:
                    return LocalizationLanguageType.Italian;
                case SystemLanguage.Czech:
                    return LocalizationLanguageType.Czech;
            }
            
            Debug.LogWarning($"[Config] Cant Setup Default Language, {Application.systemLanguage} Language Are Not Supported");

            return LocalizationLanguageType.English;
        }
    }

    public class GameGraphicsSettings
    {
        public int TargetFrameRate;

        public ViewFormat ViewFormat;

        public GameGraphicsSettings()
        {
            #if UNITY_EDITOR
            TargetFrameRate = Mathf.RoundToInt((float)Screen.currentResolution.refreshRateRatio.value);
            #else
            TargetFrameRate = 0;
            #endif
            
            ViewFormat = ViewFormat.Basic;
        }
    }


    public class GameControllSettings
    {
        public float Sensativity;

        public float m_Yaw;

        public GameControllSettings()
        {
            Sensativity = 1;
            
            m_Yaw = (float)Screen.height / (float)Screen.width;
        }
    }

    public enum ViewFormat
    {
        Lite = 0,
        Basic = 1,
    }
}