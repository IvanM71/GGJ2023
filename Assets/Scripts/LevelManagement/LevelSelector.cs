using UnityEngine;
using UnityEngine.UI;

namespace Apollo11.LevelManagement
{
    public class LevelSelector : MonoBehaviour
    {
        // Array of level buttons
        [SerializeField] private Button[] _lvlButtons;
        [SerializeField] private LevelButton[] _lvlButtons2;

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
                    _lvlButtons2[i].SetImage(true);
                }
                else
                {
                    _lvlButtons[i].interactable = true;
                    _lvlButtons2[i].SetImage(false);
                }
                _lvlButtons2[i].SetLevelNumber(i + 1);
            }
        }
    }
}
