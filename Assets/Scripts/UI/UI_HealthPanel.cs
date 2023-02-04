using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Apollo11.UI
{
    public class UI_HealthPanel : MonoBehaviour
    {
        [SerializeField] private List<Image> hearts;
        [SerializeField] private Sprite fullHeart;
        [SerializeField] private Sprite halfHeart;
        [SerializeField] private Sprite emptyHeart;


        /*[Range(0, 10)]
        [SerializeField] private int val;
        private void FixedUpdate() => SetHealth(val);*/


        public void SetHealth(int health10)
        {
            int full = health10 / 2;
            int half = health10 % 2;
            
            for (int i = 0; i < 5; i++)
                hearts[i].sprite = emptyHeart;

            for (int i = 0; i < full; i++)
                hearts[i].sprite = fullHeart;

            if (half!=0)
            {
                hearts[full].sprite = halfHeart;
            }

        }
    }
}
