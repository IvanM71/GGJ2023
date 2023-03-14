using UnityEngine;
using UnityEngine.UI;
using System;
using Apollo11.UI;

namespace Apollo11
{
    public class SettingsManager : MonoBehaviour
    {
        [SerializeField] private Slider musicSlider;
        [SerializeField] private Slider effectsSlider;
        [SerializeField] private Toggle touchToggle;
        [SerializeField] private UI_Switch touchControlsSwitch;

        
        public event Action<bool> OnTouchControlsValueChanged; 
        public event Action<float> OnMusicValueChanged; 
        public event Action<float> OnSoundValueChanged; 
        
        
        public void Init()
        {
            touchControlsSwitch.Init();
            musicSlider.value = PlayerPrefs.GetFloat("musicVolume", 0.9f);
            effectsSlider.value = PlayerPrefs.GetFloat("effectsVolume", 0.5f);
            touchToggle.isOn = Convert.ToBoolean(PlayerPrefs.GetInt("touchControls", 1));
        }

        public void OnMusicSlider()
        {
            PlayerPrefs.SetFloat("musicVolume", musicSlider.value);
            OnMusicValueChanged?.Invoke(musicSlider.value);
        }

        public void OnEffectsSlider()
        {
            PlayerPrefs.SetFloat("effectsVolume", effectsSlider.value);
            OnSoundValueChanged?.Invoke(effectsSlider.value);
        }

        public void OnToggle()
        {
            PlayerPrefs.SetInt("touchControls", Convert.ToInt32(touchToggle.isOn));
            OnTouchControlsValueChanged?.Invoke(touchToggle.isOn);
        }

    }
}
