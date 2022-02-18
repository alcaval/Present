using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLoader : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if(globales.nextClass == 4){
            globales.nextClass = 1;
            SceneManager.LoadScene("ExitScene");
        }
    }
}
