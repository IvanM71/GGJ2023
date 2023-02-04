using Apollo11.Core;
using UnityEngine;

namespace Apollo11.Player
{
    public class PlayerSpawner : MonoBehaviour
    {
        [SerializeField] private Transform playerSpawnPoint;
        [SerializeField] private GameObject playerPrefab;

        private void Awake()
        {
            var player = Instantiate(playerPrefab, playerSpawnPoint.position, Quaternion.identity);
            SystemsLocator.Inst.PlayerSystems = player.GetComponent<PlayerSystems>();
        }
    }
}
