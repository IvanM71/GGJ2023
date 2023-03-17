using Apollo11.UI;
using SimpleInputNamespace;
using UnityEngine;

namespace Apollo11.Core
{
    /// <summary>
    /// This logic is shit, I am sorry
    /// </summary>
    public class TouchControls : MonoBehaviour
    {
        [SerializeField] private Joystick joystick;
        [SerializeField] private ButtonWithDownEvent buttonE;
        [SerializeField] private ButtonWithDownEvent buttonF;
        [SerializeField] private ButtonWithDownEvent buttonR;

        public Joystick Joystick => joystick;

        public bool PressedEThisFrame { get; private set; }
        public bool PressedFThisFrame { get; private set; }
        public bool PressedRThisFrame { get; private set; }
        
        public bool ReleasedEThisFrame { get; private set; }
        public bool ReleasedFThisFrame { get; private set; }
        public bool ReleasedRThisFrame { get; private set; }

        private bool PressedE => buttonE.Pressed;
        private bool PressedF => buttonF.Pressed;
        private bool PressedR => buttonR.Pressed;

        private void Awake()
        {
            buttonE.OnPress += AtPressE;
            buttonF.OnPress += AtPressF;
            buttonR.OnPress += AtPressR;
            
            buttonE.OnRelease += AtReleaseE;
            buttonF.OnRelease += AtReleaseF;
            buttonR.OnRelease += AtReleaseR;
        }
        
        private void OnDestroy()
        {
            buttonE.OnPress -= AtPressE;
            buttonF.OnPress -= AtPressF;
            buttonR.OnPress -= AtPressR;
            
            buttonE.OnRelease -= AtReleaseE;
            buttonF.OnRelease -= AtReleaseF;
            buttonR.OnRelease -= AtReleaseR;
        }

        private void AtPressE() => PressedEThisFrame = true;
        private void AtPressF() => PressedFThisFrame = true;
        private void AtPressR() => PressedRThisFrame = true;

        private void AtReleaseE() => ReleasedEThisFrame = true;
        private void AtReleaseF() => ReleasedFThisFrame = true;
        private void AtReleaseR() => ReleasedRThisFrame = true;

        public void ResetFlags()
        {
            PressedEThisFrame = false;
            PressedFThisFrame = false;
            PressedRThisFrame = false;
            ReleasedEThisFrame = false;
            ReleasedFThisFrame = false;
            ReleasedRThisFrame = false;
        }
    }
}
