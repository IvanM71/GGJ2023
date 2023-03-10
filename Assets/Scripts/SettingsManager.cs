using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

namespace Apollo11
{
    public class SettingsManager : MonoBehaviour
    {
        [SerializeField] private Slider musicSlider;
        [SerializeField] private Slider effectsSlider;
        [SerializeField] private Toggle touchToggle;

        private void Awake()
        {
            musicSlider.value = PlayerPrefs.GetFloat("musicVolume", 0.9f);
            effectsSlider.value = PlayerPrefs.GetFloat("effectsVolume", 0.5f);
            touchToggle.isOn = Convert.ToBoolean(PlayerPrefs.GetInt("touchControls", 1));
        }

        public void OnMusicSlider()
        {
            PlayerPrefs.SetFloat("musicVolume", musicSlider.value);
        }

        public void OnEffectsSlider()
        {
            PlayerPrefs.SetFloat("effectsVolume", effectsSlider.value);
        }

        public void OnToggle()
        {
            PlayerPrefs.SetInt("touchControls", Convert.ToInt32(touchToggle.isOn));
        }
    }
}
