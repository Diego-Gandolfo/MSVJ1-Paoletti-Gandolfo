using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MSVJ1.Main
{
    public class ChangeSpriteByLife : MonoBehaviour
    {
        [SerializeField] private float lifePorcentage = 0f;
        [SerializeField] private Sprite sprite = null;
        private LifeController lifeController = null;
        private SpriteRenderer spriteRenderer = null;

        private void Awake()
        {
            lifeController = GetComponent<LifeController>();
            spriteRenderer = GetComponent<SpriteRenderer>();
            lifeController.OnGetDamage += CheckLife;
        }

        private void CheckLife()
        {
            var _lifeProcentage = lifeController.GetLifePorcentage();

            if (_lifeProcentage <= lifePorcentage)
                ChangeSprite();
        }

        private void ChangeSprite()
        {
            spriteRenderer.sprite = sprite;
        }
    }
}
