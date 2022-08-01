using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    public InputField ballSizeInput;
    public GameObject startMenu;
    public GameObject mode1Menu;
    public GameObject crossHair;

    Vector3 cameraLocationInGame;
    Vector3 cameraVelocity;
    float cameraMoveSpeed;
    float cameraRotateSpeed;
    Vector3 cameraRotationInGame;

    private bool moveCameraMode1 = false;
    private bool moveCameraMode1Start = false;
    private bool gameStart = false;



    private void Start()
    {
        // main start function
        // init all things here

        Camera.main.transform.position = new Vector3(0, 13, -10);
        Camera.main.transform.eulerAngles = new Vector3(0, -180, 0);

        cameraVelocity = Vector3.zero;
        cameraLocationInGame = new Vector3(0, 13, 24);
        cameraRotationInGame = Vector3.zero;

        cameraMoveSpeed = 10f;
        cameraRotateSpeed = 1f;

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
        moveCameraMode1 = true;
    }

    public void OnClickMode1StartGame()
    {
        // mode 1 game start!
        Cursor.visible = false;
        CloseAllUIShow();
        moveCameraMode1Start = true;
    }
    
    void Update()
    {

        if (moveCameraMode1)
        {
            // 在进入游戏模式1的设置模式中时发生
            UpdateCameraMoveMode1();
        }
        
        if (moveCameraMode1Start)
        {
            // 在准备进入游戏时发生，摄像头的移动
            UpdateCameraMoveMode1Start();
        }

        if (gameStart)
        {
            // 在游戏正式开始时发生
            Cursor.lockState = CursorLockMode.Locked;
        }
       

    }

    void UpdateCameraMoveMode1()
    {
        Camera.main.transform.rotation = Quaternion.Slerp(Camera.main.transform.rotation, Quaternion.Euler(0, -90, 0), cameraRotateSpeed * Time.deltaTime);

        if (Camera.main.transform.rotation == Quaternion.Euler(0, -90, 0))
        {
            moveCameraMode1 = false;
            // show mode 1 setting UI
            mode1Menu.gameObject.SetActive(true);
            Cursor.visible = true;
        }
    }

    void UpdateCameraMoveMode1Start()
    {
        float step = cameraMoveSpeed * Time.deltaTime;
        Camera.main.transform.position = Vector3.MoveTowards(Camera.main.transform.position, cameraLocationInGame, step);

        Camera.main.transform.rotation = Quaternion.Slerp(Camera.main.transform.rotation, Quaternion.Euler(0, 0, 0), cameraRotateSpeed * Time.deltaTime);

        if (Camera.main.transform.position == cameraLocationInGame)
        {
            moveCameraMode1Start = false;
            gameStart = true;
            crossHair.SetActive(true);
        }
    }

    void CloseAllUIShow()
    {
        startMenu.gameObject.SetActive(false);
        mode1Menu.gameObject.SetActive(false);
        crossHair.gameObject.SetActive(false);
    }
}
