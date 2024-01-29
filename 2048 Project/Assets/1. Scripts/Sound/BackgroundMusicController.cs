using UnityEngine;

namespace Puzzle_2048
{
    public class BackgroundMusicController : MonoBehaviour
    {
        private AudioSource audioSource;

        private void Start()
        {
            audioSource = GetComponent<AudioSource>();
        }

        public void StopMusic()
        {
            if (audioSource.isPlaying)
            {
                audioSource.Stop();
            }
        }

        public void ToggleMusic()
        {
            if (audioSource.isPlaying)
            {
                audioSource.Stop(); // 음악이 재생 중이면 정지
            }
            else
            {
                audioSource.Play(); // 음악이 정지되어 있으면 재생
            }
        }
    }
}

