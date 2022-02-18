using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectDontDestroyOnLoad : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }
}
