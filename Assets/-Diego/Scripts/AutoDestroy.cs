using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MSVJ1.Diego
{
    public class AutoDestroy : MonoBehaviour
    {
        [SerializeField] private float timeToDestroy = 600f;

        private void Start()
        {
            Destroy(gameObject, timeToDestroy);
        }
    }
}
