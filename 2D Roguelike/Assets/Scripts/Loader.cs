using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loader : MonoBehaviour
{
    // loader는 Gamemanager 가 instance 되었는지를 확인한다.
    // 만약 안되어있다면, prefab에 있는 gamemanager를 instantiate하도록 만든다. 

    public GameObject gameManager;
    // Start is called before the first frame update
    void Start()
    {
        if(GameManager.instance == null)
        {
            Instantiate(gameManager);
        }
    }
}
