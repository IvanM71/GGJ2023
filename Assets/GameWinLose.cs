using System;
using UnityEngine;

namespace Apollo11
{
    public class GameWinLose : MonoBehaviour
    {
        [SerializeField] private RootsSystem _rootsSystem;

        private void Awake()
        {
            _rootsSystem.OnAllMainRootsDead += OnAllMainRootsDead;
        }

        private void OnDestroy()
        {
            _rootsSystem.OnAllMainRootsDead -= OnAllMainRootsDead;
        }

        private void OnAllMainRootsDead()
        {
            print("WIN");
        }
    }
}
