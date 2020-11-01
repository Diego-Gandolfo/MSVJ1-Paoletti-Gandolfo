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

        [Header("SoundManager")]
        [SerializeField] private SoundManager soundManager = null;

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
            Time.timeScale = 0;
            soundManager.PlaySound("win");
            canvasPlayer2Win.SetActive(true);
        }
        
        private void OnPlayer2DieHandler()
        {
            Time.timeScale = 0;
            soundManager.PlaySound("win");
            canvasPlayer1Win.SetActive(true);
        }
    }
}
