using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace MSVJ1.Main
{
    public class InGameMenuCanvasController : MonoBehaviour
    {
        [Header("Buttons")]
        [SerializeField] private Button buttonMainMenu = null;
        [SerializeField] private Button buttonBack = null;

        [Header("Scenes")]
        [SerializeField] private string sceneMainMenu = null;

        private void Awake()
        {
            buttonMainMenu.onClick.AddListener(OnClickMainMenuHandler);
            buttonBack.onClick.AddListener(OnClickBackHandler);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
                OnClickBackHandler();
        }

        private void OnClickMainMenuHandler()
        {
            Time.timeScale = 1;
            SceneManager.LoadScene(sceneMainMenu);
        }

        private void OnClickBackHandler()
        {
            Time.timeScale = 1;
            gameObject.SetActive(false);
        }
    }
}
