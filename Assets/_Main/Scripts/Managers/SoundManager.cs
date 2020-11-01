using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MSVJ1.Main
{
    public class SoundManager : MonoBehaviour
    {
        private AudioSource audioSource = null;

        [Header("AudioClips")]
        [SerializeField] private AudioClip shoot = null;
        [SerializeField] private AudioClip explotion = null;
        [SerializeField] private AudioClip win = null;
        [SerializeField] private AudioClip click = null;

        private void Awake()
        {
            audioSource = GetComponent<AudioSource>();
        }

        public void PlaySound(string sound)
        {
            switch (sound)
            {
                case "shoot":
                    audioSource.PlayOneShot(shoot);
                    break;

                case "explotion":
                    audioSource.PlayOneShot(explotion);
                    break;
                case "win":
                    audioSource.PlayOneShot(win);
                    break;

                case "click":
                    audioSource.PlayOneShot(click);
                    break;
            }
        }
    }
}
