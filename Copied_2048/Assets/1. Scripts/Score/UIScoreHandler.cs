
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Puzzle_2048
{   
    public class UIScoreHandler : MonoBehaviour
    {
        public InputField initialsInputField;
        public Canvas scoreSaveCanvas;
        public Text currentScoreText;
        private ScoreManager scoreManager;
        private int currentScore;

        private void Start()
        {            
            scoreManager = new ScoreManager();
        }

        public void SavePlayerScore()
        {
            string initials = initialsInputField.text; // 사용자 이니셜 가져오기
            scoreManager.AddScore(currentScore, initials); // 점수 저장
            scoreManager.SaveScores(); // 변경된 점수 목록 저장
        }

        public void OnGameOver(int score)
        {            
            currentScore = score;
            StartCoroutine(ShowScoreSaveCanvasIfNeeded());
        }

        private IEnumerator ShowScoreSaveCanvasIfNeeded()
        {
            yield return new WaitForSeconds(1.5f); // 1.5초 대기

            // 현재 점수가 최고 점수 목록에 들어갈 만한지 확인
            if (scoreManager.CanAddScore(currentScore))
            {
                currentScoreText.text = currentScore.ToString();
                scoreSaveCanvas.enabled = true;
            }
        }
    }
}
