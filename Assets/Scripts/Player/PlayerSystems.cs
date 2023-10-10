using Apollo11.Interaction;
using UnityEngine;

namespace Apollo11.Player
{
    public class PlayerSystems : MonoBehaviour
    {
        [SerializeField] private PlayerMovement playerMovement;
        public PlayerMovement PlayerMovement => playerMovement;
        
        
        [SerializeField] private PlayerHealth playerHealth;
        public PlayerHealth PlayerHealth => playerHealth;
        
        [SerializeField] private PlayerItemCarry playerItemCarry;
        public PlayerItemCarry PlayerItemCarry => playerItemCarry;
        
        [SerializeField] private PlayerWeaponsInHand playerWeaponsInHand;
        public PlayerWeaponsInHand PlayerWeaponsInHand => playerWeaponsInHand;
        
        
        [SerializeField] private PlayerAnimation playerAnimation;
        public PlayerAnimation PlayerAnimation => playerAnimation;
        
        [SerializeField] private InteractionVision interactionVision;
        public InteractionVision InteractionVision => interactionVision;

        [SerializeField] private ParticleSystemManager particleSystemManager;
        public ParticleSystemManager ParticleSystemManager => particleSystemManager;
        
        public Transform cameraFollowPoint;
    }
}
