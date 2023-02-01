using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Apollo11
{
    public class UI_HealthPanel : MonoBehaviour
    {
        [SerializeField] private List<Image> hearts;
        [SerializeField] private Sprite hullHeart;
        [SerializeField] private Sprite halfHeart;
        [SerializeField] private Sprite emptyHeart;

        public void SetHealth(int health10)
        {
            int full = health10 / 2;
            int half = health10 % 2;
            
            
        }
    }
}
