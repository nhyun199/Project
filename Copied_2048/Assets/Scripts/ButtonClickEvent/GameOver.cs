using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public Scene Scene;

    public void Button_Restart()
    {
        Debug.Log("게임을 다시 시작합니다.");
        Scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene("GameScene");        
    }

    public void Button_Title()
    {
        Debug.Log("타이틀로 돌아갑니다");
        SceneManager.LoadScene("Title");
    }
}
