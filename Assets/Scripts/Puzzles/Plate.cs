using System;
using DG.Tweening;
using UnityEngine;

namespace Apollo11.Puzzles
{
    public class Plate : MonoBehaviour, IBreathBlinkPuzzle
    {
        [SerializeField] private SpriteRenderer plateSpriteRenderer;
        [SerializeField] private Transform symbolsVisualsT;
        [SerializeField] private SpriteRenderer symbolSpriteRenderer;
        [SerializeField] private SpriteRenderer symbolActiveSpriteRenderer;
        [Space]
        [SerializeField] private Sprite plateUp;
        [SerializeField] private Sprite plateDown;
        
        public int PlateID { get; private set; }
        public event Action<Plate> OnPlatePressed;

        private bool _isUp = true;
        
        private Sprite _symbol;
        private Sprite _symbolActive;
        private PlatePuzzle _platePuzzle;
        private Vector3 _upPos;
        private Vector3 _downPos;

        private Tween _breathTween;
        
        public void Init(int plateID, Sprite symbolUp, Sprite symbolDown, PlatePuzzle platePuzzle)
        {
            PlateID = plateID;
            _symbol = symbolUp;
            _symbolActive = symbolDown;
            _platePuzzle = platePuzzle;

            plateSpriteRenderer.sprite = plateUp;
            symbolSpriteRenderer.sprite = _symbol;
            symbolActiveSpriteRenderer.sprite = _symbolActive;

            _upPos = symbolsVisualsT.position;
            _downPos = _upPos - new Vector3(0f, 0.05f);
        }
        
        public void BreathBlink(float halfBreathTime, float intensity01)
        {
            if (!_isUp) return;
            _breathTween = symbolActiveSpriteRenderer.DOFade(intensity01, halfBreathTime).SetEase(Ease.OutQuad).SetLoops(2, LoopType.Yoyo);
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
            _isUp = up;
            plateSpriteRenderer.sprite = up ? plateUp : plateDown;
            symbolsVisualsT.position = up ? _upPos : _downPos;
            if (!up)
            {
                _breathTween?.Kill();
                symbolActiveSpriteRenderer.color = Color.white;
            }
            else
            {
                var c = Color.white;
                c.a = 0f;
                symbolActiveSpriteRenderer.color = c;
            }

            if (_platePuzzle.IsSolved)
                symbolSpriteRenderer.sprite = _symbol;
        }

        
    }
}