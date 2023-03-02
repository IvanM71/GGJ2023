using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Apollo11
{
    public class LevelSelector : MonoBehaviour
    {
        // Array of level buttons
        [SerializeField] private Button[] _lvlButtons;

        // Every time on awake checks max unlocked level and enabling buttons
        private void Awake()
        {
            // Get max unlocked level from PlayerPrefs
            // If not set to 1
            int maxCompletedLevel = PlayerPrefs.GetInt("maxCompletedLevel", 1);

            // Check every button is it`s level unlocked
            // If unlocked - enable and hide lock image
            // If locked - unenable and show lock image
            for (int i = 0; i < _lvlButtons.Length; i++)
            {
                if (i + 1 > maxCompletedLevel)
                {
                    _lvlButtons[i].interactable = false;
                    //if (i > 0)
                    //    _lvlButtons[i].transform.GetChild(1).GetComponent<Image>().enabled = true;
                }
                else
                {
                    _lvlButtons[i].interactable = true;
                    //if (i > 0)
                    //    _lvlButtons[i].transform.GetChild(1).GetComponent<Image>().enabled = false;
                }
                _lvlButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = (i + 1).ToString();
            }
        }
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
    }
}
