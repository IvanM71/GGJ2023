using Apollo11.Core;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Apollo11.LevelManagement
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
            LevelManager.Instance.CurrentLevel = level;
            SceneManager.LoadScene(level);
        }
        public void OpenNextLevel()
        {
            if(LevelManager.Instance.CurrentLevel == SceneManager.sceneCountInBuildSettings)
                LevelManager.Instance.CurrentLevel = 1;
            else
                LevelManager.Instance.CurrentLevel++;

            Debug.Log(LevelManager.Instance.CurrentLevel);

            SystemsLocator.Inst.Analytics.AtLevelStarted(LevelManager.Instance.CurrentLevel);
            OpenLevel(LevelManager.Instance.CurrentLevel);
        }

        public void RetryLevel()
        {
            SystemsLocator.Inst.Analytics.AtLevelRestart(LevelManager.Instance.CurrentLevel);
            OpenLevel(LevelManager.Instance.CurrentLevel);
        }
    }
}
