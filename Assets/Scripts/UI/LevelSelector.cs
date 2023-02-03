using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Apollo11
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
