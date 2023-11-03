using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Apollo11.Core;

namespace Apollo11
{
    public class CoolMathAds : MonoBehaviour
    {
        public static CoolMathAds instance;

        void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else if (instance != null && instance != this)
            {
                Destroy(this);
            }
        }

        void Start()
        {
            DontDestroyOnLoad(gameObject);
        }

        public void PauseGame()
        {
            Debug.Log("PauseGame function called");
            Time.timeScale = 0f;
            SystemsLocator.Inst.InAdsPause = true;
            SystemsLocator.Inst.SoundController.PlayMainTheme(false);
        }

        public void ResumeGame()
        {
            Debug.Log("ResumeGame function called");
            Time.timeScale = 1.0f;
            SystemsLocator.Inst.InAdsPause = false;
            SystemsLocator.Inst.SoundController.PlayMainTheme(true);
        }

        public void InitiateAds()
        {
            Debug.Log("Initiate Ads");
            Application.ExternalCall("triggerAdBreak");
        }


    }
}
