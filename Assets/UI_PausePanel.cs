using System;
using UnityEngine;

namespace Apollo11
{
    public class UI_PausePanel : MonoBehaviour
    {
        [SerializeField] private GameObject pausePanel;

        private bool _inPause;

        private void Awake()
        {
            pausePanel.SetActive(false);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (_inPause)
                {
                    Time.timeScale = 1f;
                    pausePanel.SetActive(false);
                    _inPause = false;
                }
                else
                {
                    Time.timeScale = 0f;
                    pausePanel.SetActive(true);
                    _inPause = true;
                }
            }
        }
    }
}
