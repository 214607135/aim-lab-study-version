using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class UIManager : MonoBehaviour
{

    public InputField ballNrInput;
    public GameObject startMenu;
    public GameObject mode1Menu;
    public GameObject crossHair;
    public GameObject gameMode1Window;
    public Text timeCountDown;

    Vector3 cameraLocationInGame;
    Vector3 cameraVelocity;
    float cameraMoveSpeed;
    float cameraRotateSpeed;
    Vector3 cameraRotationInGame;

    private void Start()
    {
        // main start function
        // init all things here

        Camera.main.transform.position = new Vector3(0, 13, -10);
        Camera.main.transform.eulerAngles = new Vector3(0, -180, 0);

        cameraVelocity = Vector3.zero;
        cameraLocationInGame = new Vector3(0, 13, 24);
        cameraRotationInGame = Vector3.zero;

        cameraRotateSpeed = 20f; // 每秒钟多少度角

        crossHair.SetActive(false);
        mode1Menu.SetActive(false);
        startMenu.SetActive(true);


    }

    public void OnClickMode1()
    {
        //int ballSize = int.Parse(ballSizeInput.text);
        //ObjectGenerator.SetBallSize(ballSize);
        Cursor.visible = false;
        CloseAllUIShow();

        // 从 0 -180 0 旋转到 0 -90 0
        StartCoroutine(CameraMoveMode1());
    }

    IEnumerator CameraMoveMode1()
    {      
        float clip = cameraRotateSpeed * Time.deltaTime;
        float cnt = (180 - 90) / clip;
        int i = 0;
        while (i < cnt)
        {
            Camera.main.transform.Rotate(new Vector3(0, clip, 0));
            i++;
            yield return 0;
        }

        // show mode 1 setting UI
        mode1Menu.gameObject.SetActive(true);
        Cursor.visible = true;
    }

    public void OnClickMode1StartGame()
    {
        // mode 1 game start!
        Cursor.visible = false;
        CloseAllUIShow();

        string ballNumber = ballNrInput.text;
        int ballNr = 0;
        if (int.TryParse(ballNumber, out ballNr))
        {
            if (ballNr > 0)
            {
                ObjectGenerator.SetBallNr(ballNr);
            }
        }
        
        // 从 0 -90 0 旋转到 0 0 0
        StartCoroutine(CameraMoveMode1StartGame());
    }

    IEnumerator CameraMoveMode1StartGame()
    {
        float clip = cameraRotateSpeed * Time.deltaTime;
        float cnt = (180 - 90) / clip;
        int i = 0;
        float step = Vector3.Distance(Camera.main.transform.position, cameraLocationInGame) / cnt;
        while (i < cnt)
        {
            Camera.main.transform.Rotate(new Vector3(0, clip, 0));
            Camera.main.transform.position = Vector3.MoveTowards(Camera.main.transform.position, cameraLocationInGame, step);
            i++;
            yield return 0;
        }
        crossHair.SetActive(true);

        // 此时应该开始生成球对象，并倒计时三秒
        ObjectGenerator.OpenGenerator();
        gameMode1Window.SetActive(true);
        StartCoroutine(TimerCountDown(3));
    }

    private IEnumerator TimerCountDown(int totalTime)
    {
        timeCountDown.gameObject.SetActive(true);
        while (totalTime > 0)
        {
            timeCountDown.text = totalTime.ToString();
            yield return new WaitForSeconds(1);
            totalTime--;
        }
        timeCountDown.gameObject.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
    }

    void CloseAllUIShow()
    {
        startMenu.gameObject.SetActive(false);
        mode1Menu.gameObject.SetActive(false);
        crossHair.gameObject.SetActive(false);
        gameMode1Window.gameObject.SetActive(false);
    }
}
