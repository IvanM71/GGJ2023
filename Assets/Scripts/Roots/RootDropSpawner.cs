using Apollo11.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Apollo11
{
    public class RootDropSpawner : MonoBehaviour
    {
        [SerializeField] private Enums.Items itemToGive = Enums.Items.Root;
        [SerializeField] private float resourceSpawnRadius = 0.3f;

        public void Spawn(Vector3 resourceSpawnPoint)
        {
            var offset1 = Random.Range(-resourceSpawnRadius, resourceSpawnRadius);
            var offset2 = Random.Range(-resourceSpawnRadius, resourceSpawnRadius);
            var pos = resourceSpawnPoint + new Vector3(offset1, offset2, 0);
            var prefab = SystemsLocator.Inst.SO_ItemsPrefabs.Dictionary[itemToGive];
            Instantiate(prefab, pos, Quaternion.identity);
            SystemsLocator.Inst.SoundController.PlayThrowItem();
        }
    }
}
