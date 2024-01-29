using UnityEngine;

namespace Puzzle_2048
{
    public class Title : MonoBehaviour
    {
        public Canvas BestScoreCanvas;
        public Canvas ResetCanvas;
        public Canvas ResetComplete;

        private void Awake()
        {
            BestScoreCanvas.enabled = false;
            ResetCanvas.enabled = false;
            ResetComplete.enabled = false;
        }
    }
}
