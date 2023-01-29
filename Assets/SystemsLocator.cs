using System;
using Apollo11.Player;
using UnityEngine;

namespace Apollo11
{
    public class SystemsLocator : MonoBehaviour
    {
        public static SystemsLocator Inst;
        private void Awake() => Inst = this;


        [SerializeField] private PlayerMovement playerMovement;
        public PlayerMovement PlayerMovement => playerMovement;
    }
}
