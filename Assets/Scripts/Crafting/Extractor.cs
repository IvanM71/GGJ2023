using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;

namespace Apollo11.Crafting
{
    public class Extractor : MonoBehaviour, IOnProductionDone
    {
        [SerializeField] private ProgressBar progressBar;
        [SerializeField] private float extractionTime = 3f;
        
        public event Action OnProductionDone;

        private Coroutine _extractionRoutine;
        private Tween _extractionProgressTween;


        private void Awake()
        {
            progressBar.gameObject.SetActive(false);
        }

        public void StartExtraction()
        {
            _extractionRoutine = StartCoroutine(IE_Extraction());
        }
        
        public void StopExtraction()
        {
            if (_extractionRoutine == null) return;
            StopCoroutine(_extractionRoutine);
            _extractionRoutine = null;
            
            _extractionProgressTween.Kill();
            progressBar.gameObject.SetActive(false);
        }

        private IEnumerator IE_Extraction()
        {
            var wait = new WaitForSeconds(extractionTime);
            while (true)
            {
                progressBar.gameObject.SetActive(true);
                _extractionProgressTween = DOTween.To(progressBar.SetValue01, 0f, 1f, extractionTime).SetEase(Ease.Linear);
                
                yield return wait;
                OnProductionDone?.Invoke();
            }
        }



      
    }
}