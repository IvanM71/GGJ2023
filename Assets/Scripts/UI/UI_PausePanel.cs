using Apollo11.Core;
using UnityEngine;

namespace Apollo11.UI
{
    public class UI_PausePanel : MonoBehaviour
    {
        [SerializeField] private GameObject pausePanel;
        [SerializeField] private GameObject pauseButton;

        private void Awake()
        {
            pausePanel.SetActive(false);
        }

        private void Start()
        {
            SystemsLocator.Inst.OnPause += ToPauseView;
            SystemsLocator.Inst.OnResume += ToResumeView;
        }

        private void OnDestroy()
        {
            Time.timeScale = 1f;
            SystemsLocator.Inst.OnPause -= ToPauseView;
            SystemsLocator.Inst.OnResume -= ToResumeView;
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
            SystemsLocator.Inst.Resume();
        }

        public void Pause()
        {
            SystemsLocator.Inst.Pause();
        }
        public void ToResumeView()
        {
            pausePanel.SetActive(false);
            pauseButton.SetActive(true);
        }

        public void ToPauseView()
        {
            pausePanel.SetActive(true);
            pauseButton.SetActive(false);
        }
    }
}
