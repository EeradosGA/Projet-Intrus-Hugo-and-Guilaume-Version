using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToCall : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this);
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.G))
        //    this.LoadScene(SceneName.GAME);
        //if (Input.GetKeyDown(KeyCode.M))
        //    this.LoadScene(SceneName.MENU);
    }
}
