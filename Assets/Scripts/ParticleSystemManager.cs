using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Apollo11
{
    public class ParticleSystemManager : MonoBehaviour
    {
        [SerializeField] private ParticleSystem particleAxe;
        [SerializeField] private ParticleSystem particleSaw;
        [SerializeField] private ParticleSystem particleSprayer;

        public void ActivateParticles(Enums.RootWeapon type)
        {
            switch(type)
            {
                case Enums.RootWeapon.Axe:
                    particleAxe.Play(); break;
                case Enums.RootWeapon.Saw:
                    particleSaw.Play(); break;
                case Enums.RootWeapon.Sprayer:
                    particleSprayer.Play(); break;
                default:
                    Debug.Log("Unknown type"); break;
            }
        }
    }
}
