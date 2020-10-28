using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

namespace MSVJ1.Main
{
    public class CinemaMachineManager : MonoBehaviour
    {
        [SerializeField] private Transform centralPosition = null;
        [SerializeField] private Vector2 centralPositionOffset = Vector2.zero;
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

            if (virtualCamera.Follow != null)
                transposer.m_TrackedObjectOffset = cameraOffset;
        }

        public void SetDistanceView(float value)
        {
            virtualCamera.m_Lens.OrthographicSize = value;
        }

        public void SetCentralPosition()
        {
            virtualCamera.Follow = centralPosition;
            transposer.m_TrackedObjectOffset = centralPositionOffset;
        }
    }
}
