using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class closeToGuard : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if(GameObject.Find("Player").transform.position.x >= 7.5){
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
        }else if(GameObject.Find("Player").transform.position.x >= 3){
            gameObject.GetComponent<SpriteRenderer>().enabled = true;
        }else{
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
        }
    }
}
