using UnityEngine;

namespace Unity.FPS.Game
{
    public static class SettingsManager
    {
        private const string LookSensitivityKey = "LookSensitivity";
        private const string ShadowsToggleKey = "ShadowsToggle";
        private const string VolumeSliderValueKey = "VolumeSliderValue";
        private const string FramerateToggleKey = "FramerateToggle";

        public static float LookSensitivity
        {
            get => PlayerPrefs.GetFloat(LookSensitivityKey, 0.5f);
            set
            {
                PlayerPrefs.SetFloat(LookSensitivityKey, value);
                PlayerPrefs.Save();
            }
        }

        public static bool ShadowsEnabled
        {
            get => PlayerPrefs.GetInt(ShadowsToggleKey, 1) == 1;
            set
            {
                PlayerPrefs.SetInt(ShadowsToggleKey, value ? 1 : 0);
                PlayerPrefs.Save();
            }
        }

        public static float Volume
        {
            get => PlayerPrefs.GetFloat(VolumeSliderValueKey, 1f);
            set
            {
                PlayerPrefs.SetFloat(VolumeSliderValueKey, value);
                PlayerPrefs.Save();
            }
        }

        public static bool FramerateDisplayEnabled
        {
            get => PlayerPrefs.GetInt(FramerateToggleKey, 0) == 1;
            set
            {
                PlayerPrefs.SetInt(FramerateToggleKey, value ? 1 : 0);
                PlayerPrefs.Save();
            }
        }

        public static void LoadSettings()
        {
            // Access properties to ensure values are loaded
            _ = LookSensitivity;
            _ = ShadowsEnabled;
            _ = Volume;
            _ = FramerateDisplayEnabled;
        }
    }
}
