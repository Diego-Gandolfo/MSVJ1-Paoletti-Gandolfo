using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MSVJ1.Main
{
    public class Parallax : MonoBehaviour
    {
        [SerializeField, Range(0f,1f)] private float parallaxEffectMultiplier = 0f;
        [SerializeField] private int numberOfCopys = 0;

        private Transform cameraTransform = null;
        private Vector3 lastCameraPosition = Vector3.zero;
        private float width = 0f;
        private SpriteRenderer spriteRenderer = null;

        private void Start()
        {
            cameraTransform = Camera.main.transform;
            lastCameraPosition = cameraTransform.position;
            spriteRenderer = GetComponent<SpriteRenderer>();
            width = spriteRenderer.bounds.size.x;
        }

        private void FixedUpdate() // Lo cambie al FixedUpdate porque en el Update glitcheaba
        {
            Vector3 deltaMovement = cameraTransform.position - lastCameraPosition;
            transform.position += new Vector3(deltaMovement.x * parallaxEffectMultiplier, 0f, 0f);
            lastCameraPosition = cameraTransform.position;

            float distanceWithCamera = cameraTransform.position.x - transform.position.x;

            if (Mathf.Abs((distanceWithCamera / (numberOfCopys / 2))) > width) // Le saqué el igual, a ver si con esto se arregla
            {
                float movement = (distanceWithCamera / (numberOfCopys / 2)) > 0 ? width * numberOfCopys : width * -numberOfCopys;

                transform.position += new Vector3(movement, 0f, 0f);
            }
        }
    }
}
