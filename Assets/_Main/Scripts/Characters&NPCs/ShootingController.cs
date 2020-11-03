using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

namespace MSVJ1.Main
{
    public class ShootingController : MonoBehaviour
    {
        [Header("Projectile Settings")]
        [SerializeField] private ProjectileBehavior projectile = null; // Prefab del Proyectil
        [SerializeField] private Transform projectileSpawnPoint = null; // El Spawnpoint donde se Instanciara el Proyectil
        [SerializeField] private LineRenderer lineRenderer = null;
        [SerializeField] private GameObject shootEffect = null;
        [SerializeField] private float projectileForce = 0f; // La potencia inicial con la que sale disparado el Proyectil
        [SerializeField] private float projectileForceIncrement = 0f; // Valor que se le va sumando a la potencia del disparo del Proyectil
        private float currentForce = 0f; // Donde iremos almacenando la potencia actual acumulada
        [HideInInspector] public bool doneShoot = false;
        [HideInInspector] public bool doneExplotion = false;

        [Header("Camera Settings")]
        [SerializeField] private CinemaMachineManager cinemaManager = null;
        [SerializeField] private Vector2 offsetCamera = Vector2.zero;
        [SerializeField] private float projectileDistanceView = 0f;

        [Header("SoundManager")]
        [SerializeField] private SoundManager soundManager = null;

        [Header("Canvas Settings")]
        [SerializeField] private Text textText = null; // El Texto del Canvas que usamos para los Titulos o Mensajes
        [SerializeField] private Text textNumber = null; // El Texto del Canvas que usamos para el Contador

        private void Awake()
        {
            if (cinemaManager == null) Debug.LogError($"{gameObject.transform.parent.transform.parent.name}.{gameObject.transform.parent.name}.{gameObject.name} no tiene CinemaMachineManager<CM vcam1> asignado en ShootingController");
        }

        private void Update()
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition); // Almacenamos las coordenadas de donde se encuentra el puntero del Mouse

            Vector2 direction = new Vector2(mousePosition.x - transform.position.x, mousePosition.y - transform.position.y); // Calculamos la dirección a la que hay que mirar

            transform.right = direction; // Actualizamos el Transform para que mire al puntero del Mouse

            /*
            if (direction.normalized.y > ((transform.parent.transform.parent.rotation.normalized.z * 2) - 0f))
                transform.right = direction; // Actualizamos el Transform para que mire al puntero del Mouse
            if (direction.normalized.y > ((transform.parent.transform.parent.rotation.normalized.z * -2) - 0f))
                transform.right = direction; // Actualizamos el Transform para que mire al puntero del Mouse
            */
            /*
            if (direction.normalized.y > 0f && transform.parent.transform.parent.rotation.normalized.z > 0)
                transform.right = direction + new Vector2(0, -transform.parent.transform.parent.rotation.eulerAngles.z * 2); // Actualizamos el Transform para que mire al puntero del Mouse
            if (direction.normalized.y > 0f && transform.parent.transform.parent.rotation.normalized.z < 0)
                transform.right = direction + new Vector2(0, transform.parent.transform.parent.rotation.eulerAngles.z * 2); // Actualizamos el Transform para que mire al puntero del Mouse
            */
            /*
            if (transform.rotation.eulerAngles.z > 180)
                transform.rotation.Euler(0, 0, 180);
            */
            if (Input.GetKeyDown(KeyCode.Space)) // Al presionar
            {
                currentForce = projectileForce; // Inicializamos currentForce
                SetLineRenderer(currentForce / 5);
                //lineRenderer.SetPosition(1, new Vector3(currentForce / 5, 0, 0));
            }

            if (Input.GetKey(KeyCode.Space)) // Al mantener presionado
            {
                currentForce += projectileForceIncrement; // Incrementamos la currentForce
                SetLineRenderer(currentForce / 5);
                //lineRenderer.SetPosition(1, new Vector3(currentForce / 5, 0, 0));
            }

            if (Input.GetKeyUp(KeyCode.Space)) // Al soltar
            {
                SetLineRenderer(0);
                //lineRenderer.SetPosition(1, new Vector3(0, 0, 0));
                Instantiate(shootEffect, transform.position, transform.rotation);
                doneShoot = true;
                ProjectileBehavior projectileClone = Instantiate(projectile, projectileSpawnPoint.position, projectileSpawnPoint.rotation); // Instanciamos el Proyectil
                cinemaManager.SetTarget(projectileClone.gameObject);
                cinemaManager.SetOffset(offsetCamera);
                cinemaManager.SetDistanceView(projectileDistanceView);
                projectileClone.DoShootProjectile(transform.right, currentForce); // Le pasamos la direccion y la potencia con la que tiene que ser lanzado
                projectileClone.OnProjectileExplotion2 += OnProjectileExplotionHandler2;
                projectileClone.OnProjectileReflect += OnProjectileReflectHandler;
                projectileClone.AsignText(textText, textNumber);
                doneExplotion = false;
            }
        }

        private void OnProjectileReflectHandler()
        {
            soundManager.PlaySound("proyectileReflect");
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

        public void SetLineRenderer(float power)
        {
            lineRenderer.SetPosition(1, new Vector3(power, 0, 0));
        }

        public void ResetDirection()
        {
            transform.right = gameObject.transform.parent.transform.parent.name == "Player1" ? new Vector3(1, 0, 0) : new Vector3(-1, 0, 0);
        }
    }
}
