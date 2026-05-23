using Unity.Cinemachine;
using UnityEngine;

namespace MixingCameraControl
{
    public class ControlledEntityVirtualCameraContainer : MonoBehaviour
    {
        [SerializeField] private CinemachineVirtualCameraBase virtualCamera;
        [SerializeField] private bool setCameraActiveOnStart = false;
        
        public CinemachineVirtualCameraBase VirtualCamera => virtualCamera;
        public bool SetCameraActiveOnStart => setCameraActiveOnStart;
    }
}