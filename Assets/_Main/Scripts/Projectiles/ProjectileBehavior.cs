using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MSVJ1.Main
{
    public class ProjectileBehavior : MonoBehaviour
    {
        [SerializeField] private float minVelocityMagnitude = 0f; // A que velocidad de Velocity va a Explotar, si Velocity está por debajo de este valor Explota
        private Vector2 lastVelocity = Vector2.zero; // La ultima velocidad registrada
        [SerializeField] private GameObject exploteEffect = null; // Efecto de Particulas para la Explosion
        private bool canExplote = false; // Si puede explotar
        private Rigidbody2D rb2D = null; // Nuestro Rigidbody
        [SerializeField] private float inertia = 0f;

        public Action OnProjectileExplotion2;

        private void Awake()
        {
            rb2D = GetComponent<Rigidbody2D>(); // Inicializamos el Rigidbody
        }

        private void FixedUpdate()
        {
            if (canExplote && Mathf.Abs(rb2D.velocity.magnitude) <= minVelocityMagnitude && Mathf.Abs(lastVelocity.magnitude) <= minVelocityMagnitude) // Si puede explotar y la magnitud absoluta de velocidad actual y de la ultima velocidad es menor o igual a la velocidad minima
            {
                Instantiate(exploteEffect, transform.position, transform.rotation); // Instanciar Efecto de Particulas para la Explosion
                // TODO: Hacer Daño de Proyectil
                OnProjectileExplotion2.Invoke();
                Destroy(gameObject); // Destruir este Proyectil
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
        }
    }
}
