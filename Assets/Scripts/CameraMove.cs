using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    float xMove = 0;
    float yMove = 0;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Cursor.lockState == CursorLockMode.Locked)
        {
            xMove += Input.GetAxis("Mouse X");
            yMove += Input.GetAxis("Mouse Y");

            xMove = Mathf.Clamp(xMove, -50, 50);
            yMove = Mathf.Clamp(yMove, -40, 40);

            this.transform.eulerAngles = new Vector3(-yMove, xMove, 0);
        }
    }
}
