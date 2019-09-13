using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RayDes : MonoBehaviour
{
    Camera arCamera;
    Text IDK;

    void Start()
    {
        arCamera = GetComponent<Camera>();
        IDK = GameObject.Find("IDK").GetComponent<Text>();
    }

    private void Update()
    {
        RaycastHit hitObj;
        Ray ray = arCamera.ScreenPointToRay(new Vector2(Screen.width / 2, Screen.height / 2));
        if (Physics.Raycast(ray, out hitObj))
        {
            IDK.text = hitObj.transform.name;
        }
    }
}
