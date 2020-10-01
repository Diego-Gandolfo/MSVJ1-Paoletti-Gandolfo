using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MSVJ1.Diego
{
    public class MovimientoContinuo : MonoBehaviour
    {
        [SerializeField] private Vector3 speed = Vector3.zero;

        private void Update()
        {
            transform.Translate(speed * Time.deltaTime);
        }
    }
}
