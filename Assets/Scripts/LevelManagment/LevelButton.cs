using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Apollo11
{
    public class LevelButton : MonoBehaviour
    {
        [SerializeField] Image blockImage;
        [SerializeField] TextMeshProUGUI levelNumber_txt;
        private int levelNumber;
        public void SetImage(bool isEnabled)
        {
            blockImage.enabled = isEnabled;
        }
        public void SetLevelNumber(int levelNum)
        {
            levelNumber = levelNum;
            levelNumber_txt.text = levelNum.ToString();
        }
        public void OpenLevel()
        {
            LevelManager.Instance.currentLevel = levelNumber;
            SceneManager.LoadScene(levelNumber);
        }
    }
}
