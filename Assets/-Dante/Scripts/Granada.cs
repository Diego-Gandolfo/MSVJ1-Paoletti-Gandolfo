using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MSVJ1.Dante
{
public class Granada : MonoBehaviour
{
    [SerializeField] private float damage;
    [SerializeField] private float explosionTime;
    [SerializeField] private float explosionRadius;
    [SerializeField] private float explosionIntensity;
     [SerializeField] private LayerMask layerMask;
    

    


    private float currentExplosionTime;
        void Update()
        {
            currentExplosionTime += Time.deltaTime;

            if(currentExplosionTime >= explosionTime)
            {
                Explosion();
            }
        }

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
                    Destroy(gameObject);
                }
                LifeController lifeController = collider.GetComponent<LifeController>();
                if(lifeController != null)
                {
                    lifeController.GetDamage((damage * (Vector3.Distance(collider.transform.position, transform.position))) / explosionRadius );
                    if (collider.gameObject.CompareTag("Player"))
                    {
                        print("Distance: " + Vector3.Distance(collider.transform.position, transform.position));
                        print("Distance * Damage: " + damage * Vector3.Distance(collider.transform.position, transform.position));
                        print("(Distance * Damage) / radio: " + (damage * (Vector3.Distance(collider.transform.position, transform.position))) / explosionRadius);
                    }
                    //lifeController.GetDamage(damage / (Vector3.Distance ( collider.transform.position, transform.position)));
                }
        }    

        }
}
}