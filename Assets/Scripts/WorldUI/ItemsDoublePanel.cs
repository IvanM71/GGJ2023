using TMPro;
using UnityEngine;

namespace Apollo11.WorldUI
{
    public class ItemsDoublePanel : MonoBehaviour
    {
        [SerializeField] private TMP_Text item1TMP;
        [SerializeField] private TMP_Text item2TMP;
        [SerializeField] private SpriteRenderer icon1Renderer;
        [SerializeField] private SpriteRenderer icon2Renderer;

        public void SetValues(int current1, int max1, int current2, int max2)
        {
            item1TMP.text = $"{current1}/{max1}";
            item2TMP.text = $"{current2}/{max2}";
        }

        public void SetIcons(Sprite s1, Sprite s2)
        {
            icon1Renderer.sprite = s1;
            icon2Renderer.sprite = s2;
        }
    }
}
