using DG.Tweening;
using UnityEngine;

namespace Apollo11.Core
{
    public class GameWinLose : MonoBehaviour
    {
        [SerializeField] private RootsSystem _rootsSystem;
        [SerializeField] private GameObject winPanel;
        [SerializeField] private GameObject losePanel;

        private void Start()
        {
            _rootsSystem.OnAllMainRootsDead += AtWin;
            SystemsLocator.Inst.PlayerSystems.PlayerHealth.OnPlayerDead += AtLose;
        }

        private void OnDestroy()
        {
            _rootsSystem.OnAllMainRootsDead -= AtWin;
            SystemsLocator.Inst.PlayerSystems.PlayerHealth.OnPlayerDead -= AtLose;
        }

        private void AtLose()
        {
            print("LOSE");

            SystemsLocator.Inst.SoundController.PlayTheme(false);
            
            var seq = DOTween.Sequence();
            seq.AppendInterval(2f);
            seq.AppendCallback(() =>
            {
                losePanel.gameObject.SetActive(true);
                SystemsLocator.Inst.SoundController.PlayLose();
            });
        }

        private void AtWin()
        {
            print("WIN");

            SystemsLocator.Inst.SoundController.PlayTheme(false);
            
            var seq = DOTween.Sequence();
            seq.AppendInterval(2f);
            seq.AppendCallback(() =>
            {
                winPanel.gameObject.SetActive(true);
                SystemsLocator.Inst.SoundController.PlayWin();
            });
        }
    }
}
