using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MSVJ1.Main
{
    public class LifeController : MonoBehaviour
    {
        [SerializeField] private float maxLife = 100;
        [SerializeField] private float currentLife;

        public Action OnGetDamage;
        public Action OnDie;

        private void Awake()
        {
            currentLife = maxLife;
        }

        public void ResetLife()
        {
            currentLife = maxLife;
        }

        public void GetDamage(float damage)
        {
            OnGetDamage?.Invoke();

            currentLife -= damage;

            if (currentLife <= 0)
            {
                Invoke("Kill", 0);
            }
        }
        /*
        public void GetHeal (float heal)
        {
            if (currentLife < maxLife)
            {
                currentLife += heal;

                if (currentLife > maxLife)
                {
                    currentLife = maxLife;
                }
            }
        }
        */
        private void Kill()
        {
            OnDie?.Invoke();
            Destroy(gameObject);
        }

        public float GetCurrentLife()
        {
            return currentLife;
        }

        public float GetLifePorcentage()
        {
            return (currentLife / maxLife);
        }
    }
}