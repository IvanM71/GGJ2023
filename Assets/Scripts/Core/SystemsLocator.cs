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


        [SerializeField] private PlayerSystems playerSystems;
        public PlayerSystems PlayerSystems => playerSystems;

        
        
        
        [Space]
        [SerializeField] private WeaponsCharges weaponsCharges;
        public WeaponsCharges WeaponsCharges => weaponsCharges;
        
        
        [SerializeField] private AttackSystem attackSystem;
        public AttackSystem AttackSystem => attackSystem;
        
        
        [Space]
        [SerializeField] private SO_ItemsPrefabs itemsPrefabs;
        public SO_ItemsPrefabs SO_ItemsPrefabs => itemsPrefabs;

        [SerializeField] private RootsSystem rootsSystem;
        public RootsSystem RootsSystem => rootsSystem;
        
    }
}
