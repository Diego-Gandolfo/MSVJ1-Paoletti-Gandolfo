using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MSVJ1.Main
{
    public class Granada : MonoBehaviour
    {
        [SerializeField] private float damage;
        [SerializeField] private float explosionTime; // Hay que asegurarse de que el tiempo sea solo por si la bala pasa demasiado rebotando o cayendo
        [SerializeField] private float explosionRadius;
        [SerializeField] private float explosionIntensity;
        [SerializeField] private LayerMask layerMask;
    
        private float currentExplosionTime;
        private ProjectileBehavior projectileBehavior = null;

        private void Awake()
        {
            projectileBehavior = GetComponent<ProjectileBehavior>();
            projectileBehavior.OnProjectileExplotion2 += Explosion;
        }
        /*
        void Update() // Comento el Update porque el tiempo se hace en el otro Script
        {
            currentExplosionTime += Time.deltaTime;

            if(currentExplosionTime >= explosionTime)
            {
                Explosion();
            }
        }
        */
        private void Explosion()
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll((Vector2)transform.position, explosionRadius, layerMask);

            foreach (var collider in colliders)
            {
                Rigidbody2D rigidbody = collider.gameObject.GetComponent<Rigidbody2D>();

                if(rigidbody != null)
                {
                    Vector3 direction = collider.transform.position - transform.position;
                    float distance = direction.magnitude;
                    direction.Normalize();
                    //Mientras mas lejos estan de la explosion, menos  impulso reciben...
                    rigidbody.AddForce((direction * explosionIntensity) / distance, ForceMode2D.Impulse);
                }

                LifeController lifeController = collider.GetComponent<LifeController>();

                if(lifeController != null)
                {
                    float distance = Vector3.Distance(collider.transform.position, transform.position);
                    lifeController.GetDamage(Mathf.Abs(explosionRadius - distance) * (damage / explosionRadius));
                    //lifeController.GetDamage((damage * (Vector3.Distance(collider.transform.position, transform.position))) / explosionRadius );
                    //lifeController.GetDamage(damage / (Vector3.Distance ( collider.transform.position, transform.position)));
                }
            }    

            Destroy(gameObject);
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere((Vector2)transform.position, explosionRadius);
        }
    }
}
