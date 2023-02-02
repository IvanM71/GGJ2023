using Apollo11.Player;
using UnityEngine;

namespace Apollo11
{
    public class PlayerSystems : MonoBehaviour
    {
        [SerializeField] private PlayerMovement playerMovement;
        public PlayerMovement PlayerMovement => playerMovement;
        
        
        [SerializeField] private PlayerItemCarry playerItemCarry;
        public PlayerItemCarry PlayerItemCarry => playerItemCarry;
        
        
        [SerializeField] private PlayerAnimation playerAnimation;
        public PlayerAnimation PlayerAnimation => playerAnimation;
        
        public Transform cameraFollowPoint;
    }
}
