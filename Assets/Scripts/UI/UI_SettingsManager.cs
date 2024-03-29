using Apollo11.Core;
using UnityEngine;
using UnityEngine.UI;

namespace Apollo11.UI
{
    public class UI_SettingsManager : MonoBehaviour
    {
        [SerializeField] private Slider musicSlider;
        [SerializeField] private Slider effectsSlider;
        [SerializeField] private Toggle touchToggle;
        [SerializeField] private UI_Switch touchControlsSwitch;
        
        private PlayerSettings _playerSettings;
        
        public void Awake()
        {
            _playerSettings = PlayerSettings.Instance;

            musicSlider.value = _playerSettings.MusicLevel;
            effectsSlider.value = _playerSettings.SoundLevel;
            touchToggle.isOn = _playerSettings.TouchControls;
            touchControlsSwitch.Init();

            musicSlider.onValueChanged.AddListener(OnMusicSlider) ;
            effectsSlider.onValueChanged.AddListener(OnEffectsSlider) ;
            touchToggle.onValueChanged.AddListener(OnToggleTouchControlsSwitch);
        }
        

        private void OnMusicSlider(float val)
        {
            _playerSettings.MusicLevel = val;
            SystemsLocator.Inst.SoundController.AtSettingsUpdated();
        }

        private void OnEffectsSlider(float val)
        {
            _playerSettings.SoundLevel = val;
            SystemsLocator.Inst.SoundController.AtSettingsUpdated();
        }

        private void OnToggleTouchControlsSwitch(bool active)
        {
            _playerSettings.TouchControls = active;
        }

    }
}
