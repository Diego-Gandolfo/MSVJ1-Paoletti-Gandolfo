using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MSVJ1.Diego
{
    public class ShootingController : MonoBehaviour
    {
        [SerializeField] private Transform spawnpoint = null;
        [SerializeField] private GameObject prefabProyectile = null;

        private void Update()
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition); // Almacenamos las coordenadas de donde se encuentra el puntero del Mouse

            Vector2 direction = new Vector2(mousePosition.x - transform.position.x, mousePosition.y - transform.position.y); // Calculamos la dirección a la que hay que mirar

            transform.right = direction; // Actualizamos el Transform para que mire al puntero del Mouse

            if (Input.GetKeyDown(KeyCode.Space))
            {
                Instantiate(prefabProyectile, spawnpoint.position, transform.rotation);
            }
        }
    }
}
