using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MSVJ1.Main
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private LifeController lifeControllerPlayer1 = null;
        [SerializeField] private LifeController lifeControllerPlayer2 = null;

        private void Awake()
        {
            lifeControllerPlayer1.OnDie += OnPlayer1DieHandler;
            lifeControllerPlayer2.OnDie += OnPlayer2DieHandler;
        }

        // TODO: Fin de Partida

        private void OnPlayer1DieHandler()
        {
            print("Gano el Jugador 2");
        }
        
        private void OnPlayer2DieHandler()
        {
            print("Gano el Jugador 1");
        }
    }
}
