using FMODUnity;
using UnityEngine;
using UnityEngine.Advertisements;

namespace Apollo11
{
    public class Interstitial : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener
    {
        [SerializeField] string _androidAdUnitId = "Interstitial_Android";
        [SerializeField] string _iOsAdUnitId = "Interstitial_iOS";
        string _adUnitId;

        void Awake()
        {
            // Get the Ad Unit ID for the current platform:
            #if UNITY_IOS
                _adUnitId = _iOsAdUnitId;
            #elif UNITY_ANDROID
                _adUnitId = _androidAdUnitId;
            #elif UNITY_EDITOR
                _adUnitId = _androidAdUnitId; //Only for testing the functionality in the Editor
            #endif
            LoadAd();
        }

        // Load content to the Ad Unit:
        public void LoadAd()
        {
            // IMPORTANT! Only load content AFTER initialization (in this example, initialization is handled in a different script).
            Debug.Log("Loading Ad: " + _adUnitId);
            Advertisement.Load(_adUnitId, this);
        }

        // Show the loaded content in the Ad Unit:
        public void ShowAd()
        {
            // Note that if the ad content wasn't previously loaded, this method will fail
            
            Debug.Log("Showing Ad: " + _adUnitId);
            Advertisement.Show(_adUnitId, this);
            LoadAd();
        }

        // Implement Load Listener and Show Listener interface methods: 
        public void OnUnityAdsAdLoaded(string adUnitId)
        {
            // Optionally execute code if the Ad Unit successfully loads content.
        }

        public void OnUnityAdsFailedToLoad(string _adUnitId, UnityAdsLoadError error, string message)
        {
            Debug.Log($"Error loading Ad Unit: {_adUnitId} - {error.ToString()} - {message}");
            // Optionally execute code if the Ad Unit fails to load, such as attempting to try again.
        }

        public void OnUnityAdsShowFailure(string _adUnitId, UnityAdsShowError error, string message)
        {
            Debug.Log($"Error showing Ad Unit {_adUnitId}: {error.ToString()} - {message}");
            // Optionally execute code if the Ad Unit fails to show, such as loading another ad.
        }

        public void OnUnityAdsShowStart(string _adUnitId) 
        {
            RuntimeManager.MuteAllEvents(true);
        }
        public void OnUnityAdsShowClick(string _adUnitId) { }
        public void OnUnityAdsShowComplete(string _adUnitId, UnityAdsShowCompletionState showCompletionState) 
        {
            RuntimeManager.MuteAllEvents(false);
        }
    }
}
