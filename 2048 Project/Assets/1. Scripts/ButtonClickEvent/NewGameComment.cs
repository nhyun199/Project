using UnityEngine;
using UnityEngine.SceneManagement;

public class NewGameComment : MonoBehaviour
{
    public Scene Scene;
    public Canvas NewGame;

    public void Button_Yes()
    {
        Debug.Log("게임을 다시 시작합니다.");
        Scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene("GameScene");        
    }

    public void Button_No()
    {
        NewGame.enabled = false;
    }
}
