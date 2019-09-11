using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateGameObj : MonoBehaviour
{
    [SerializeField]
    GameObject good;

    [SerializeField]
    GameObject badTop;

    [SerializeField]
    GameObject badMiddle;

    [SerializeField]
    GameObject badBottom;

    [SerializeField]
    GameObject smoke;

    Transform self;

    // 重生（用協程，或者invokeRepeating）
    // 解決重疊（劃分格子，用陣列）
    // 地板黃色的消失（看youTube）
    // 調整位置按鈕（那兩個script就enable）


    // Start is called before the first frame update
    void Start()
    {
        self = GetComponent<Transform>();
        CreateObj(1, 0.25f, badTop);
        CreateObj(1, 0.125f, badMiddle);
        CreateObj(1, 0.05f, badBottom);
        CreateObj(1, 0.05f, good);
        CreateObj(1, 0.05f, smoke);
    }

    void CreateObj(int Time,float PosY,GameObject Obj)
    {
        for (int i = 0; i < Time; i++)
        {
            float randX = Random.Range(-0.45f, 0.45f);
            float randZ = Random.Range(-0.45f, 0.45f);
            Vector3 ObjPos = new Vector3(randX, PosY, randZ);
            Instantiate(Obj, transform.position + ObjPos, transform.rotation, self);
        }
    }
}