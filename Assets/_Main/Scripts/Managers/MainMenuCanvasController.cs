using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace MSVJ1.Main
{
    public class MainMenuCanvasController : MonoBehaviour
    {
        [Header("SoundManager")]
        [SerializeField] private SoundManager soundManager = null;

        [Header("Buttons")]
        [SerializeField] private Button buttonExit = null;
        [SerializeField] private Button buttonPlay = null;
        [SerializeField] private Button buttonHowToPlay = null;
        [SerializeField] private Button buttonBack = null;

        [Header("Menus")]
        [SerializeField] private GameObject mainMenu = null;
        [SerializeField] private GameObject howToPlay = null;

        [Header("Scenes")]
        [SerializeField] private string scenePlay = null;

        private void Awake()
        {
            buttonExit.onClick.AddListener(OnClickExitHandler);
            buttonPlay.onClick.AddListener(OnClickPlayHandler);
            buttonHowToPlay.onClick.AddListener(OnClickHowToPlayHandler);
            buttonBack.onClick.AddListener(OnClickBackHandler);
            howToPlay.SetActive(false);
            mainMenu.SetActive(true);
        }

        private void OnClickExitHandler()
        {
            soundManager.PlaySound("click");
            Application.Quit();
        }

        private void OnClickPlayHandler()
        {
            soundManager.PlaySound("click");
            SceneManager.LoadScene(scenePlay);
        }

        private void OnClickHowToPlayHandler()
        {
            soundManager.PlaySound("click");
            mainMenu.SetActive(false);
            howToPlay.SetActive(true);
        }

        private void OnClickBackHandler()
        {
            soundManager.PlaySound("click");
            howToPlay.SetActive(false);
            mainMenu.SetActive(true);
        }
    }
}
