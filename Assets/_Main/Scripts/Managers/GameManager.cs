using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MSVJ1.Main
{
    public class GameManager : MonoBehaviour
    {
        [Header("Players")]
        [SerializeField] private LifeController lifeControllerPlayer1 = null;
        [SerializeField] private LifeController lifeControllerPlayer2 = null;

        [Header("Canvas Player Win")]
        [SerializeField] private GameObject canvasPlayer1Win = null;
        [SerializeField] private GameObject canvasPlayer2Win = null;

        [Header("Canvas Player Win")]
        [SerializeField] private GameObject canvasMenu = null;

        private void Awake()
        {
            lifeControllerPlayer1.OnDie += OnPlayer1DieHandler;
            lifeControllerPlayer2.OnDie += OnPlayer2DieHandler;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Time.timeScale = 0;
                canvasMenu.SetActive(true);
            }
        }

        private void OnPlayer1DieHandler()
        {
            canvasPlayer2Win.SetActive(true);
        }
        
        private void OnPlayer2DieHandler()
        {
            canvasPlayer1Win.SetActive(true);
        }
    }
}
