using UnityEngine;
using UnityEngine.SceneManagement;

public class InNewGame : MonoBehaviour
{
    public Scene Scene;
    public Canvas NewGame;
    public Canvas Title;

    public void Button_Restart()
    {
        NewGame.enabled = true;        
    }

    public void Button_Title()
    {
        Title.enabled = true;
    }
}
