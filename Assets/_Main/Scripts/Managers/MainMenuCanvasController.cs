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

        [Header("Scenes")]
        [SerializeField] private string scenePlay = null;

        private void Awake()
        {
            buttonExit.onClick.AddListener(OnClickExitHandler);
            buttonPlay.onClick.AddListener(OnClickPlayHandler);
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
    }
}
