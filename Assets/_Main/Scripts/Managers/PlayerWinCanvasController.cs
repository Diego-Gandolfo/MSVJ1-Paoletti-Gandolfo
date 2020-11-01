using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace MSVJ1.Main
{
    public class PlayerWinCanvasController : MonoBehaviour
    {
        [Header("SoundManager")]
        [SerializeField] private SoundManager soundManager = null;

        [Header("Buttons")]
        [SerializeField] private Button buttonMainMenu = null;
        [SerializeField] private Button buttonPlayAgain = null;

        [Header("Scenes")]
        [SerializeField] private string sceneMainMenu = null;

        private void Awake()
        {
            buttonMainMenu.onClick.AddListener(OnClickMainMenuHandler);
            buttonPlayAgain.onClick.AddListener(OnClickPlayAgainHandler);
        }

        private void OnClickMainMenuHandler()
        {
            Time.timeScale = 1;
            soundManager.PlaySound("click");
            SceneManager.LoadScene(sceneMainMenu);
        }

        private void OnClickPlayAgainHandler()
        {
            Time.timeScale = 1;
            soundManager.PlaySound("click");
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
