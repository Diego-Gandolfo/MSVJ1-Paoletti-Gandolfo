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
        private CharacterController characterControllerPlayer1 = null;
        private ShootingController shootingControllerPlayer1 = null;
        [SerializeField] private GameObject prefabPlayer2 = null;
        private CharacterController characterControllerPlayer2 = null;
        private ShootingController shootingControllerPlayer2 = null;

        [Header("Turns Settings")]
        [SerializeField] private float startDuration = 0f;
        private float startTimer = 0f;
        private bool canStart = true;
        [SerializeField] private float movementDuration = 0f;
        private float movementTimer = 0f;
        private bool canMove = false;
        [SerializeField] private float shootDuration = 0f;
        private float shootTimer = 0f;
        private bool canShoot = false;
        private int currentTurn;

        [Header("Canvas Settings")]
        [SerializeField] private Text textText = null;
        [SerializeField] private Text textNumber = null;


        private void Start()
        {
            characterControllerPlayer1 = prefabPlayer1.GetComponent<CharacterController>();
            characterControllerPlayer1.enabled = false;
            characterControllerPlayer2 = prefabPlayer2.GetComponent<CharacterController>();
            characterControllerPlayer2.enabled = false;

            shootingControllerPlayer1 = prefabPlayer1.GetComponentInChildren<ShootingController>();
            shootingControllerPlayer1.enabled = false;
            shootingControllerPlayer2 = prefabPlayer2.GetComponentInChildren<ShootingController>();
            shootingControllerPlayer2.enabled = false;

            currentTurn = (int)Random.Range(1, 3);

            startTimer = startDuration;
            movementTimer = movementDuration;
            shootTimer = shootDuration;
        }

        private void LateUpdate()
        {
            // Start Turn
            if (canStart && startTimer <= 0)
            {
                canStart = false;

                textText.text = "";
                textNumber.text = "";

                if (currentTurn == 1) characterControllerPlayer1.enabled = true;
                else if (currentTurn == 2) characterControllerPlayer2.enabled = true;

                canMove = true;
            }
            else if (canStart && startTimer > 0)
            {
                startTimer -= Time.deltaTime;
                textText.text = "Turno de Jugador" + currentTurn + " comienza en";
                int intStartTimer = (int)startTimer + 1;
                textNumber.text = intStartTimer.ToString();
            }

            // Movement
            if (canMove && movementTimer <= 0)
            {
                canMove = false;

                textText.text = "";
                textNumber.text = "";

                if (currentTurn == 1)
                {
                    characterControllerPlayer1.enabled = false;
                    shootingControllerPlayer1.enabled = true;
                }
                else if (currentTurn == 2)
                {
                    characterControllerPlayer2.enabled = false;
                    shootingControllerPlayer2.enabled = true;
                }

                canShoot = true;
            }
            else if (canMove && movementTimer > 0)
            {
                movementTimer -= Time.deltaTime;
                textText.text = "Jugador" + currentTurn + " puede moverse!";
                int intMovementTimer = (int)movementTimer + 1;
                textNumber.text = intMovementTimer.ToString();
            }

            // Shooting
            if (canShoot && (Input.GetKeyUp(KeyCode.Space) || shootTimer <= 0))
            {
                canShoot = false;

                shootingControllerPlayer1.enabled = false;
                shootingControllerPlayer2.enabled = false;

                textText.text = "";
                textNumber.text = "";
            }
            else if(canShoot && shootTimer > 0)
            {
                shootTimer -= Time.deltaTime;
                textText.text = "Jugador" + currentTurn + " puede DISPARAR!";
                int intShootTimer = (int)shootTimer + 1;
                textNumber.text = intShootTimer.ToString();
            }

            // Finish Turn
            if (!canStart && !canMove && !canShoot)
            {
                currentTurn = currentTurn == 1 ? 2 : 1;

                startTimer = startDuration;
                movementTimer = movementDuration;
                shootTimer = shootDuration;

                canStart = true;
            }
        }
    }
}
