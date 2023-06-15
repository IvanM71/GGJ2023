using Apollo11.Core;
using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Apollo11
{
    public class Tutorial : MonoBehaviour
    {
        [SerializeField] private GameObject tutorialPanel;
        [SerializeField] private int pageCount;
        [SerializeField] private List<Sprite> firstScreenList;
        [SerializeField] private List<Sprite> secondScreenList;
        [SerializeField] private List<string> pageText;

        [SerializeField] private Image firstScreenshot;
        [SerializeField] private Image secondScreenshot;
        [SerializeField] private TextMeshProUGUI text;
        [SerializeField] private Button nextButton;

        [SerializeField] private Sprite startButtonSprite;

        private int currentPage = 0;

        private void Awake()
        {
            SystemsLocator.Inst.InPause = true;
            firstScreenshot.sprite = firstScreenList[currentPage];
            firstScreenshot.SetNativeSize();
            secondScreenshot.sprite = secondScreenList[currentPage];
            secondScreenshot.SetNativeSize();
            text.SetText(pageText[currentPage]);
        }
        public void NextPageClick()
        {
            
            if(currentPage == pageCount)
            {
                tutorialPanel.SetActive(false);
                SystemsLocator.Inst.InPause = false;
            }
            else
            {
                currentPage++;
                firstScreenshot.sprite = firstScreenList[currentPage];
                firstScreenshot.SetNativeSize();
                secondScreenshot.sprite = secondScreenList[currentPage];
                secondScreenshot.SetNativeSize();
                text.SetText(pageText[currentPage]);
                if (currentPage == pageCount - 1)
                {
                    nextButton.image.sprite = startButtonSprite;
                    currentPage++;
                }
            }
        }
    }
}
