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
            lifeControllerPlayer1.OnDie += OnDieHandler;
            lifeControllerPlayer2.OnDie += OnDieHandler;
        }

        private void OnDieHandler()
        {
            // TODO: Fin de Partida
        }
    }
}
