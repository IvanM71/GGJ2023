using DG.Tweening;
using UnityEngine;

namespace Apollo11
{
    public class ProgressBar : MonoBehaviour
    {
        [SerializeField] private Transform fillObject;
        [SerializeField] private float fullSize;

        //for test
        /*private void Start()
        {
            DOTween.To(SetValue01, 0f, 1f, 1f).SetLoops(-1);
        }*/

        public void SetValue01(float val)
        {
            var localPos = fillObject.localPosition;
            localPos.x = fullSize*val / 2f - fullSize/2f;
            fillObject.localPosition = localPos;
            
            var localScale = fillObject.localScale;
            localScale.x = fullSize * val;
            fillObject.localScale = localScale;
        }
    }
}
