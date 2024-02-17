using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleComment : MonoBehaviour
{
    public Scene Scene;
    public Canvas Title;

    public void Button_Yes()
    {
        Debug.Log("타이틀로 돌아갑니다.");
        SceneManager.LoadScene("Title");
    }

    public void Button_No()
    {
        Title.enabled = false;
    }
}
