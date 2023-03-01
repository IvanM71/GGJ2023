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
            if (level < 1 || level >= SceneManager.sceneCount)
            {
                Debug.LogError("Tried to load non existing level!");
                OpenMainMenu();
            }
            SceneManager.LoadScene(level);
        }
    }
}
