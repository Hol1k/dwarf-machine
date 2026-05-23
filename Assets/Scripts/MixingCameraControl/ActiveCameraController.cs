using System.Collections;
using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;
using Zenject;

namespace MixingCameraControl
{
    [RequireComponent(typeof(CinemachineMixingCamera))]
    public class ActiveCameraController : MonoBehaviour
    {
        [SerializeField] private float cameraChangeAnimationDuration = 0.1f;
        
        private CinemachineMixingCamera _mixingCamera;
        private CinemachineVirtualCameraBase _activeCamera;

        [Inject]
        private void Init(List<ControlledEntityVirtualCameraContainer> virtualCameraContainers)
        {
            _mixingCamera = GetComponent<CinemachineMixingCamera>();

            bool isActiveCameraSet = false;
            foreach (var virtualCameraContainer in virtualCameraContainers)
            {
                var targetCamera = virtualCameraContainer.VirtualCamera;
                if (!targetCamera)
                {
                    Debug.LogError($"Внимание! ControlledEntityVirtualCameraContainer на объекте '{virtualCameraContainer.name}' не имеет привязанной Virtual Camera!");
                    continue;
                }
                
                targetCamera.transform.SetParent(_mixingCamera.transform);
                
                if (virtualCameraContainer.SetCameraActiveOnStart)
                {
                    ForceChangeCamera(targetCamera);
                    isActiveCameraSet = true;
                }
            }
            
            if (!isActiveCameraSet & _mixingCamera.ChildCameras?.Count > 0)
                ForceChangeCamera(_mixingCamera.ChildCameras[0]);
        }

        public void SetActiveCamera(CinemachineVirtualCameraBase virtualCamera)
        {
            StartCoroutine(SmoothChangeCamera(virtualCamera, cameraChangeAnimationDuration));
        }

        private void ForceChangeCamera(CinemachineVirtualCameraBase targetCamera)
        {
            if (!_activeCamera)
            {
                foreach (var currCamera in _mixingCamera.ChildCameras)
                {
                    _mixingCamera.SetWeight(currCamera, 0);
                }
                
                _mixingCamera.SetWeight(targetCamera, 1);
                _activeCamera = targetCamera;
                return;
            }
            
            _mixingCamera.SetWeight(_activeCamera, 0);
            _mixingCamera.SetWeight(targetCamera, 1);
            
            _activeCamera = targetCamera;
        }
        
        private IEnumerator SmoothChangeCamera(CinemachineVirtualCameraBase targetCamera, float duration)
        {
            if (!_activeCamera)
            {
                foreach (var currCamera in _mixingCamera.ChildCameras)
                {
                    _mixingCamera.SetWeight(currCamera, 0);
                }
                
                _mixingCamera.SetWeight(targetCamera, 1);
                _activeCamera = targetCamera;
                yield break;
            }
            
            var currentTime = 0f;

            while (currentTime < duration)
            {
                currentTime += Time.deltaTime;
                
                var smoothValue = currentTime / duration;
                _mixingCamera.SetWeight(_activeCamera, 1 - smoothValue);
                _mixingCamera.SetWeight(targetCamera, smoothValue);
                
                yield return null;
            }
            
            _mixingCamera.SetWeight(_activeCamera, 0);
            _mixingCamera.SetWeight(targetCamera, 1);
            
            _activeCamera = targetCamera;
        }
    }
}