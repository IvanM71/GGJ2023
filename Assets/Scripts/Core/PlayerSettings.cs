using System;
using UnityEngine;

namespace Apollo11.Core
{
    public class PlayerSettings : MonoBehaviour
    {
        public static PlayerSettings Instance { get; private set; }
        
        private float _musicLevel;
        private float _soundLevel;
        private bool _touchControls;

        public float MusicLevel
        {
            get => _musicLevel;
            set
            {
                _musicLevel = value;
                PlayerPrefs.SetFloat("musicVolume",_musicLevel);
                OnMusicValueChanged?.Invoke(_musicLevel);
            }
        }

        public float SoundLevel
        {
            get => _soundLevel;
            set
            {
                _soundLevel = value; 
                PlayerPrefs.SetFloat("effectsVolume", _soundLevel);
                OnSoundValueChanged?.Invoke(_soundLevel);
            }
        }

        public bool TouchControls
        {
            get => _touchControls;
            set
            {
                _touchControls = value; 
                PlayerPrefs.SetInt("touchControls", Convert.ToInt32(_touchControls));
                OnTouchControlsValueChanged?.Invoke(_touchControls);
            }
        }
        
        public event Action<bool> OnTouchControlsValueChanged; 
        public event Action<float> OnMusicValueChanged; 
        public event Action<float> OnSoundValueChanged; 

        private void Awake()
        {
            if (Instance != null)
            {
                Debug.LogWarning("Second instance of singleton Awake!"); 
                return;
            }
            
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        private void Start()
        {
            MusicLevel = PlayerPrefs.GetFloat("musicVolume", 0.9f);
            SoundLevel = PlayerPrefs.GetFloat("effectsVolume", 0.5f);
            TouchControls = SystemInfo.deviceType == DeviceType.Handheld;
        }
        
    }
}
