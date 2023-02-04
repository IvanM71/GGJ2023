using System;
using Apollo11.Core;
using UnityEngine;

namespace Apollo11
{
    public class UI_PausePanel : MonoBehaviour
    {
        [SerializeField] private GameObject pausePanel;

        private void Awake()
        {
            pausePanel.SetActive(false);
        }

        private void OnDestroy()
        {
            Time.timeScale = 1f;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (SystemsLocator.Inst.InPause)
                {
                    Resume();
                }
                else
                {
                    Pause();
                }
            }
        }

        public void Resume()
        {
            Time.timeScale = 1f;
            pausePanel.SetActive(false);
            SystemsLocator.Inst.InPause = false;
        }

        private void Pause()
        {
            Time.timeScale = 0f;
            pausePanel.SetActive(true);
            SystemsLocator.Inst.InPause = true;
        }
    }
}
