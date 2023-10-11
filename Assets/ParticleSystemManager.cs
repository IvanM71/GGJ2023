using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Apollo11
{
    public class ParticleSystemManager : MonoBehaviour
    {
        [SerializeField] private ParticleSystem particles;

        private Color redLeft = new Color(64f, 8f, 12f);
        private Color redRight = new Color(179f, 39f, 57f);

        private Color blueLeft = new Color(1f, 25f, 75f);
        private Color blueRight = new Color(40f, 115f, 220f);

        private Color purpleLeft = new Color(45f, 4f, 83f);
        private Color purpleRight = new Color(152f, 62f, 255f);

        public void ActivateParticles(Enums.RootWeapon type)
        {
            #region trash

            //var startColor = particles.main.startColor;
            //var main = particles.main;
            //var particlesLocal = particles;

            //Color left, right;

            //switch(type)
            //{
            //    case Enums.RootWeapon.Axe:
            //        left = redLeft;
            //        right = redRight;
            //        break;
            //    case Enums.RootWeapon.Saw:
            //        left = blueLeft;
            //        right = blueRight;
            //        break;
            //    case Enums.RootWeapon.Sprayer:
            //        left = purpleLeft;
            //        right = purpleRight;
            //        break;
            //    default:
            //        Debug.Log("Unknown weapon");
            //        return;
            //}

            //SerializedObject so = new SerializedObject(particles);
            //if ((so.FindProperty("main.startColor.minColor") != null) && (so.FindProperty("main.startColor.maxColor") != null))
            //{
            //    so.FindProperty("main.startColor.minColor").colorValue = left;

            //    so.FindProperty("main.startColor.maxColor").colorValue = right;

            //    so.ApplyModifiedProperties();
            //}
            // var colorOverLifetime = particles.colorOverLifetime;
            //switch (type)
            //{
            //    case Enums.RootWeapon.Axe:
            //        //particlesLocal.startColor = redLeft;
            //        //startColor = new ParticleSystem.MinMaxGradient(redLeft, redRight);
            //        //startColor = redLeft;
            //        //particles.startColor = redLeft;
            //        //main.startColor = redLeft;
            //        //main.startColor = redLeft;
            //        //startColor.mode = ParticleSystemGradientMode.RandomColor;
            //        //colorOverLifetime.color = new ParticleSystem.MinMaxGradient(redLeft, redRight);
            //        break;
            //    case Enums.RootWeapon.Saw:
            //        //particlesLocal.startColor = blueLeft;
            //        //startColor = new ParticleSystem.MinMaxGradient(blueLeft, blueRight);
            //        //startColor = blueLeft;
            //        //particles.startColor = blueLeft;
            //        //main.startColor = blueLeft;
            //        //startColor.mode = ParticleSystemGradientMode.RandomColor;
            //        //colorOverLifetime.color = new ParticleSystem.MinMaxGradient(blueLeft, blueRight);
            //        break;
            //    case Enums.RootWeapon.Sprayer:
            //        //particlesLocal.startColor = purpleLeft;
            //        //startColor = new ParticleSystem.MinMaxGradient(purpleLeft, purpleRight);
            //        //startColor = purpleLeft;
            //        //particles.startColor = purpleLeft;
            //        //main.startColor = purpleLeft;
            //        //startColor.mode = ParticleSystemGradientMode.RandomColor;
            //        //colorOverLifetime.color = new ParticleSystem.MinMaxGradient(purpleLeft, purpleRight);
            //        break;
            //    default:
            //        Debug.Log("Unknown weapon");
            //        return;
            //}

            #endregion

            particles.Play();
        }
    }
}
