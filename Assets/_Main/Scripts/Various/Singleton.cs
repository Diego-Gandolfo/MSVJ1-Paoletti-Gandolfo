/*
 * Es un Singleton, por ende solo puede existir uno en la Escena; si ya hay un Objeto con Singleton, el Objeto nuevo se auto-destruirá
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MSVJ1.Main
{
    public class Singleton : MonoBehaviour
    {
        public static Singleton instance; // Variable donde se almacenará la primer instancia del Singleton, es una variable de su propia clase

        public void Awake() // Cuando "se despierta" (es lo primero que se ejecuta, para más info https://docs.unity3d.com/Manual/ExecutionOrder.html) nos fijamos si ya existe otro Singleton
        {
            if (instance != null)
            {
                Destroy(gameObject);
            }
            else
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
            }
        }
    }
}
