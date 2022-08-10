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
                    Debug.Log(hitInfo.collider.gameObject.name);
                    if (hitInfo.collider.gameObject.tag == "Enemy")
                    {
                        Explode(hitInfo.point);
                        ObjectGenerator.RemoveBall(hitInfo.collider.gameObject.transform.parent.gameObject);
                        //Destroy(hitInfo.collider.gameObject.transform.parent.gameObject);
                        UIManager.EffectClickAdd();
                    }
                }
                 UIManager.ClickAdd();
            }
        }
    }


    private float radius = 5f;
    private float power = 50F;
    public void Explode(Vector3 hitPoint)
    {
        Vector3 explosionPos = hitPoint;
        Collider[] colliders = Physics.OverlapSphere(explosionPos, radius);
        foreach (Collider hit in colliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();

            if (rb != null)
            {
                rb.AddExplosionForce(power, explosionPos, radius, 3.0F);
            }
            
        }
    }
}
