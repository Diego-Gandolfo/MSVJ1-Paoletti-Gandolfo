using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MSVJ1.Main
{
    public class ConstantTranslate : MonoBehaviour
    {
        [SerializeField] private Vector3 speed = Vector3.zero; // Para poder asignar una velocidad en cualquiera de los 3 ejes

        private void Update()
        {
            transform.Translate(speed * Time.deltaTime); // Movemos el objeto
        }
    }
}
