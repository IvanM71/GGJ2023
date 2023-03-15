using Apollo11.UI;
using UnityEngine;

namespace Apollo11
{
    public class GameCanvas : MonoBehaviour
    {
        [SerializeField] private UI_HealthPanel healthPanel;
        public UI_HealthPanel UI_HealthPanel => healthPanel;
        
        [SerializeField] private TouchControls touchControls;
        public TouchControls TouchControls => touchControls;
        private void Awake()
        {
            AtTouchControlsSettingChanged( PlayerSettings.Instance.TouchControls);
            PlayerSettings.Instance.OnTouchControlsValueChanged += AtTouchControlsSettingChanged;
        }

        private void OnDestroy()
        {
            PlayerSettings.Instance.OnTouchControlsValueChanged -= AtTouchControlsSettingChanged;
        }

        private void AtTouchControlsSettingChanged(bool touch)
        {
            TouchControls.gameObject.SetActive(touch);
        }
    }
}
