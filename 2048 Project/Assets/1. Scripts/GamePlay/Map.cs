using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Puzzle_2048
{
    public class Map : MonoBehaviour
    {
        public GameObject Board;
        public Canvas GameOverCanvas;
        public Canvas NewGameCanvas;
        public Canvas TitleCanvas;
        public Canvas ScoreSaveCanvas;
        public Text ScoreText;
        public Text GameOverScoreText;
        public Text SaveScoreText;
        public BackgroundMusicController backgroundMusicController;

        public int Score
        {
            get
            {
                return _score;
            }
        }

        private int _score = 0;
        private Node[,] map = new Node[4, 4];
        private LinkedList<Node> nodesEmpty = new LinkedList<Node>();
        private bool isAnimating = false;
        private UIScoreHandler uIScoreHandler;

        private void Awake()
        {
            int index = 0;
            Node[] _mapTrans = Board.GetComponentsInChildren<Node>();

            for (int x = 0; x < map.GetLength(0); x++)
            {
                for (int y = 0; y < map.GetLength(1); y++)
                {
                    map[x, y] = _mapTrans[index++];

                    map[x, y].onValueChanged += (node, value) =>
                    {
                        if (node.isEmpty)
                        {
                            nodesEmpty.AddLast(node);
                        }
                        else
                        {
                            nodesEmpty.Remove(node);
                        }
                    };
                }
            }
            foreach (var node in map)
            {
                node.animator = node.GetComponent<Animator>();

                if (node.isEmpty)
                {
                    nodesEmpty.AddLast(node);
                    //Debug.Log($"Node {node.name}이(가) nodesEmpty 리스트에 추가됨");
                }
            }
        }

        private void Start()
        {
            uIScoreHandler = FindObjectOfType<UIScoreHandler>();
            GameOverCanvas.enabled = false;
            NewGameCanvas.enabled = false;
            TitleCanvas.enabled = false;
            ScoreSaveCanvas.enabled = false;
            Spawn_Start();
        }

        private void Update()
        {
            if (isAnimating)
                return;

            MovingAndCombine();
            UpdateScore(_score);

            if (Input.GetKeyDown(KeyCode.Space))
            {
                GameOver();
            }
        }

        public void Spawn_Start()
        {

            if (TryRandomSpawn(out Node node))
            {
                isAnimating = true;
                node.animator.SetTrigger("Spawn");
                StartCoroutine(DelaySpawn2(node, 0.3f));
                StartCoroutine(Animating(1.0f));
                //StartCoroutine(Animating(0.1f));

            }

            if (TryRandomSpawn(out Node node2))
            {
                isAnimating = true;
                node2.animator.SetTrigger("Spawn");
                StartCoroutine(DelaySpawn4(node2, 0.3f));
                StartCoroutine(Animating(1.0f));
                //StartCoroutine(Animating(0.1f));

            }
        } // 게임 시작 시 2, 2 블록 또는 2, 4 블록을 생성함

        public void Spawn_Moving()
        {
            if (TryRandomSpawn(out Node node))
            {
                isAnimating = true;
                node.animator.SetTrigger("Spawn");
                StartCoroutine(DelaySpawn2(node, 0.3f));
                StartCoroutine(Animating(1.0f));
                //StartCoroutine(Animating(0.1f));

            }
        } // 방향키를 눌렀을 때 랜덤으로 2 블록을 생성함

        private bool TryRandomSpawn(out Node node) // 랜덤으로 블록을 생성할 위치를 선정하고 시도함
        {

            node = nodesEmpty.OrderBy(x => Guid.NewGuid())
                             .FirstOrDefault();

            if (node != null)
            {
                //node.animator.SetTrigger("Spawn");
                return true;
            }
            else
            {

                return false;
            }
        }

        public void MovingAndCombine()
        {
            bool moved = false;

            #region 윗 방향키
            if (Input.GetKeyDown(KeyCode.UpArrow) && isAnimating == false)
            {
                for (int x = 0; x < map.GetLength(0); x++)
                {
                    for (int y = map.GetLength(1) - 1; y > 0; y--)
                    {
                        for (int y1 = y - 1; y1 >= 0; y1--)
                        {
                            if (map[x, y1].value == 0)
                                continue;

                            if (map[x, y].value == 0)
                            {
                                isAnimating = true;
                                map[x, y].value = map[x, y1].value;
                                map[x, y1].value = 0;
                                moved = true;
                                StartCoroutine(Animating(1.0f));
                                //StartCoroutine(Animating(0.1f));
                            }

                            else if (map[x, y].value == map[x, y1].value)
                            {
                                isAnimating = true;
                                map[x, y].value *= 2;
                                map[x, y1].value = 0;
                                _score += map[x, y].value;
                                map[x, y].animator.SetTrigger("Combine");
                                moved = true;
                                StartCoroutine(Animating(1.0f));
                                //StartCoroutine(Animating(0.1f));
                                break;
                            }

                            else
                            {
                                break;
                            }
                        }
                    }
                }

                if (moved)
                {
                    Spawn_Moving();
                    CheckGameOver();
                }
            }
            #endregion

            #region 아래 방향키
            if (Input.GetKeyDown(KeyCode.DownArrow) && isAnimating == false)
            {
                for (int x = 0; x < map.GetLength(0); x++)
                {
                    for (int y = 0; y < map.GetLength(1) - 1; y++)
                    {
                        for (int y1 = y + 1; y1 < map.GetLength(1); y1++)
                        {

                            if (map[x, y1].value == 0)  // 빈 노드는 건너뜁니다.
                                continue;

                            if (map[x, y].value == 0)  // 현재 노드가 비어 있으면, 다음 노드로 이동합니다.
                            {
                                isAnimating = true;
                                map[x, y].value = map[x, y1].value;
                                map[x, y1].value = 0;
                                moved = true;
                                StartCoroutine(Animating(1.0f));
                                //StartCoroutine(Animating(0.1f));
                            }

                            else if (map[x, y].value == map[x, y1].value)  // 값이 같으면 합칩니다.
                            {
                                isAnimating = true;
                                map[x, y].value *= 2;
                                map[x, y1].value = 0;
                                _score += map[x, y].value;
                                map[x, y].animator.SetTrigger("Combine");
                                moved = true;
                                StartCoroutine(Animating(1.0f));
                                //StartCoroutine(Animating(0.1f));
                                break;  // 하나의 노드는 한 번만 합칠 수 있습니다.
                            }

                            else  // 값이 다르면, 이동을 중단합니다.
                            {
                                break;
                            }
                        }
                    }
                }

                if (moved)
                {
                    Spawn_Moving();
                    CheckGameOver();
                }
            }
            #endregion

            #region 오른 방향키
            if (Input.GetKeyDown(KeyCode.RightArrow) && isAnimating == false)
            {
                for (int y = 0; y < map.GetLength(1); y++)
                {
                    for (int x = map.GetLength(0) - 1; x > 0; x--)
                    {
                        for (int x1 = x - 1; x1 >= 0; x1--)
                        {
                            if (map[x1, y].value == 0)
                                continue;

                            if (map[x, y].value == 0)
                            {
                                isAnimating = true;
                                map[x, y].value = map[x1, y].value;
                                map[x1, y].value = 0;
                                moved = true;
                                StartCoroutine(Animating(1.0f));
                                //StartCoroutine(Animating(0.1f));
                            }

                            else if (map[x, y].value == map[x1, y].value)
                            {
                                isAnimating = true;
                                map[x, y].value *= 2;
                                map[x1, y].value = 0;
                                _score += map[x, y].value;
                                map[x, y].animator.SetTrigger("Combine");
                                moved = true;
                                StartCoroutine(Animating(1.0f));
                                //StartCoroutine(Animating(0.1f));
                                break;
                            }

                            else
                            {
                                break;
                            }
                        }
                    }
                }
                if (moved)
                {
                    Spawn_Moving();
                    CheckGameOver();
                }
            }
            #endregion

            #region 왼 방향키
            if (Input.GetKeyDown(KeyCode.LeftArrow) && isAnimating == false)
            {
                for (int y = 0; y < map.GetLength(1); y++)
                {
                    for (int x = 0; x < map.GetLength(0) - 1; x++)
                    {
                        for (int x1 = x + 1; x1 < map.GetLength(0); x1++)
                        {
                            if (map[x1, y].value == 0)
                                continue;

                            if (map[x, y].value == 0)
                            {
                                isAnimating = true;
                                map[x, y].value = map[x1, y].value;
                                map[x1, y].value = 0;
                                moved = true;
                                StartCoroutine(Animating(1.0f));
                                //StartCoroutine(Animating(0.1f));
                            }

                            else if (map[x, y].value == map[x1, y].value)
                            {
                                isAnimating = true;
                                map[x, y].value *= 2;
                                map[x1, y].value = 0;
                                _score += map[x, y].value;
                                map[x, y].animator.SetTrigger("Combine");
                                moved = true;
                                StartCoroutine(Animating(1.0f));
                                //StartCoroutine(Animating(0.1f));
                                break;
                            }
                            else
                            {
                                break;
                            }
                        }
                    }
                }
                if (moved)
                {
                    Spawn_Moving();
                    CheckGameOver();
                }
            }
            #endregion
        } // 블록의 이동과 결합

        public void GameOver()
        {
            GameOverCanvas.enabled = true; // 조건 충족 시 GameOver Canvas를 띄움
            backgroundMusicController.StopMusic();

            if (uIScoreHandler != null)
            {
                uIScoreHandler.OnGameOver(Score);
            }
        }

        private void CheckGameOver()
        {
            if (nodesEmpty.Count == 0 && !IsThereAnyMove())
            {
                GameOver(); // GameOver의 조건인가?
            }
        }
        private bool IsThereAnyMove()
        {
            for (int x = 0; x < map.GetLength(0); x++)
            {
                for (int y = 0; y < map.GetLength(1); y++)
                {
                    int currentValue = map[x, y].value;
                    if (x < map.GetLength(0) - 1 && currentValue == map[x + 1, y].value)
                        return true; // 오른쪽 블록과 합칠 수 있음
                    if (y < map.GetLength(1) - 1 && currentValue == map[x, y + 1].value)
                        return true; // 아래쪽 블록과 합칠 수 있음
                }
            }
            return false; // 더 이상 합칠 수 있는 블록이 없음
        } // 움직일 수 있는 상태를 체크
        private void UpdateScore(int newScore)
        {
            ScoreText.text = newScore.ToString();

            if (GameOverCanvas.enabled || ScoreSaveCanvas.enabled)
            {
                GameOverScoreText.text = newScore.ToString();
                SaveScoreText.text = newScore.ToString();
            }
        }


        #region Coroutine
        IEnumerator DelaySpawn2(Node node, float dalay)
        {
            yield return new WaitForSeconds(dalay);

            node.value = 2;
            CheckGameOver();
        }
        IEnumerator DelaySpawn4(Node node, float dalay)
        {
            yield return new WaitForSeconds(dalay);

            node.value = UnityEngine.Random.Range(0, 2) > 0 ? 2 : 4;
            CheckGameOver();
        }

        IEnumerator Animating(float delay)
        {
            yield return new WaitForSeconds(delay);

            isAnimating = false;
        }
        #endregion



    }
}
