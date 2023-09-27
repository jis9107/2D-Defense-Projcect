using UnityEngine;
using BackEnd; //뒤끝 SDK

public class BackendManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        //뒤끝 서버 초기화
        BackendSetup();
    }

    private void Update()
    {
        ////서버의 비동기 메소드 호출(콜백 함수 풀링)을 위해 작성
        //if (Backend.IsInitialized)
        //{
        //    Backend.AsyncPoll();
        //}
    }

    private void BackendSetup()
    {
        //
        var bro = Backend.Initialize(true);

        //초기화에 대한 응답값
        if (bro.IsSuccess())
        {
            //초기화 성공 시 statusCode 204 Succes
            Debug.Log($"초기화 성공 : {bro}");
        }
        else
        {
            //초기화 실패 시 statusCode 400대 에러 발생
            Debug.LogError($"초기화 실패 : {bro}");
        }
    }

    public void ShowErrorUI(BackendReturnObject backendReturn)
    {
        int statusCode = int.Parse(backendReturn.GetStatusCode());

        switch (statusCode)
        {
            case 401:
                Debug.Log("ID 또는 비밀번호가 틀렸습니다.");
                break;


            case 403:
                Debug.Log(backendReturn.GetErrorCode());
                break;


            case 404:
                Debug.Log("game not found, game을 찾을 수 없습니다.");
                break;


            case 408:
                Debug.Log(backendReturn.GetMessage());
                break;


            case 409:
                Debug.Log("중복된 ID입니다.");
                break;

            case 410:
                Debug.Log("bad refreshToken, 잘못된 refreshToken 입니다.");
                break;

        }
    }
}
