using Apollo11.Core;
using UnityEngine;

namespace Apollo11
{
    public class GnomeAnimationEventsHandler : MonoBehaviour
    {
        public void AtAttackDone()
        {
            print("AtAttackDone");
            if (SystemsLocator.Inst.InteractionSystem.InAttack)
            {
                SystemsLocator.Inst.PlayerSystems.PlayerAnimation.StopHandWeapon();
                SystemsLocator.Inst.InteractionSystem.InAttack = false;
            }
           
            
        }
    }
}
