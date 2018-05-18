using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JustDontDie : MonoBehaviour {

    //Unity3D дал баг с освещением, пришлось сделать прямой источник освещения "бессмертным"
    void Awake()
    {
        GameObject.DontDestroyOnLoad(gameObject);
    }
	
}
