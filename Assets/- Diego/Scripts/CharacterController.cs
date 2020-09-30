using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MSVJ1.Diego
{
    public class CharacterController : MonoBehaviour
    {
        [SerializeField] private float speed;

        private void Update()
        {
            transform.position += transform.right * (speed * Time.deltaTime);
        }
    }
}
