using System;
using UnityEngine;

namespace Apollo11
{
    public class UI_PausePanel : MonoBehaviour
    {
        [SerializeField] private GameObject pausePanel;

        private bool _inPause;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                
            }
        }
    }
}
