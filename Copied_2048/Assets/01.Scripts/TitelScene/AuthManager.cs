using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Auth;
using UnityEngine.UI;
using System.Threading.Tasks;

public class AuthManager : MonoBehaviour
{
    [SerializeField] InputField emailField;
    [SerializeField] InputField passwordField;
    [SerializeField] InputField passwordResetEmail;

    // 인증 관리 객체
    Firebase.Auth.FirebaseAuth auth;

    void Awake()
    {
        auth = Firebase.Auth.FirebaseAuth.DefaultInstance;    
    }

    public void login()
    {
        auth.SignInWithEmailAndPasswordAsync(emailField.text, passwordField.text).ContinueWith
            (
            task =>
            {
                if (task.IsCompleted && !task.IsFaulted && !task.IsCanceled)
                {
                    Debug.Log(emailField.text + " 로 로그인하셨습니다.");
                }
                else
                {
                    Debug.Log("없거나 잘못된 형식입니다. 다시 한번 확인해주세요.");
                }
            }
            );
    }

    public void register()
    {
        auth.CreateUserWithEmailAndPasswordAsync(emailField.text, passwordField.text).ContinueWith
            (
            task =>
            {
                if (!task.IsCanceled && !task.IsFaulted)
                {
                    Debug.Log(emailField.text + "로 회원가입에 성공했습니다.");
                }
                else
                {
                    Debug.Log("회원가입에 실패하였습니다.");
                }
            }
            );
    }

    public void logout()
    {
        auth.SignOut();
        Debug.Log("로그아웃되었습니다.");
    }
}
