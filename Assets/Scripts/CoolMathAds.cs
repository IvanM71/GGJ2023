using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Apollo11.Core;
using FMODUnity;

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
            Debug.Log("CoolMathAds PauseGame function called");
            SystemsLocator.Inst.Pause();
            RuntimeManager.MuteAllEvents(true);
        }

        public void ResumeGame()
        {
            Debug.Log("CoolMathAds ResumeGame function called");
            SystemsLocator.Inst.Resume();
            RuntimeManager.MuteAllEvents(false);
        }

        public void InitiateAds()
        {
            Debug.Log("Initiate Ads");
            Application.ExternalCall("triggerAdBreak");
            //PauseGame();
        }


    }
}
