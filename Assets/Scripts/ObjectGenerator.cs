using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;

public class ObjectGenerator : MonoBehaviour
{
    public Transform backGround;

    private float windowLength;
    private float windowHeight;

    private static List<GameObject> balls = new List<GameObject>();
    private static int numberOfBalls = 0;
    private static float ballRadius = 5f;
    private static int ballMaxNumber = 8;

    private static bool updateSwitch = false;



    // Start is called before the first frame update
    void Start()
    {
        windowLength = backGround.localScale.x / 2;
        windowHeight = backGround.localScale.y / 2;
        balls.Clear();
    }

    float xLoc, yLoc;
    void FixedUpdate()
    {
        if (updateSwitch)
        {
            numberOfBalls = balls.Count;
            if (numberOfBalls < ballMaxNumber)
            {
                xLoc = backGround.position.x + Random.Range(-windowLength, windowLength);
                yLoc = backGround.position.y + Random.Range(-windowHeight, windowHeight);

                bool tooClose = false;

                if (balls.Count != 0)
                {
                    foreach (GameObject gameObject in balls)
                    {
                        if (GetClose(xLoc, yLoc, gameObject))
                        {
                            tooClose = true;
                            break;
                        }
                    }
                }

                if (!tooClose)
                {
                    GameObject go = Instantiate(Resources.Load("Prefab/ball") as GameObject);
                    go.transform.localScale = new Vector3(ballRadius, ballRadius, ballRadius);
                    go.transform.position = new Vector3(xLoc, yLoc, backGround.position.z);
                    balls.Add(go);
                }
            }
        }
    }

    public bool GetClose(float x, float y, GameObject ball)
    {
        float distanceX = Mathf.Abs(x - ball.transform.position.x);
        float distanceY = Mathf.Abs(y - ball.transform.position.y);
        double radius = 0.5 * ball.transform.localScale.x;
        double distance = Mathf.Sqrt(Mathf.Pow(distanceX, 2) + Mathf.Pow(distanceY, 2));
        
        if (distance >= 2 * radius + 5)
        {
            return false;
        } else
        {
            return true;
        }
    }

    public static void RemoveBall(GameObject go)
    {
        balls.Remove(go);
    }

    public static void SetBallSize(int ballSize)
    {
        ballRadius = ballSize;
    }
    public static void SetBallNr(int ballNr)
    {
        ballMaxNumber = ballNr;
    }

    public static void OpenGenerator()
    {
        updateSwitch = true;
    }
    public static void CloseGenerator()
    {
        updateSwitch = false;
    }

    public static bool GetUpdateSwitch()
    {
        return updateSwitch;
    }

    public static void ClearBalls()
    {
        foreach (GameObject ball in balls) {
            Destroy(ball);
        }
        balls.Clear();
    }
}
