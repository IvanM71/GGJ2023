using TMPro;
using UnityEngine;

namespace Apollo11
{
    public class ItemsSinglePanel : MonoBehaviour
    {
        [SerializeField] private TMP_Text item1TMP;
        [SerializeField] private SpriteRenderer icon1Renderer;

        public void SetValues(int current1, int max1)
        {
            item1TMP.text = $"{current1}/{max1}";
        }

        public void SetIcons(Sprite s1)
        {
            icon1Renderer.sprite = s1;
        }
    }
}
