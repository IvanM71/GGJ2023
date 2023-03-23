﻿using System;
using UnityEngine;

namespace Apollo11.Puzzles
{
    public class Plate : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer plateSpriteRenderer;
        [SerializeField] private SpriteRenderer symbolSpriteRenderer;
        [Space]
        [SerializeField] private Sprite plateUp;
        [SerializeField] private Sprite plateDown;

        private Sprite _symbol;
        private Sprite _symbolActive;

        public int PlateID { get; private set; }
        public event Action<Plate> OnPlatePressed;

        public void Init(int plateID, Sprite symbolUp, Sprite symbolDown)
        {
            PlateID = plateID;
            _symbol = symbolUp;
            _symbolActive = symbolDown;
            
            plateSpriteRenderer.sprite = plateUp;
            symbolSpriteRenderer.sprite = _symbol;
        }

        private void OnTriggerEnter2D(Collider2D col) => AtEntered();
        private void OnTriggerExit2D(Collider2D other) => AtExited();

        private void AtEntered()
        {
            ChangeVisualState(false);
            OnPlatePressed?.Invoke(this);
        }

        private void AtExited()
        {
            ChangeVisualState(true);
        }

        private void ChangeVisualState(bool up)
        {
            plateSpriteRenderer.sprite = up ? plateUp : plateDown;
            symbolSpriteRenderer.sprite = up ? _symbol : _symbolActive;
        }
    }
}