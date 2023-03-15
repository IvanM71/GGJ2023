using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Apollo11.LevelManagement
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
            LevelManager.Instance.CurrentLevel = levelNumber;
            SceneManager.LoadScene(levelNumber);
        }
    }
}
