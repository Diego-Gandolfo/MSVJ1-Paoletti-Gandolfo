using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MSVJ1.Main
{
    public class AutoDestroy : MonoBehaviour
    {
        [SerializeField] private float timeToDestroy = 600f; // Cuanto tiempo pasa antes de que se destruya

        private void Start()
        {
            Destroy(gameObject, timeToDestroy); // Nos destruimos cuando pasa el tiempo pasado por la variable
        }
    }
}
