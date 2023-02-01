using Apollo11.Items;
using Apollo11.Player;
using Apollo11.WeaponCharges;
using UnityEngine;

namespace Apollo11.Core
{
    public class SystemsLocator : MonoBehaviour
    {
        public static SystemsLocator Inst;
        private void Awake() => Inst = this;


        [SerializeField] private PlayerMovement playerMovement;
        public PlayerMovement PlayerMovement => playerMovement;
        
        
        [SerializeField] private PlayerItemCarry playerItemCarry;
        public PlayerItemCarry PlayerItemCarry => playerItemCarry;
        
        
        [SerializeField] private PlayerAnimation playerAnimation;
        public PlayerAnimation PlayerAnimation => playerAnimation;

        
        [SerializeField] private SO_ItemsPrefabs itemsPrefabs;
        public SO_ItemsPrefabs SO_ItemsPrefabs => itemsPrefabs;
        
        
        [SerializeField] private WeaponsCharges weaponsCharges;
        public WeaponsCharges WeaponsCharges => weaponsCharges;
        
        
        [SerializeField] private AttackSystem attackSystem;
        public AttackSystem AttackSystem => attackSystem;
        
        
        
    }
}
