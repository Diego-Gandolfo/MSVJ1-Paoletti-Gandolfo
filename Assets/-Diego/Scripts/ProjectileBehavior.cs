using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MSVJ1.Diego
{
    public class ProjectileBehavior : MonoBehaviour
    {
        [SerializeField] private float minVelocityMagnitude = 0f;
        private Vector2 lastVelocity; // La ultima velocidad registrada
        [SerializeField] private GameObject exploteEffect; // Efecto de Particulas para la Explosion
        private bool canExplote = false; // Si puede explotar
        private Rigidbody2D rb2D; // Nuestro Rigidbody

        private void Awake()
        {
            rb2D = GetComponent<Rigidbody2D>(); // Inicializamos el Rigidbody
        }

        private void FixedUpdate()
        {
            lastVelocity = rb2D.velocity; // Capturamos la velocidad actual y la guardamos como la ultima velocidad registrada
            
            if (canExplote && rb2D.velocity.magnitude <= minVelocityMagnitude) // Si puede explotar y la magnitud de velocidad actual es menor o igual a la velocidad minima
            {
                Instantiate(exploteEffect, transform.position, transform.rotation); // Instanciar Efecto de Particulas para la Explosion
                // TODO: Hacer Daño de Proyectil
                Destroy(gameObject); // Destruir este Proyectil
            }
        }

        public void DoThrowGranade(Vector2 direction, float force) // Versión Publica del ThrowGranade
        {
            ThrowGranade(direction, force);
        }

        private void ThrowGranade(Vector2 direction, float force) // Versión Privada del ThrowGranade
        {
            rb2D.AddForce(direction * force); // Agregamos la fuerza y direccion a nuestro Rigidbody
            //rb2D.velocity = Vector2.right; // Le incicamos la Velocidad
            rb2D.velocity = (direction * force); // Le incicamos la Velocidad
            canExplote = true; // Le decimos que puede explotar cuando se cumplan las condiciones
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            var speed = lastVelocity.magnitude / 2;
            var direction = Vector2.Reflect(lastVelocity.normalized, collision.contacts[0].normal);

            rb2D.velocity = direction * speed;
        }
    }
}
