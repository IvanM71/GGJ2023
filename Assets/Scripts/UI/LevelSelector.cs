using UnityEngine;
using UnityEngine.SceneManagement;

namespace Apollo11.UI
{
    public class LevelSelector : MonoBehaviour
    {
        public int level;

        public void OpenLevel()
        {
            SceneManager.LoadScene(level);
        }
    }
}
