using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Apollo11.Crafting
{
    public class Extractor : MonoBehaviour
    {
        [SerializeField] private GameObject spawnedResourcePrefab;
        [Space]
        [SerializeField] private Transform resourceSpawnPoint;
        [SerializeField] private float resourceSpawnRadius = 0.35f;
        [Space]
        [SerializeField] private ProgressBar progressBar;
        [SerializeField] private float extractionTime = 3f;

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
                SpawnResource();
            }
        }

        private void SpawnResource()
        {
            print("Spawning resource");
            var offset1 = Random.Range(-resourceSpawnRadius, resourceSpawnRadius);
            var offset2 = Random.Range(-resourceSpawnRadius, resourceSpawnRadius);
            var pos = resourceSpawnPoint.position + new Vector3(offset1, offset2, 0);
            Instantiate(spawnedResourcePrefab, pos, Quaternion.identity);
        }

        
    }
}