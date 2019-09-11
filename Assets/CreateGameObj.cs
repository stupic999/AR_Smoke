using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateGameObj : MonoBehaviour
{
    [SerializeField]
    GameObject good;

    [SerializeField]
    GameObject bad;

    [SerializeField]
    GameObject smoke;

    Transform self;

    // Start is called before the first frame update
    void Start()
    {
        self = GetComponent<Transform>();
        CreateObj(1, bad);
        CreateObj(1, good);
        CreateObj(1, smoke);
    }

    void CreateObj(int Time,GameObject Obj)
    {
        for (int i = 0; i < Time; i++)
        {
            float randX = Random.Range(-0.45f, 0.45f);
            float randZ = Random.Range(-0.45f, 0.45f);
            Vector3 ObjPos = new Vector3(randX, 0.05f, randZ);
            Instantiate(Obj, transform.position + ObjPos, transform.rotation, self);
        }
    }
}