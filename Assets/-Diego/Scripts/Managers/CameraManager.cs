using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MSVJ1.Diego
{
    public class CameraManager : MonoBehaviour
    {
        [Header("Camera Settings")]
        [SerializeField] private Vector3 cameraOffset = Vector2.zero;
        private Camera mainCamera = null;
        private GameObject currentTarget;

        private void Awake()
        {
            mainCamera = Camera.main;
        }

        private void Update()
        {
            if (currentTarget != null) transform.position = currentTarget.transform.position + cameraOffset;
        }

        public void MoveCamera(GameObject target)
        {
            currentTarget = target;
        }
    }
}
