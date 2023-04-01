using UnityEngine;

namespace Apollo11.WorldUI
{
    public class ProgressBar : MonoBehaviour
    {
        [SerializeField] private Transform fillObject;
        [SerializeField] private float fullSize;

        //TODO throws errors:
        /*
         * transform.localScale (or transform.localPosition) assign attempt for 'Fill' is not valid. Input localScale is { NaN, 0.090000, 1.000000 }.
UnityEngine.Transform:set_localScale (UnityEngine.Vector3)
Apollo11.WorldUI.ProgressBar:SetValue01 (single) (at Assets/Scripts/WorldUI/ProgressBar.cs:24)
Apollo11.Roots.MainRoot:TakeDamage (int) (at Assets/Scripts/Roots/MainRoot.cs:31)
Apollo11.Interaction.AttackSystem:AtAttackAnimation () (at Assets/Scripts/Interaction/AttackSystem.cs:31)
Apollo11.Player.GnomeAnimationEventsHandler:AtDealDamage () (at Assets/Scripts/Player/GnomeAnimationEventsHandler.cs:23)
         */

        public void SetValue01(float val)
        {
            var localPos = fillObject.localPosition;
            localPos.x = fullSize*val / 2f - fullSize/2f;
            fillObject.localPosition = localPos;
            
            var localScale = fillObject.localScale;
            localScale.x = fullSize * val;
            fillObject.localScale = localScale;
        }
    }
}
