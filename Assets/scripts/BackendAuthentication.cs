using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using BackEnd;

public class BackendAuthentication : MonoBehaviour
{
    public InputField idInput;
    public InputField pwInput;

    // 1. 동기 방식
    // 순차적으로 앞의 코드가 완료되면 다음 줄 실행

    //동기방식 회원가입
    public void OnClickSignUp()
    {
        BackendReturnObject BRO = Backend.BMember.CustomSignUp(idInput.text, pwInput.text, "로그인 강좌로 가입된 유저");

        if (BRO.IsSuccess())
        {
            Debug.Log("회원 가입 완료");
        }
        else
        {
            //BackendManager.ShowError
            Debug.Log("회원 가입 실패");
        }
    }

    // 2. 비동기 방식
    // 순차적으로 실행되나 앞의 코드에 완료여부와 상관없이 다음 줄 실행

}
