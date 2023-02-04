using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace Apollo11
{
    public class PlayerDamageIndication : MonoBehaviour
    {
        [SerializeField] private Color damageColor = Color.red;
        [SerializeField] private float indicationDuration = 0.2f;
        

        [SerializeField] private List<SpriteRenderer> sprites;


        private Sequence _sequence;

        public void Blink()
        {
            _sequence?.Kill();

            foreach (var sr in sprites)
                sr.color = Color.white;
            SetColors(damageColor, indicationDuration * 0.1f);
            
            _sequence = DOTween.Sequence();
            _sequence.AppendInterval(indicationDuration*0.4f);
            _sequence.Append(SetColors(Color.white, indicationDuration*0.5f));
        }


        private Tween SetColors(Color c, float time)
        {
            Tween t = null;
            foreach (var sr in sprites)
            {
                t = sr.DOColor(c, time);
            }

            return t;
        }
    }
}
