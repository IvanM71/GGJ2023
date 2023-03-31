using DG.Tweening;
using System;
using UnityEngine;

namespace Apollo11.Puzzles
{
    public class Plate : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer plateSpriteRenderer;
        [SerializeField] private SpriteRenderer symbolSpriteRenderer;
        [SerializeField] private SpriteRenderer symbolActiveSpriteRenderer;
        [Space]
        [SerializeField] private Sprite plateUp;
        [SerializeField] private Sprite plateDown;

        private Sprite _symbol;
        private Sprite _symbolActive;
        private PlatePuzzle _platePuzzle;
        private Vector2 upPos;
        private Vector2 downPos;

        public int PlateID { get; private set; }
        public event Action<Plate> OnPlatePressed;


        public void Init(int plateID, Sprite symbolUp, Sprite symbolDown, PlatePuzzle platePuzzle)
        {
            PlateID = plateID;
            _symbol = symbolUp;
            _symbolActive = symbolDown;
            _platePuzzle = platePuzzle;

            plateSpriteRenderer.sprite = plateUp;
            symbolSpriteRenderer.sprite = _symbol;
            symbolActiveSpriteRenderer.sprite = _symbolActive;

            symbolActiveSpriteRenderer.DOFade(125, 0.7f).SetLoops(-1).SetEase(Ease.InBack, 1, 10);

            upPos = new Vector2(symbolActiveSpriteRenderer.transform.position.x, symbolActiveSpriteRenderer.transform.position.y);
            downPos = new Vector2(upPos.x, upPos.y - 0.05f);
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
            symbolActiveSpriteRenderer.transform.position = up ? upPos : downPos;
            symbolSpriteRenderer.transform.position = up ? upPos : downPos;

            if (_platePuzzle.IsSolved)
                symbolSpriteRenderer.sprite = _symbol;
        }
    }
}