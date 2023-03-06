using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Apollo11
{
    public class LevelButton : MonoBehaviour
    {
        [SerializeField] Image blockImage;
        [SerializeField] TextMeshProUGUI levelNumber;
        public void SetImage(bool isEnabled)
        {
            blockImage.enabled = isEnabled;
        }
        public void SetLevelNumber(int levelNum)
        {
            levelNumber.text = levelNum.ToString();
        }
    }
}
