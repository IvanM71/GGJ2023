using Apollo11.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Apollo11
{
    public class RootDropSpawner : MonoBehaviour
    {
        [SerializeField] private Enums.Items itemToGive = Enums.Items.Root;
        [SerializeField] private int amount = 1;
        [SerializeField] private Vector3 resourceSpawnPoint;
        [SerializeField] private float resourceSpawnRadius = 0.3f;
        [SerializeField] private GameObject prefab;
        public void Spawn(Vector3 resourceSpawnPoint)
        {
            this.resourceSpawnPoint = resourceSpawnPoint;
            //var itemPrefab = SystemsLocator.Inst.SO_ItemsPrefabs.Dictionary[itemToGive];
            for (int i = 0; i < amount; i++)
            {
                var offset1 = Random.Range(-resourceSpawnRadius, resourceSpawnRadius);
                var offset2 = Random.Range(-resourceSpawnRadius, resourceSpawnRadius);
                var pos = resourceSpawnPoint + new Vector3(offset1, offset2, 0);
                Instantiate(prefab, pos, Quaternion.identity);
                SystemsLocator.Inst.SoundController.PlayItemOut();
                //SystemsLocator.Inst.SoundController.PlayThrowItem();
            }
        }
    }
}
