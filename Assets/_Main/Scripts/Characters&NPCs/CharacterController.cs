using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MSVJ1.Main
{
    public class CharacterController : MonoBehaviour
    {
        [SerializeField] private float movementSpeed = 3f; // Velocidad de Movimiento
        private Rigidbody2D rb2D = null; // Nuestro Rigidbody

        [SerializeField] private SoundManager soundManager = null;
        [SerializeField] private GameObject dustEffect = null;
        private bool doOnce = true;

        private void Awake()
        {
            rb2D = GetComponent<Rigidbody2D>(); // Inicializamos el Rigidbody
        }

        private void OnEnable()
        {
            doOnce = true;
        }

        private void FixedUpdate()
        {
            // Movement
            float xMovement = Input.GetAxis("Horizontal") * (movementSpeed); // Tomamos el movimiento en X
            rb2D.velocity = new Vector2(xMovement, rb2D.velocity.y); // Igualamos nuestra velocidad al movimiento en X

            if (xMovement != 0f)
            {
                rb2D.gravityScale = 2;
                Instantiate(dustEffect, transform.position, transform.rotation);

                if (doOnce)
                {
                    soundManager.PlaySound("playerMove");
                    doOnce = false;
                }
            }
            else if (!doOnce)
            {
                soundManager.PlaySound("playerStop");
                doOnce = true;
                ReduceGravity();
            }

            // Hay dos pequeños problemas si ponemos superficies inclinadas:
            // 1) Si se detiene a mitad de la subida al dejar de moverse se va deslizando hacia abajo
            // 2) Si quiere desender, en lugar de deslizarse sale "flotando" en el Eje X
        }

        public void ReduceGravity()
        {
            rb2D.velocity = Vector2.zero;
            rb2D.gravityScale = 0.01f;
        }
    }
}
