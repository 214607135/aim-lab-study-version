using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class UIManager : MonoBehaviour
{

    public InputField ballNrInput;
    public InputField textTimeSetting;

    public GameObject startMenu;
    public GameObject mode1Menu;
    public GameObject crossHair;
    public GameObject gameMode1Window;
    public GameObject gameSettingWindow;
    public GameObject gameEndWindow;

    public Text timeCountDown;
    public Text textScore;
    public Text textHitRate;
    public Text textGameTime;
    public Text textFinalScore;
    public Text textFinalRate;
 
    Vector3 cameraLocationInGame;
    float cameraMoveSpeed;
    float cameraRotateSpeed;

    static float totalClickCount = 0;
    static float validClickCount = 0;
    private float hitRate = 0;
    private float score = 0;
    private double gamePassTime = 0;
    private int gameSettingTime = 0;
    

    private void Start()
    {
        // main start function
        // init all things here

        Camera.main.transform.position = new Vector3(0, 13, -10);
        Camera.main.transform.eulerAngles = new Vector3(0, -180, 0);

        cameraLocationInGame = new Vector3(0, 13, 24);

        cameraRotateSpeed = 20f; // 每秒钟多少度角



        crossHair.SetActive(false);
        mode1Menu.SetActive(false);
        startMenu.SetActive(true);
        gameMode1Window.SetActive(false);
        gameSettingWindow.SetActive(false);
        gameEndWindow.SetActive(false);
        textGameTime.gameObject.SetActive(false);
    }

    public void OnClickMode1()
    {
        //int ballSize = int.Parse(ballSizeInput.text);
        //ObjectGenerator.SetBallSize(ballSize);
        Cursor.visible = false;
        CloseAllUIShow();

        textTimeSetting.text = "";
        ballNrInput.text = "";

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

        gameSettingTime = Int32.Parse(textTimeSetting.text);
        textGameTime.text = "Time Left: " + gameSettingTime.ToString();

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
        textGameTime.gameObject.SetActive(true);
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

    private void PauseGame()
    {
        Time.timeScale = 0;
        Cursor.visible = true;
        gameSettingWindow.SetActive(true);
        ObjectGenerator.CloseGenerator();
        Cursor.lockState = CursorLockMode.None;
    }

    public void OnClickResumeGame()
    {
        Time.timeScale = 1;
        Cursor.visible = false;
        gameSettingWindow.SetActive(false);
        StartCoroutine(TimerCountDown(3));
        ObjectGenerator.OpenGenerator();
    }


    void CloseAllUIShow()
    {
        startMenu.gameObject.SetActive(false);
        mode1Menu.gameObject.SetActive(false);
        crossHair.gameObject.SetActive(false);
        gameMode1Window.gameObject.SetActive(false);
    }

    public static void ClickAdd()
    {
        totalClickCount++;
    }
    public static void EffectClickAdd()
    {
        validClickCount++;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && ObjectGenerator.GetUpdateSwitch() && Cursor.lockState == CursorLockMode.Locked)
        {
            PauseGame();
        }

        if (ObjectGenerator.GetUpdateSwitch() && Cursor.lockState == CursorLockMode.Locked)
        {
            gamePassTime += Time.deltaTime;
            if (gamePassTime >= gameSettingTime)
            {
                GameEnd();
            }
            textGameTime.text = "Time Left: " + Math.Round(gameSettingTime - gamePassTime).ToString();
        }

        if (totalClickCount == 0)
        {
            hitRate = 0;
        } else
        {
            hitRate = validClickCount / totalClickCount * 100;
        }
        score = validClickCount * 100;

        textScore.text = "Score: " + score.ToString();
        textHitRate.text = "Hit Rate: " + hitRate.ToString("f2") + "%";
    }

    private void GameEnd()
    {
        textGameTime.gameObject.SetActive(false);
        gameMode1Window.SetActive(false);
        ObjectGenerator.CloseGenerator();
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        textFinalScore.text = textScore.text;
        textFinalRate.text = "Hit Rate: " + hitRate.ToString("f2") + "%";
        gameEndWindow.SetActive(true);
    }

    public void BackToMainMenu()
    {
        Time.timeScale = 1;
        startMenu.SetActive(true);
        crossHair.SetActive(false);
        mode1Menu.SetActive(false);
        gameMode1Window.SetActive(false);
        gameSettingWindow.SetActive(false);
        gameEndWindow.SetActive(false);
        textGameTime.gameObject.SetActive(false);

        // 清空游戏数据
        ObjectGenerator.ClearBalls();
        validClickCount = 0;
        totalClickCount = 0;
        score = 0;
        hitRate = 0;
        gamePassTime = 0;
        gameSettingTime = 0;
        textScore.text = "Score: " + score.ToString();
        textHitRate.text = "Hit Rate: " + hitRate.ToString("f2") + "%";
        textGameTime.text = "Time Left: " + Math.Round(20 - gamePassTime).ToString();

        Camera.main.transform.position = new Vector3(0, 13, -10);
        Camera.main.transform.eulerAngles = new Vector3(0, -180, 0);
    }

}
