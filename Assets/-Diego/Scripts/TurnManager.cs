using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MSVJ1.Diego
{
    public class TurnManager : MonoBehaviour
    {
        [Header("Players")]
        [SerializeField] private GameObject prefabPlayer1 = null;
        private CharacterController controllerPlayer1 = null;
        [SerializeField] private GameObject prefabPlayer2 = null;
        private CharacterController controllerPlayer2 = null;

        [Header("Turns")]
        [SerializeField] private float turnDuration = 0f;
        private float timer = 0f;
        private bool doOnce = true;
        private int currentTurn;


        private void Start()
        {
            controllerPlayer1 = prefabPlayer1.GetComponent<CharacterController>();
            controllerPlayer2 = prefabPlayer2.GetComponent<CharacterController>();

            currentTurn = (int)Random.Range(1, 3);
            Debug.Log("Empieza jugando Player" + currentTurn);

            if (currentTurn == 1)
            {
                EnableController(controllerPlayer1);
                DisableController(controllerPlayer2);
            }
            else if (currentTurn == 2)
            {
                EnableController(controllerPlayer2);
                DisableController(controllerPlayer1);
            }

            ResetTimer();
        }

        private void Update()
        {
            if (timer >= turnDuration && doOnce)
            {
                if (currentTurn == 1)
                {
                    EnableController(controllerPlayer2);
                    DisableController(controllerPlayer1);
                    currentTurn = 2;
                }
                else if (currentTurn == 2)
                {
                    EnableController(controllerPlayer1);
                    DisableController(controllerPlayer2);
                    currentTurn = 1;
                }

                doOnce = false;

                Debug.Log("Comienza el turno de Player" + currentTurn);

                ResetTimer();
            }
            else
            {
                timer += Time.deltaTime;
            }
        }

        private void ResetTimer()
        {
            timer = 0;
            doOnce = true;
        }

        private void DisableController (CharacterController controller)
        {
            controller.enabled = false;
        }

        private void EnableController(CharacterController controller)
        {
            controller.enabled = true;
        }
    }
}
