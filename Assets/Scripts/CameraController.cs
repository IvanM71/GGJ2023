using Apollo11.Core;
using Cinemachine;
using UnityEngine;

namespace Apollo11
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private CinemachineVirtualCamera mainVCam;

        private void Start()
        {
            mainVCam.Follow = SystemsLocator.Inst.PlayerSystems.cameraFollowPoint;
        }
    }
}
