using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


namespace MSVJ1.Main
{
    public class ProjectileBehavior : MonoBehaviour
    {
        [Header("Explotion by Velocity")]
        [SerializeField] private float minVelocityMagnitude = 0f; // A que velocidad de Velocity va a Explotar, si Velocity está por debajo de este valor Explota
        private Vector2 lastVelocity = Vector2.zero; // La ultima velocidad registrada

        [Header("Explotion by Time")]
        [SerializeField] private float explosionTime = 0f;
        private float currentExplosionTime = 0f;

        private bool canExplote = false; // Si puede explotar
        
        [Header("Particle Effect")]
        [SerializeField] private GameObject exploteEffect = null; // Efecto de Particulas para la Explosion
        [SerializeField] private GameObject projectileReflectEffect = null;

        [Header("Physics")]
        [SerializeField] private float inertia = 0f;
        [SerializeField] private float friction = 0f;
        private Rigidbody2D rb2D = null; // Nuestro Rigidbody


        [Header("Canvas")]
        [SerializeField] private Canvas canvas;
        [SerializeField] private TextMeshProUGUI text;


        public Action OnProjectileExplotion2;
        public Action OnProjectileReflect;

        private void Awake()
        {
            rb2D = GetComponent<Rigidbody2D>(); // Inicializamos el Rigidbody
            canvas.worldCamera = Camera.main;
        }

        void Update() // Esto es lo que traje del Script de Dante
        {
            currentExplosionTime += Time.deltaTime;

            int intTimer = (int)(explosionTime - currentExplosionTime) + 1; // Acá guarado en un int el valor entero de timer y le sumo 1, para que la cuenta no termine en 0 en el Canvas
            text.text = intTimer.ToString(); // Ponemos en el Canvas el valor de intTimer

            if (currentExplosionTime >= explosionTime)
            {
                Instantiate(exploteEffect, transform.position, transform.rotation); // Instanciar Efecto de Particulas para la Explosion
                OnProjectileExplotion2.Invoke();
            }
        }

        private void FixedUpdate()
        {
            if (canExplote && Mathf.Abs(rb2D.velocity.magnitude) <= minVelocityMagnitude && Mathf.Abs(lastVelocity.magnitude) <= minVelocityMagnitude) // Si puede explotar y la magnitud absoluta de velocidad actual y de la ultima velocidad es menor o igual a la velocidad minima
            {
                Instantiate(exploteEffect, transform.position, transform.rotation); // Instanciar Efecto de Particulas para la Explosion
                OnProjectileExplotion2.Invoke();
                //Destroy(gameObject);
            }

            lastVelocity = rb2D.velocity; // Capturamos la velocidad actual y la guardamos como la ultima velocidad registrada
        }

        public void DoShootProjectile(Vector2 direction, float force) // Versión Publica del ThrowGranade
        {
            ShootProjectile(direction, force);
        }

        private void ShootProjectile(Vector2 direction, float force) // Versión Privada del ThrowGranade
        {
            rb2D.AddForce(direction * force); // Agregamos la fuerza y direccion a nuestro Rigidbody
            //rb2D.velocity = Vector2.right; // Le incicamos la Velocidad
            rb2D.velocity = (direction * force); // Le incicamos la Velocidad
            canExplote = true; // Le decimos que puede explotar cuando se cumplan las condiciones
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            var speed = inertia == 0 ? 0 : lastVelocity.magnitude / inertia; // Almacenamos en speed la mitad de la magnitud de la Velocity actual
            var direction = Vector2.Reflect(lastVelocity.normalized, collision.contacts[0].normal); // Calculamos la nueva dirección

            rb2D.velocity = direction * speed; // Hacemos que la velocity sea igual a la nueva dirección * speed

            //Instantiate(projectileReflectEffect, transform.position, Quaternion.identity); // No me gusto como quedo el Efecto

            //OnProjectileReflect?.Invoke(); // No me gusto como quedo el sonido
        }

        private void OnCollisionStay2D(Collision2D collision)
        {
            rb2D.velocity = new Vector2(rb2D.velocity.x / friction, rb2D.velocity.y);
        }
    }
}
