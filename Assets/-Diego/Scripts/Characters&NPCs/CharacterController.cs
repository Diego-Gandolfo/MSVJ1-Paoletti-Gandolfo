using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MSVJ1.Diego
{
    public class CharacterController : MonoBehaviour
    {
        [SerializeField] private float speed = 0f;

        private void Update()
        {
            transform.Translate(Input.GetAxis("Horizontal") * (speed * Time.deltaTime), 0, 0);
        }
    }
}
