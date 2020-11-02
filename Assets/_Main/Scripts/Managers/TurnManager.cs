using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MSVJ1.Main
{
    public class TurnManager : MonoBehaviour
    {
        [Header("Players Settings")]
        [SerializeField] private GameObject player1 = null; // El GameObject del Player 1
        [SerializeField] private GameObject player2 = null; // El GameObject del Player 2
        private CharacterController characterController = null; // Para almacenar el CharacterController de los Players
        private ShootingController shootingController = null; // Para almacenar el ShootingController de los Players

        [Header("Turns Settings")]
        [SerializeField] private float startDuration = 0f; // Cuanto debe de esperar desde que termina un turno hasta que empieza el siguiente
        private bool canStart = true; // Si puede empezar con la sección de Start
        [SerializeField] private float movementDuration = 0f; // Cuanto tiempo tienen para Moverse
        private bool canMove = false; // Si puede empezar con la sección de Movimiento
        [SerializeField] private float shootDuration = 0f; // Cuanto tiempo tienen para Disparar
        private bool canShoot = false; // Si puede empezar con la sección de Disparar
        [SerializeField] private float explotionDuration = 0f; // Cuanto tiempo tienen para Disparar
        private bool doneExplotion = false; // Si puede empezar con la sección de Disparar
        private int currentTurn; // De quien es el Turno Actual
        private float timer = 0f; // Para llevar el Contador de tiempo

        [Header("Canvas Settings")]
        [SerializeField] private Text textText = null; // El Texto del Canvas que usamos para los Titulos o Mensajes
        [SerializeField] private Text textNumber = null; // El Texto del Canvas que usamos para el Contador

        [Header("Camera Settings")]
        [SerializeField] private CinemaMachineManager cinemaManager = null;
        [SerializeField] private Vector2 offsetCamera = Vector2.zero;
        [SerializeField] private float sceneDistanceView = 0f;
        [SerializeField] private float playerDistanceView = 0f;

        [Header("SoundManager")]
        [SerializeField] private SoundManager soundManager = null;

        private void Start()
        {
            currentTurn = Random.Range(1, 3); // Asignamos de forma aleatoria el 1er Turno

            if (currentTurn == 1) // Si es el Turno del Jugador1
            {
                AsignTurnPlayer2(player2); // Primero le asignaomos el Turno al Jugador2 para que le desactive los componentes
                AsignTurnPlayer1(player1); // Y después se lo asignamos al Jugador1 para que esté listo para jugar
            }
            else if (currentTurn == 2) // Si es el Turno del Jugador2
            {
                AsignTurnPlayer1(player1); // Primero le asignaomos el Turno al Jugador1 para que le desactive los componentes
                AsignTurnPlayer2(player2); // Y después se lo asignamos al Jugador2 para que esté listo para jugar
            }

            timer = startDuration; // Inicializamos el timer para que empiece con el valor de startDuration
        }

        private void LateUpdate() // Lo hacemos en un LateUpdate para asegurarnos que esto sea lo ultimo que ocurre en los Updates
        {
            // Start Turn
            if (canStart && (timer <= 0 || Input.GetKeyUp(KeyCode.Space))) // Si termino de Contar y puede hacer el Start
            {
                Input.ResetInputAxes();
                canStart = false; // Lo ponemos en FALSE porque ya está haciendo el Start

                characterController.enabled = true; // Activamos el CharacterController

                timer = movementDuration; // Inicializamos el timer con el valor de movementDuration
                canMove = true; // Indicamos que puede empezar con el Movimiento
                cinemaManager.SetDistanceView(playerDistanceView);
                cinemaManager.SetTarget(characterController.gameObject);
                cinemaManager.SetOffset(offsetCamera);
            }
            else if (canStart && timer > 0) // Si no termino de Contar y puede hacer el Start
            {
                textText.text = currentTurn == 1 ? "Turno de <b><color=green>Jugador1</color></b> comienza en" : "Turno de <b><color=blue>Jugador2</color></b> comienza en";
                DoTimer(); // Que haga la funcion del Timer
            }

            // Movement
            if (canMove && (timer <= 0 || Input.GetKeyUp(KeyCode.Space))) // Si termino de Contar y puede hacer el Movimiento
            {
                canMove = false; // Lo ponemos en FALSE porque ya está haciendo el Movimiento

                characterController.enabled = false; // Desactivamos el CharacterController
                shootingController.enabled = true; // Activamos el ShootingController

                soundManager.PlaySound("playerStop");

                timer = shootDuration; // Inicializamos el timer con el valor de shootDuration
                canShoot = true; // Indicamos que puede empezar con el Disparar
            }
            else if (canMove && timer > 0) // Si no termino de Contar y puede hacer el Movimiento
            {
                textText.text = currentTurn == 1 ? "<b><color=green>Jugador1</color></b> puede moverse!" : "<b><color=blue>Jugador2</color></b> puede moverse!";
                DoTimer(); // Que haga la funcion del Timer
            }

            // Shooting
            if (canShoot && (shootingController.doneShoot || timer <= 0)) // Si termino de Contar o toco el Space, y si puede Disparar
            {
                canShoot = false; // Lo ponemos en FALSE porque ya está haciendo el Disparar

                shootingController.enabled = false; // Desactivamos el ShootingController

                if (shootingController.doneShoot)
                {
                    soundManager.PlaySound("shoot");

                    shootingController.doneShoot = false;

                    timer = explotionDuration; // Inicializamos el timer con el valor de shootDuration
                }
                else
                {
                    doneExplotion = true;
                }

                textText.text = ""; // Si el timer es igual a explotionDuration ponemos el titulo en blanco
                textNumber.text = ""; // Si el timer es igual a explotionDuration ponemos el contador en blanco
            }
            else if (canShoot && timer > 0) // Si no termino de Contar y puede Disparar
            {
                textText.text = currentTurn == 1 ? "<b><color=green>Jugador1</color></b> puede DISPARAR!" : "<b><color=blue>Jugador2</color></b> puede DISPARAR!";
                DoTimer(); // Que haga la funcion del Timer
            }

            if (shootingController.GetHasExploted() && timer <= 0)
            {
                doneExplotion = true;
                shootingController.SetHasExploted(false);
            }
            else if (shootingController.GetHasExploted() && timer > 0)
            {
                if (timer == explotionDuration) soundManager.PlaySound("explotion");
                
                timer -= Time.deltaTime; // Restamos Time.deltaTime para hacer una cuenta regresiva
            }

            if (!canStart && !canMove && !canShoot && doneExplotion) // Momentaneamente, si ya termino con todas las fases
            {
                doneExplotion = false;

                currentTurn = currentTurn == 1 ? 2 : 1; // Si es el Turno del Jugador1 que ahora currentTurns sea 2 y si no es el Turno del Jugador1 entonces que ahora currentTurns sea 1

                if (currentTurn == 1) AsignTurnPlayer1(player1); // Si el nuevo turno es del Jugador1, le asignamos los componentes
                else if (currentTurn == 2) AsignTurnPlayer2(player2); // Si el nuevo turno es del Jugador2, le asignamos los componentes

                timer = startDuration; // Inicializamos el timer con el valor de startDuration
                canStart = true; // Indicamos que puede empezar con el Start
            }
        }

        private void AsignTurnPlayer1(GameObject player1) // Asignamos los Componentes al Jugador1
        {
            characterController = player1.GetComponent<CharacterController>(); // Obtenemos el CharactgerController del Player 1
            characterController.enabled = false; // Lo ponemos en falso, para controlar nosotros cuando se prende
            shootingController = player1.GetComponentInChildren<ShootingController>(); // Obtenemos el ShootingController del Player 1
            shootingController.enabled = false; // Lo ponemos en falso, para controlar nosotros cuando se prende
            cinemaManager.SetDistanceView(sceneDistanceView);
            cinemaManager.SetCentralPosition();
        }

        private void AsignTurnPlayer2(GameObject player2) // Asignamos los Componentes al Jugador2
        {
            shootingController = player2.GetComponentInChildren<ShootingController>(); // Obtenemos el ShootingController del Player 2
            shootingController.enabled = false; // Lo ponemos en falso, para controlar nosotros cuando se prende
            characterController = player2.GetComponent<CharacterController>(); // Obtenemos el CharactgerController del Player 2
            characterController.enabled = false; // Lo ponemos en falso, para controlar nosotros cuando se prende
            cinemaManager.SetDistanceView(sceneDistanceView);
            cinemaManager.SetCentralPosition();
        }

        private void DoTimer() // Hacemos el Timer
        {
            timer -= Time.deltaTime; // Restamos Time.deltaTime para hacer una cuenta regresiva
            int intTimer = (int)timer + 1; // Acá guarado en un int el valor entero de timer y le sumo 1, para que la cuenta no termine en 0 en el Canvas
            textNumber.text = intTimer.ToString(); // Ponemos en el Canvas el valor de intTimer
        }
    }
}
