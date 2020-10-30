using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MSVJ1.Main
{
    public class LifeBarManager : MonoBehaviour
    {
        [SerializeField] private LifeController player = null;
        [SerializeField] private Image lifeImage = null;

        private void Awake()
        {
            player.OnGetDamage += ActualizeLifeBar;
        }

        private void Start()
        {
            ActualizeLifeBar();
        }

        private void ActualizeLifeBar()
        {
            lifeImage.fillAmount = player.GetLifePorcentage();
        }
    }
}
