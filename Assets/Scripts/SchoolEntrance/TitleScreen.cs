using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScreen : MonoBehaviour
{

    [SerializeField] Dialogue entranceDialogue;
    [SerializeField] GameObject titleScreen;
    private bool isStarted = false;

    void Start() {
        globales.nextClass = 1;
        globales.clasesAsistidas = 0;
        globales.spawnPos = new Vector3(-14, -3, 0);

        GameObject.Find("Player").GetComponent<PlayerMovement>().stop = true;
        GameObject.Find("Player").GetComponent<PlayerMovement>().speed = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space") && !isStarted)
        {
            GetComponent<CanvasGroup>().alpha = 0;
            GameObject.FindGameObjectWithTag("DialogueManager").GetComponent<DialogueManager>().StartDialogue(GetComponent<DialogueTrigger>(), entranceDialogue);
            isStarted = true;
        }
    }   
}
