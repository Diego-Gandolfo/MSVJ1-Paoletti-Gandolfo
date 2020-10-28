using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MSVJ1.Main
{
    public class ShootingController : MonoBehaviour
    {
        [Header("Projectile Settings")]
        [SerializeField] private ProjectileBehavior projectile = null; // Prefab del Proyectil
        [SerializeField] private Transform projectileSpawnPoint = null; // El Spawnpoint donde se Instanciara el Proyectil
        [SerializeField] private float projectileForce = 0f; // La potencia inicial con la que sale disparado el Proyectil
        [SerializeField] private float projectileForceIncrement = 0f; // Valor que se le va sumando a la potencia del disparo del Proyectil
        private float currentForce = 0f; // Donde iremos almacenando la potencia actual acumulada
        [HideInInspector] public bool doneShoot = false;
        [HideInInspector] public bool doneExplotion = false;

        [Header("Camera Settings")]
        //[SerializeField] private CameraManager cameraManager = null;
        [SerializeField] private CinemaMachineManager cinemaManager = null;
        [SerializeField] private Vector2 offsetCamera = Vector2.zero;

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
                doneShoot = true;
                ProjectileBehavior projectileClone = Instantiate(projectile, projectileSpawnPoint.position, projectileSpawnPoint.rotation); // Instanciamos el Proyectil
                //cameraManager.MoveCamera(projectileClone.gameObject);
                cinemaManager.MoveCamera(projectileClone.gameObject);
                //cinemaManager.SetOffset(offsetCamera);
                //cinemaManager.SetOffset(new Vector2(offsetCamera.x, offsetCamera.y));
                cinemaManager.SetOffset(new Vector2(0, 0));
                projectileClone.DoShootProjectile(transform.right, currentForce); // Le pasamos la direccion y la potencia con la que tiene que ser lanzado
                projectileClone.OnProjectileExplotion2 += OnProjectileExplotionHandler2;
                doneExplotion = false;
            }
        }

        private void OnProjectileExplotionHandler2()
        {
            doneExplotion = true;
        }

        public bool GetHasExploted()
        {
            return doneExplotion;
        }

        public void SetHasExploted(bool value)
        {
            doneExplotion = value;
        }
    }
}
