using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour {

    private Vector3 currSavePos;

    void Start () {
        currSavePos.x = PlayerPrefs.GetFloat("checkX");
        currSavePos.y = PlayerPrefs.GetFloat("checkY");
        transform.position = currSavePos;
    }

    //Проверка на касание стен объектом
    private void OnTriggerEnter(Collider coll)
    {
        if(coll.tag == "deathZone")
        {
            Application.LoadLevel(Application.loadedLevel);
        }
    }
}
