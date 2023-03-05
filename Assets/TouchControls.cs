using System;
using SimpleInputNamespace;
using UnityEngine;
using UnityEngine.UI;

namespace Apollo11
{
    public class TouchControls : MonoBehaviour
    {
        [SerializeField] private Joystick joystick;
        [SerializeField] private ButtonWithDownEvent buttonE;
        [SerializeField] private ButtonWithDownEvent buttonF;
        [SerializeField] private ButtonWithDownEvent buttonR;

        public Joystick Joystick => joystick;
        public event Action OnEPress;
        public event Action OnFPress;
        public event Action OnRPress;

        private void Awake()
        {
            buttonE.OnPressDown += InvokePressE;
            buttonF.OnPressDown += InvokePressF;
            buttonR.OnPressDown += InvokePressR;
        }

        private void InvokePressE() => OnEPress?.Invoke();
        private void InvokePressF() => OnFPress?.Invoke();
        private void InvokePressR() => OnRPress?.Invoke();

        private void OnDestroy()
        {
            buttonE.OnPressDown -= InvokePressE;
            buttonF.OnPressDown -= InvokePressF;
            buttonR.OnPressDown -= InvokePressR;
        }
    }
}
