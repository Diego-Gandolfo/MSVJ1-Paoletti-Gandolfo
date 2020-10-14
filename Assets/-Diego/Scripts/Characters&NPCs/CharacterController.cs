using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MSVJ1.Diego
{
    public class CharacterController : MonoBehaviour
    {
        [SerializeField] private float movementSpeed = 0f; // Velocidad de Movimiento
        private Rigidbody2D rb2D = null; // Nuestro Rigidbody

        private void Awake()
        {
            rb2D = GetComponent<Rigidbody2D>(); // Inicializamos el Rigidbody
        }

        private void FixedUpdate()
        {
            // Movement
            float xMovement = Input.GetAxis("Horizontal") * (movementSpeed); // Tomamos el movimiento en X
            rb2D.velocity = new Vector2(xMovement, rb2D.velocity.y); // Igualamos nuestra velocidad al movimiento en X

            // Hay dos pequeños problemas si ponemos superficies inclinadas:
            // 1) Si se detiene a mitad de la subida al dejar de moverse se va deslizando hacia abajo
            // 2) Si quiere desender, en lugar de deslizarse sale "flotando" en el Eje X
        }
    }
}
