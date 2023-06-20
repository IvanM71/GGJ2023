using Apollo11.Core;
using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using FMOD.Studio;
using Unity.VisualScripting;
using static UnityEngine.Rendering.DebugUI.Table;

namespace Apollo11
{
    public class Tutorial : MonoBehaviour
    {
        [SerializeField] private GameObject tutorialPanel;
        [SerializeField] private int pageCount;
        [SerializeField] private List<Sprite> firstScreenList;
        [SerializeField] private List<Sprite> secondScreenList;
        [SerializeField] private List<bool> showScreenshots;
        [SerializeField] private List<string> pageText;

        [SerializeField] private Image firstScreenshot;
        [SerializeField] private Image secondScreenshot;
        [SerializeField] private TextMeshProUGUI text;
        [SerializeField] private Button nextButton;
        [SerializeField] private TextMeshProUGUI buttonText;
        [SerializeField] private GameObject screenshotsPanel;

        [SerializeField] private Sprite startButtonSprite;

        private int currentPage = 0;
        private int currentScreenshotsPage = 0;

        private void Start()
        {
            SystemsLocator.Inst.InPause = true;
            Time.timeScale = 0f;
            if (showScreenshots[currentPage])
            {
                screenshotsPanel.SetActive(true);

                firstScreenshot.sprite = firstScreenList[currentScreenshotsPage];
                firstScreenshot.SetNativeSize();

                secondScreenshot.sprite = secondScreenList[currentScreenshotsPage];
                secondScreenshot.SetNativeSize();
                
                currentScreenshotsPage++;
            }
            else
            {
                screenshotsPanel.SetActive(false);
            }

            for(int i = 0; i < pageText.Count; i++)
            {
                pageText[i] = pageText[i].Replace(@"\n", "\n");
            }

            text.SetText(pageText[currentPage]);
        }
        public void NextPageClick()
        {
            
            if(currentPage == pageCount)
            {
                tutorialPanel.SetActive(false);
                SystemsLocator.Inst.InPause = false;
                Time.timeScale = 1f;
            }
            else
            {
                currentPage++;
                if (showScreenshots[currentPage])
                {
                    screenshotsPanel.SetActive(true);

                    firstScreenshot.sprite = firstScreenList[currentScreenshotsPage];
                    firstScreenshot.SetNativeSize();

                    secondScreenshot.sprite = secondScreenList[currentScreenshotsPage];
                    secondScreenshot.SetNativeSize();

                    currentScreenshotsPage++;
                }
                else
                {
                    screenshotsPanel.SetActive(false);
                }
                text.SetText(pageText[currentPage]);
                if (currentPage == pageCount - 1)
                {
                    nextButton.image.sprite = startButtonSprite;
                    buttonText.SetText("Start");
                    currentPage++;
                }
            }
        }
    }
}
