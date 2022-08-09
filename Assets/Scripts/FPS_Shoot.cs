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
        if (ObjectGenerator.GetUpdateSwitch() && (Cursor.lockState == CursorLockMode.Locked))
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Input.GetMouseButtonDown(0))
            {
                if (Physics.Raycast(ray, out hitInfo))
                {
                    if (hitInfo.collider.gameObject.tag == "Enemy")
                    {
                        ObjectGenerator.DestroyBall(hitInfo.collider.gameObject);
                        UIManager.EffectClickAdd();
                    }
                }
                
                 UIManager.ClickAdd();
                
            }
        }
    }
}
