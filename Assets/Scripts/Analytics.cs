using System;
using UnityEngine;

namespace Apollo11
{
    public class Analytics : MonoBehaviour
    {
        public void AtLevelStarted(int level)
        {
            Debug.Log($"Analytics: AtLevelStarted {level}");
            
            try
            {
                JSAPIBridge.StartLevelEvent(level);
            }
            catch (Exception e)
            {
                Debug.LogWarning(e);
            }
            
        }

        public void AtLevelRestart(int level)
        {
            Debug.Log($"Analytics: AtLevelRestart {level}");
            
            try
            {
                JSAPIBridge.ReplayEvent(level);
            }
            catch (Exception e)
            {
                Debug.LogWarning(e);
            }
        }
        
        public void AtPlayButtonPressed()
        {
            Debug.Log("Analytics: AtPlayButtonPressed");
            
            try
            {
                JSAPIBridge.StartGameEvent();
            }
            catch (Exception e)
            {
                Debug.LogWarning(e);
            }
        }
    }
}