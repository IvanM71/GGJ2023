using UnityEngine;
using UnityEngine.UI;
using Apollo11.UI;

namespace Apollo11
{
    public class SettingsManager : MonoBehaviour
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
        }

        private void OnEffectsSlider(float val)
        {
            _playerSettings.SoundLevel = val;
        }

        private void OnToggleTouchControlsSwitch(bool active)
        {
            _playerSettings.TouchControls = active;
        }

    }
}
