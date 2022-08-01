using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;

public class FPS_Shoot : MonoBehaviour
{
    Ray ray;
    RaycastHit hitInfo;
    // Update is called once per frame
    void Update()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(ray, out hitInfo))
            {
                print(hitInfo.collider.gameObject.name);
                ObjectGenerator.DestroyBall(hitInfo.collider.gameObject);
            }
        }
    }
}
