using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

namespace MSVJ1.Main
{
    public class CinemaMachineManager : MonoBehaviour
    {
        private Vector2 cameraOffset = Vector2.zero;
        private CinemachineVirtualCamera virtualCamera = null;
        private CinemachineFramingTransposer transposer = null;

        private void Awake()
        {
            virtualCamera = GetComponent<CinemachineVirtualCamera>();
            transposer = virtualCamera.GetCinemachineComponent<CinemachineFramingTransposer>();
        }

        public void SetTarget(GameObject target)
        {
            virtualCamera.Follow = target.transform;
        }

        public void SetOffset(Vector2 offset)
        {
            cameraOffset = offset;
            print(cameraOffset);
            if (virtualCamera.Follow != null)
                transposer.m_TrackedObjectOffset = cameraOffset;
        }
    }
}
