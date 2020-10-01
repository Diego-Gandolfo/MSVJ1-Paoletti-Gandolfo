using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MSVJ1.Diego
{
    public class TurnManager : MonoBehaviour
    {
        [Header("Players Settings")]
        [SerializeField] private GameObject prefabPlayer1 = null;
        private CharacterController controllerPlayer1 = null;
        [SerializeField] private GameObject prefabPlayer2 = null;
        private CharacterController controllerPlayer2 = null;

        [Header("Turns Settings")]
        private int currentTurn;
        [SerializeField] private float startDuration = 0f;
        private float startTimer = 0f;
        private bool canStart = true;
        [SerializeField] private float movementDuration = 0f;
        private float movementTimer = 0f;
        private bool canMove = false;
        private bool canShoot = false;

        [Header("Canvas Settings")]
        [SerializeField] private Text textText = null;
        [SerializeField] private Text textNumber = null;


        private void Start()
        {
            controllerPlayer1 = prefabPlayer1.GetComponent<CharacterController>();
            controllerPlayer2 = prefabPlayer2.GetComponent<CharacterController>();

            controllerPlayer1.enabled = false;
            controllerPlayer2.enabled = false;

            currentTurn = (int)Random.Range(1, 3);

            startTimer = startDuration;
            movementTimer = movementDuration;
        }

        private void Update()
        {
            // Start Turn
            if (startTimer <= 0 && canStart)
            {
                canStart = false;

                textText.text = "";
                textNumber.text = "";

                if (currentTurn == 1) controllerPlayer1.enabled = true;
                else if (currentTurn == 2) controllerPlayer2.enabled = true;

                canMove = true;
            }
            else if (startTimer > 0 && canStart)
            {
                startTimer -= Time.deltaTime;
                textText.text = "Turno de Jugador" + currentTurn + " comienza en";
                int intStartTimer = (int)startTimer + 1;
                textNumber.text = intStartTimer.ToString();
            }

            // Movement
            if (movementTimer <= 0 && canMove)
            {
                canMove = false;

                textText.text = "";
                textNumber.text = "";

                if (currentTurn == 1) controllerPlayer1.enabled = false;
                else if (currentTurn == 2) controllerPlayer2.enabled = false;

                canShoot = true;
            }
            else if (movementTimer > 0 && canMove)
            {
                movementTimer -= Time.deltaTime;
                textText.text = "Jugador" + currentTurn + " puede moverse!";
                int intMovementTimer = (int)movementTimer + 1;
                textNumber.text = intMovementTimer.ToString();
            }

            // Shooting
            if (canShoot && Input.GetKeyDown(KeyCode.Space))
            {
                canShoot = false;

                textText.text = "";
            }
            else if(canShoot)
            {
                textText.text = "Jugador" + currentTurn + " puede DISPARAR!";
            }

            // Finish Turn
            if (!canStart && !canMove && !canShoot)
            {
                currentTurn = currentTurn == 1 ? 2 : 1;

                startTimer = startDuration;
                movementTimer = movementDuration;

                canStart = true;
            }
        }
    }
}
