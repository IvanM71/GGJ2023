using System.Collections;
using UnityEngine;

namespace Apollo11.Crafting
{
    public class Extractor : MonoBehaviour
    {
        [SerializeField] private GameObject spawnedResourcePrefab;
        [Space]
        [SerializeField] private Transform resourceSpawnPoint;
        [SerializeField] private float resourceSpawnRadius = 0.35f;
        [Space]
        [SerializeField] private float extractionTime = 3f;

        private Coroutine _extractionRoutine;

        public void StartExtraction()
        {
            _extractionRoutine = StartCoroutine(IE_Extraction());
        }
        
        public void StopExtraction()
        {
            if (_extractionRoutine == null) return;
            StopCoroutine(_extractionRoutine);
            _extractionRoutine = null;
        }

        private IEnumerator IE_Extraction()
        {
            var wait = new WaitForSeconds(extractionTime);
            while (true)
            {
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