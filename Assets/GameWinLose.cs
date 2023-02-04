using System;
using Apollo11.Core;
using DG.Tweening;
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

            SystemsLocator.Inst.SoundController.PlayTheme(false);
            
            var seq = DOTween.Sequence();
            seq.AppendInterval(2f);
            seq.AppendCallback(() =>
            {
                
                SystemsLocator.Inst.SoundController.PlayWin();
            });
        }
    }
}
