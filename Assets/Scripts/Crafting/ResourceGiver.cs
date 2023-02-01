using System;
using Apollo11.Core;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Apollo11.Crafting
{
    public class ResourceGiver : MonoBehaviour
    {
        [SerializeField] private Enums.Items itemToGive;
        [SerializeField] private bool givesChargesNotItems;
        [SerializeField] private Enums.RootWeapon chargeTypeToGive;
        [SerializeField] private int amount = 1;
        [SerializeField] private Transform resourceSpawnPoint;
        [SerializeField] private float resourceSpawnRadius = 0.3f;

        private IOnProductionDone _producer;
        
        private void Awake()
        {
            _producer = GetComponent<IOnProductionDone>();
            if (_producer == null)
                throw new NullReferenceException("No IOnProductionDone component assigned to ResourceGiver!");
            _producer.OnProductionDone += Spawn;
        }

        private void OnDestroy()
        {
            _producer.OnProductionDone -= Spawn;
        }

        private void Spawn()
        {
            if (givesChargesNotItems)
            {
                SystemsLocator.Inst.WeaponsCharges.AddCharges(chargeTypeToGive, amount);
            }
            else
            {
                var itemPrefab = SystemsLocator.Inst.SO_ItemsPrefabs.Dictionary[itemToGive];
                for (int i = 0; i < amount; i++)
                {
                    var offset1 = Random.Range(-resourceSpawnRadius, resourceSpawnRadius);
                    var offset2 = Random.Range(-resourceSpawnRadius, resourceSpawnRadius);
                    var pos = resourceSpawnPoint.position + new Vector3(offset1, offset2, 0);
                    Instantiate(itemPrefab, pos, Quaternion.identity);
                }
            }
            
        }
    }
}