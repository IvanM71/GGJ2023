using Apollo11.Player;
using TMPro;
using UnityEngine;

namespace Apollo11
{
    public class ItemsCounterCanvas : MonoBehaviour
    {
        [SerializeField] private PlayerItemCarry playerItemCarry;
        [SerializeField] private TMP_Text counterTMP;
        private Canvas _canvas;

        void Start()
        {
            _canvas = GetComponent<Canvas>();
            if (_canvas.renderMode == RenderMode.WorldSpace)
                _canvas.worldCamera = Camera.main;
            _canvas.enabled = false;
            
            playerItemCarry.OnItemsCountChanged += AtItemsCountChanged;
        }

        private void AtItemsCountChanged(int val)
        {
            if (val > 0)
            {
                _canvas.enabled = true;
                counterTMP.text = val.ToString();
            }
            else
            {
                _canvas.enabled = false;
            }
        }

        public void OnPlayerMirrored(float xVal)
        {
            var cScale = _canvas.transform.localScale;
            var abs = Mathf.Abs(cScale.x);
            cScale.x = xVal < 1f ? - abs : abs;
            _canvas.transform.localScale = cScale;
        }

    }
}
