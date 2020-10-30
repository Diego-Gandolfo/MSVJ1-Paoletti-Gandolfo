using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MSVJ1.Main
{
    public class FreezeWorldRotation : MonoBehaviour
    {
        [SerializeField] private Vector2 fixedWorldRotation;

        private void Update()
        {
            transform.rotation = new Quaternion(fixedWorldRotation.x, fixedWorldRotation.y, 0, 1);
        }
    }
}
