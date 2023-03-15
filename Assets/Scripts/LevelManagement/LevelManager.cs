using UnityEngine;

namespace Apollo11.LevelManagement
{
    public class LevelManager : MonoBehaviour
    {
        public static LevelManager Instance { get; private set; }

        public int CurrentLevel { get; set; }

        void Awake()
        {
            if (Instance != null)
            {
                Debug.LogWarning("Second instance of singleton Awake!");
                return;
            }
            
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        
        
        public void UnlockNextLevel()
        {
            // If next level number bigger than last already unlocked level - unlock next level
            if (CurrentLevel + 1 > PlayerPrefs.GetInt("maxCompletedLevel"))
            {
                PlayerPrefs.SetInt("maxCompletedLevel", CurrentLevel + 1);
            }
        }
    }
}
