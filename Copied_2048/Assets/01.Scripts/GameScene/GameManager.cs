using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject[] n;
    public GameObject Quit;
    public Text Score, BestScore, Plus;

    bool wait, move, stop;
    int x, y, i, j, k, l, score;
    Vector3 firstPos, gap;
    GameObject[,] Square = new GameObject[4, 4];

    void Start()
    {
        Screen.SetResolution(360, 640, false);
        Spawn();
        Spawn();
        BestScore.text = PlayerPrefs.GetInt("BestScore").ToString();
    }

    void Update()
    {
        // ESC 버튼으로 게임 종료
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        if (stop)
            return;

        if (Input.GetMouseButtonDown(0))
        {
            wait = true;
            firstPos = Input.mousePosition;
        }

        if (Input.GetMouseButton(0))
        {
            gap = Input.mousePosition - firstPos;

            if (gap.magnitude < 100)
                return;

            gap.Normalize();

            if (wait)
            {
                Drag();
                DisplayScoreAndCheckGameOver();
            }
        }
    }

    // 게임 시작 시 타일 스폰
    void Spawn()
    {
        while (true)
        {
            x = Random.Range(0, 4); // int형 변수의 Random.Range(최소값, 최대값)을 사용할 경우 최솟값 ~ (최댓값-1) 으로 범위가 적용
            y = Random.Range(0, 4);
            if (Square[x, y] == null)
                break;
        }
        Square[x, y] = Instantiate(Random.Range(0, int.Parse(Score.text) > 800 ? 4 : 8) > 0 ? n[0] : n[1],
                                   new Vector3(1.2f * x - 1.8f, 1.2f * y - 1.8f, 0),
                                   Quaternion.identity);
        Square[x, y].GetComponent<Animator>().SetTrigger("Spawn");
    }

    // [x1, y1] -> 이동 전 좌표 / [x2, y2] -> 이동 후 좌표
    void MoveOrCombine(int x1, int y1, int x2, int y2)
    {
        // 이동 가능
        if (Square[x2, y2] == null && Square[x1, y1] != null)
        {
            move = true;
            Square[x1, y1].GetComponent<Moving>().Move(x2, y2, false);
            Square[x2, y2] = Square[x1, y1];
            Square[x1, y1] = null;
        }

        // 숫자 같을 때 결합
        if (Square[x1, y1] != null && Square[x2, y2] != null &&
            Square[x1, y1].name == Square[x2, y2].name &&
            Square[x1, y1].tag != "Combine" &&
            Square[x2, y2].tag != "Combine")
        {
            move = true;
            for (j = 0; j <= 10; j++)
            {
                if (Square[x2, y2].name == n[j].name + "(Clone)")
                    break;
            }
            Square[x1, y1].GetComponent<Moving>().Move(x2, y2, true);
            Destroy(Square[x2, y2]);
            Square[x1, y1] = null;
            Square[x2, y2] = Instantiate(n[j + 1],
                                         new Vector3(1.2f * x2 - 1.8f, 1.2f * y2 - 1.8f, 0), Quaternion.identity);
            Square[x2, y2].tag = "Combine";
            Square[x2, y2].GetComponent<Animator>().SetTrigger("Combine");
            score += (int)Mathf.Pow(2, j + 2);
        }
    }

    // 드래그 로직
    void Drag() 
    {
        wait = false;

        // 위로 드래그
        if (gap.y > 0 && gap.x > -0.5f && gap.x < 0.5f)
        {
            for (x = 0; x <= 3; x++)
                for (y = 0; y <= 2; y++)
                    for (i = 3; i >= y + 1; i--)
                        MoveOrCombine(x, i - 1, x, i);
        }

        // 아래로 드래그
        else if (gap.y < 0 && gap.x > -0.5f && gap.x < 0.5f)
        {
            for (x = 0; x <= 3; x++)
                for (y = 3; y >= 1; y--)
                    for (i = 0; i <= y - 1; i++)
                        MoveOrCombine(x, i + 1, x, i);
        }

        // 오른쪽으로 드래그
        else if (gap.x > 0 && gap.y > -0.5f && gap.y < 0.5f)
        {
            for (y = 0; y <= 3; y++)
                for (x = 0; x <= 2; x++)
                    for (i = 3; i >= x + 1; i--)
                        MoveOrCombine(i - 1, y, i, y);
        }

        // 왼쪽으로 드래그
        else if (gap.x < 0 && gap.y > -0.5f && gap.y < 0.5f)
        {
            for (y = 0; y <= 3; y++)
                for (x = 3; x >= 1; x--)
                    for (i = 0; i <= x - 1; i++)
                        MoveOrCombine(i + 1, y, i, y);
        }

        else return; // Drag
    }

    // 점수 표시 및 게임오버 체크
    void DisplayScoreAndCheckGameOver()
    {
        if (move)
        {
            move = false;
            Spawn();
            k = 0;
            l = 0;

            // 점수
            if (score > 0)
            {
                Plus.text = "+" + score.ToString() + "    ";
                Plus.GetComponent<Animator>().SetTrigger("PlusBack");
                Plus.GetComponent<Animator>().SetTrigger("Plus");

                Score.text = (int.Parse(Score.text) + score).ToString();
                if (PlayerPrefs.GetInt("BestScore", 0) < int.Parse(Score.text))
                {
                    PlayerPrefs.SetInt("BestScore", int.Parse(Score.text));
                }

                BestScore.text = PlayerPrefs.GetInt("BestScore").ToString();
                score = 0;
            }

            for (x = 0; x <= 3; x++)
                for (y = 0; y <= 3; y++)
                {
                    if (Square[x, y] == null)
                    {
                        k++; // 모든 타일이 가득 찼는 지 판단함
                        continue;
                    }

                    if (Square[x, y].tag == "Combine")
                        Square[x, y].tag = "Untagged";
                }

            if (k == 0) // 모든 타일이 가득 찼으므로, 가로와 세로에 같은 블록이 있는 지 체크함
            {
                for (y = 0; y <= 3; y++) // 가로 검사
                    for (x = 0; x <= 2; x++)
                    {
                        if (Square[x, y].name == Square[x + 1, y].name)
                            l++;
                    }
                for (x = 0; x <= 3; x++) // 세로 검사
                    for (y = 0; y <= 2; y++)
                    {
                        if (Square[x, y].name == Square[x, y + 1].name)
                            l++;
                    }

                if (l == 0)
                {
                    stop = true;
                    Quit.SetActive(true);
                    return;
                }
            }
        }
    }

    public void Restart() // 게임 재시작 
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GoTitle()
    {
        SceneManager.LoadScene("Title");
    }
}
