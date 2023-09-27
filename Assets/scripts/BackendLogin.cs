using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using BackEnd;
using UnityEngine.UI;

public class BackendLogin : MonoBehaviour
{
    [SerializeField]
    private TMP_InputField inputFieldID;
    [SerializeField]
    private TMP_InputField inputFieldPW;

    [SerializeField]
    private Button btnLogin;
    private static BackendLogin _instance = null;

    public static BackendLogin Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new BackendLogin();
            }

            return _instance;
        }
    }
    
    public void OnClickLogin()
    {
        // 로그인 버튼 연타하지 못하도록 상호작용 비활성화
        btnLogin.interactable = false;

        //서버에 로그인을 요청하는 동안 화면에 출력하는 내용 업데이트

        // 뒤끝 서버 로그인 시도
        ResponseToLogin(inputFieldID.text, inputFieldPW.text);
        
    }

    private void ResponseToLogin(string ID, string PW)
    {
        //서버에 로그인 요청
        Backend.BMember.CustomLogin(ID, PW, callback =>
        {
            if (callback.IsSuccess())
            {
                SetMessage
            }
        }
    }

    private IEnumerator LoginProcess()
    {

    }
    

}