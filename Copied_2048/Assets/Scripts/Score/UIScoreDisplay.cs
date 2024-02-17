using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Puzzle_2048
{
    public class UIScoreDisplay : MonoBehaviour
    {
        public Canvas highScoreCanvas;
        public Canvas ResetScoreCanvas;
        public Canvas ResetComplete;
        public Text[] highScoreTexts;
        private ScoreManager scoreManager;

        private void Start()
        {
            scoreManager = new ScoreManager();
            scoreManager.LoadScores(); // 최고 점수 로드
        }

        public void ShowHighScores()
        {
            List<ScoreRecord> highScores = scoreManager.GetHighScores();
            for (int i = 0; i < highScoreTexts.Length && i < highScores.Count; i++)
            {
                highScoreTexts[i].text = $"{i+1}. {highScores[i].initials} : {highScores[i].score} 점";
            }
            highScoreCanvas.enabled = true;
        }

        public void ResetHighScores()
        {
            scoreManager.ResetScores(); // 점수 초기화
            UpdateScoreDisplay(); // UI 업데이트
            StartCoroutine(ShowResetConfirmation());
        }

        private void UpdateScoreDisplay()
        {
            // Text 컴포넌트들을 초기 상태로 업데이트
            foreach (var text in highScoreTexts)
            {
                text.text = "";
            }
        }
        private IEnumerator ShowResetConfirmation()
        {
            ResetComplete.enabled = true; 
            yield return new WaitForSeconds(1); 
            ResetComplete.enabled = false; 
            ResetScoreCanvas.enabled = false;
            highScoreCanvas.enabled = false; 
        }
    }
}
