using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSphere : MonoBehaviour
{

    private float normalSpeed = 6.5f;
    private Rigidbody rigidbody;

    public Transform sphereTrans, cameraTrans;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        cameraTrans = sphereTrans;
    }

    void Update()
    {
        ////Управление для Android
        //if (Input.touchCount > 0)
        //{
        //    GrabAndUpDir();
        //}
        //else
        //{
        //    NormalDir();
        //}

        //Управление для теста на ПК
        if (!Input.anyKey)
        {
            NormalDir();
        }
        if (Input.GetKey(KeyCode.Space))
        {
            GrabAndUpDir();
        }
    }

    //Свободное падение объекта по дуге вниз
    void NormalDir()
    {
        transform.RotateAround(transform.position, Vector3.back, 120 * Time.deltaTime);
        transform.Translate(new Vector3(1, 0) * normalSpeed * Time.deltaTime);
    }

    //Подъем объекта по дуге вверх
    void GrabAndUpDir()
    {
        transform.RotateAround(transform.position, Vector3.forward, 70 * Time.deltaTime);
        transform.Translate(new Vector3(1, 0) * normalSpeed * 1.25f * Time.deltaTime);
    }
}

