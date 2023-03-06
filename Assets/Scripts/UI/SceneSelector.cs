using UnityEngine;
using UnityEngine.SceneManagement;

namespace Apollo11.UI
{
    public class SceneSelector : MonoBehaviour
    {

        public void OpenMainMenu()
        {
            SceneManager.LoadScene(0);
        }

        public void OpenLevel(int level)
        {
            if (level < 1 || level >= SceneManager.sceneCountInBuildSettings)
            {
                Debug.LogError($"Tried to load non existing level ({level})!");
                OpenMainMenu();
                return;
            }
            LevelManager.Instance.currentLevel = level;
            SceneManager.LoadScene(level);
        }
        public void OpenNextLevel()
        {
            if(LevelManager.Instance.currentLevel == SceneManager.sceneCountInBuildSettings)
                LevelManager.Instance.currentLevel = 1;
            else
                LevelManager.Instance.currentLevel++;

            Debug.Log(LevelManager.Instance.currentLevel);

            OpenLevel(LevelManager.Instance.currentLevel);
        }

        public void RetryLevel()
        {
            OpenLevel(LevelManager.Instance.currentLevel);
        }
    }
}
