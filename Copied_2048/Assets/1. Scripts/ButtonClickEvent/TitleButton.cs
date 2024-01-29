using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleButton : MonoBehaviour
{
    public Canvas BestScoreCanvas;
    public Canvas ResetCanvas;

    public void Button_Start()
    {
        Debug.Log("GAME START!");
        SceneManager.LoadScene("GameScene");
    }

    public void Button_Exit()
    {
        Debug.Log("게임을 종료합니다!");
        // 에디터에서 동작할 때
        //UnityEditor.EditorApplication.isPlaying = false;
        // 빌드했을 때
        Application.Quit();
    }

    public void Button_BestScore()
    {
        BestScoreCanvas.enabled = true;
    }

    public void Button_BestScoreExit()
    {
        BestScoreCanvas.enabled = false;
    }

    public void Button_Reset()
    {
        ResetCanvas.enabled = true;
    }

    public void Button_Reset_No()
    {
        ResetCanvas.enabled = false;
    }
}
