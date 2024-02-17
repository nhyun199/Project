using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Puzzle_2048
{
    public class ScoreManager
    {
        // saves cores, load score, add score, check and add score

        private const int MaxScores = 6;
        private List<ScoreRecord> scores;

        public ScoreManager()
        {
            scores = new List<ScoreRecord>();
            LoadScores();
        }

        public void LoadScores()
        {
            string jsonData = PlayerPrefs.GetString("Scores", "{}");
            ScoreListWrapper wrapper = JsonUtility.FromJson<ScoreListWrapper>(jsonData);
            scores = wrapper.scores ?? new List<ScoreRecord>();
            SortScores();
        }

        public void SaveScores()
        {
            ScoreListWrapper wrapper = new ScoreListWrapper { scores = scores };
            string jsonData = JsonUtility.ToJson(wrapper);
            PlayerPrefs.SetString("Scores", jsonData);
            PlayerPrefs.Save();
        }

        public void AddScore(int newScore, string initials)
        {
            ScoreRecord newRecord = new ScoreRecord { score = newScore, initials = initials };

            // Check if the new score is higher than the lowest score in the list or if the list is not full
            if (scores.Count < MaxScores || newRecord.score > scores.Last().score)
            {
                scores.Add(newRecord);
                SortScores();

                // Keep only the top scores up to MaxScores
                if (scores.Count > MaxScores)
                {
                    scores.RemoveRange(MaxScores, scores.Count - MaxScores);
                }
            }
        }

        private void SortScores()
        {
            scores.Sort((a, b) => b.score.CompareTo(a.score));
        }

        public List<ScoreRecord> GetHighScores()
        {
            return scores;
        }

        public bool CanAddScore(int newScore)
        {
            // 6개의 점수가 모두 기록되어 있고, 모든 점수보다 낮다면 false 반환
            return scores.Count < 6 || newScore > scores.Last().score;
        }

        [Serializable]
        private class ScoreListWrapper
        {
            public List<ScoreRecord> scores;
        }

        public void ResetScores()
        {
            scores.Clear(); // 점수 목록 클리어
            SaveScores(); // 변경 사항 저장
        }
    }
}
