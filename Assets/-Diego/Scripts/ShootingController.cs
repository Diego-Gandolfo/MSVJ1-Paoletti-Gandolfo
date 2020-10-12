using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MSVJ1.Diego
{
    public class ShootingController : MonoBehaviour
    {
        [Header("Projectile Settings")]
        [SerializeField] private ProjectileBehavior projectile = null; // Prefab del Proyectil
        [SerializeField] private Transform projectileSpawnPoint = null; // El Spawnpoint donde se Instanciara el Proyectil
        [SerializeField] private float projectileForce = 0f; // La potencia inicial con la que sale disparado el Proyectil
        [SerializeField] private float projectileForceIncrement = 0f; // Valor que se le va sumando a la potencia del disparo del Proyectil
        private float currentForce = 0f; // Donde iremos almacenando la potencia actual acumulada

        private void Update()
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition); // Almacenamos las coordenadas de donde se encuentra el puntero del Mouse

            Vector2 direction = new Vector2(mousePosition.x - transform.position.x, mousePosition.y - transform.position.y); // Calculamos la dirección a la que hay que mirar

            transform.right = direction; // Actualizamos el Transform para que mire al puntero del Mouse

            if (Input.GetKeyDown(KeyCode.Space)) // Al presionar
                currentForce = projectileForce; // Inicializamos currentForce

            if (Input.GetKey(KeyCode.Space)) // Al mantener presionado
                currentForce += projectileForceIncrement; // Incrementamos la currentForce

            if (Input.GetKeyUp(KeyCode.Space)) // Al soltar
            {
                ProjectileBehavior projectileClone = Instantiate(projectile, projectileSpawnPoint.position, projectileSpawnPoint.rotation); // Instanciamos el Proyectil
                projectileClone.DoThrowGranade(transform.right, currentForce); // Le pasamos la direccion y la potencia con la que tiene que ser lanzado
            }
        }
    }
}
