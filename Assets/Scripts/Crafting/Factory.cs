using System;
using Apollo11.Items.Crafting;
using Apollo11.WorldUI;
using DG.Tweening;
using UnityEngine;

namespace Apollo11.Crafting
{
    public class Factory : MonoBehaviour, IOnProductionDone
    {
        [SerializeField] private ProgressBar progressBar;
        [SerializeField] private float craftingTime = 3f;

        public event Action OnProductionDone;

        private IItemsSlots _itemsSlots;
        public IItemsSlots ItemsSlots => _itemsSlots;

        private void Awake()
        {
            progressBar.gameObject.SetActive(false);
            
            _itemsSlots = GetComponent<IItemsSlots>();
            if (_itemsSlots == null)
                throw new NullReferenceException("No ItemSlots component assigned to Factory!");
            
            _itemsSlots.OnFull += AtItemsSlotsFull;
        }
        
        private void OnDestroy()
        {
            _itemsSlots.OnFull -= AtItemsSlotsFull;
        }

        private void AtItemsSlotsFull()
        {
            progressBar.gameObject.SetActive(true);
            DOTween.To(progressBar.SetValue01, 0f, 1f, craftingTime).SetEase(Ease.Linear)
                .OnComplete(() =>
                {
                    OnProductionDone?.Invoke();
                    _itemsSlots.Reset();
                    progressBar.gameObject.SetActive(false);
                });
        }

        


    }
}