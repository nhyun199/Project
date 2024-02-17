using UnityEngine;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour
{
    private void Start()
    {
        // 원하는 해상도로 설정
        Screen.SetResolution(360, 640, false);
    }

    public void GameStart()
    {
        SceneManager.LoadScene("GamePlayScene");
    }

    public void GameExit()
    {
        // 에디터에서 작동 시에만 사용함
        //UnityEditor.EditorApplication.isPlaying = false;

        // 게임 빌드 시
        Application.Quit();
    }
}
