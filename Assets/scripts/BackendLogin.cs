using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 뒤끝 SDK namespace 추가
using BackEnd;

public class BackendLogin
{
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

    public void CustomSignUp(string id)
    {
        // Step 2. 회원가입 구현하기 로직
    }

    public void CustomLogin(string id)
    {
        // Step 3. 로그인 구현하기 로직
    }
}