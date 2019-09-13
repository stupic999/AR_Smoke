using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.UI;

public class RayCastTag : MonoBehaviour
{
    Vector2 touchPosition;
    Camera arCamare;
    Text bulletNumText;
    Text badScoreText;
    Text goodScoreText;
    Text IDK;

    [SerializeField]
    GameObject bad;

    [SerializeField]
    GameObject smoke;

    int bulletNum;
    int badScore;
    int goodScore;

    Transform self;

    void Start()
    {
        self = GetComponent<Transform>();
        bulletNumText = GameObject.Find("BulletNumText").GetComponent<Text>();
        badScoreText = GameObject.Find("BadScoreText").GetComponent<Text>();
        goodScoreText = GameObject.Find("GoodScoreText").GetComponent<Text>();
        IDK = GameObject.Find("IDK").GetComponent<Text>();
        arCamare = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        bulletNumText.text = "子彈數量 : " + bulletNum;
        badScoreText.text = "被射器官數 : " + badScore;
        goodScoreText.text = "被射垃圾桶數 : " + goodScore;        
    }

    void Update()
    {
        if (Input.touchCount > 0) 
        {
            Touch touch = Input.GetTouch(0);

            touchPosition = touch.position;

            if (touch.phase == TouchPhase.Began)
            {
                //Ray ray = arCamare.ScreenPointToRay(touch.position);
                Ray ray = arCamare.ScreenPointToRay(new Vector2(Screen.width / 2, Screen.height / 2));
                RaycastHit hitObj;

                if (Physics.Raycast(ray, out hitObj))
                {
                    if (hitObj.transform.tag == "Smoke")
                    {
                        Destroy(hitObj.transform.gameObject);
                        bulletNum += 8;
                        bulletNumText.text = "子彈數量new : " + bulletNum;
                        Invoke("CreateSmoke", 0.5f);
                        IDK.text = hitObj.transform.name;
                    }
                    else if (hitObj.transform.tag == "Good" && bulletNum > 0)
                    {
                        goodScore++;
                        bulletNum--;
                        goodScoreText.text = "被射垃圾桶數new : " + goodScore;
                        bulletNumText.text = "子彈數量new : " + bulletNum;
                        IDK.text = hitObj.transform.name;
                    }
                    else if (hitObj.transform.tag == "Bad" && bulletNum > 0)
                    {
                        Destroy(hitObj.transform.gameObject);
                        bulletNum--;
                        badScore++;
                        badScoreText.text = "被射器官數new : " + badScore;
                        bulletNumText.text = "子彈數量new : " + bulletNum;
                        Invoke("CreateBad", 0.5f);
                        IDK.text = hitObj.transform.name;
                    }
                    else
                    {
                        IDK.text = hitObj.transform.name;
                    }
                }
            }
        }
    }

    void CreateBad()
    {
        float randX = Random.Range(-0.45f, 0.45f);
        float randZ = Random.Range(-0.45f, 0.45f);
        Vector3 ObjPos = new Vector3(randX, 0.05f, randZ);
        Instantiate(bad, transform.position + ObjPos, transform.rotation, self);
    }

    void CreateSmoke()
    {
        float randX = Random.Range(-0.45f, 0.45f);
        float randZ = Random.Range(-0.45f, 0.45f);
        Vector3 ObjPos = new Vector3(randX, 0.05f, randZ);
        Instantiate(smoke, transform.position + ObjPos, transform.rotation, self);
    }
}
