using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class RayCastTag : MonoBehaviour
{
    Vector2 touchPosition;

    [SerializeField]
    Camera arCamare;

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            touchPosition = touch.position;

            if (touch.phase == TouchPhase.Began)
            {
                Ray ray = arCamare.ScreenPointToRay(touch.position);
                RaycastHit hitObj;

                if (Physics.Raycast(ray, out hitObj))
                {
                    if(hitObj.transform.tag=="Good")
                    {
                        Destroy(hitObj.transform.gameObject);
                    }
                    if (hitObj.transform.tag == "Bad")
                    {
                        Destroy(hitObj.transform.gameObject);
                    }
                    if (hitObj.transform.tag == "Smoke")
                    {
                        Destroy(hitObj.transform.gameObject);
                    }
                }



            }

        }
    }

}
