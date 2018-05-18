using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitGame : MonoBehaviour {

    //Выход из игры
    public void OnMouseUp()
    {
        Application.Quit();
        Debug.Log("Вышел из игры");
    }
}
