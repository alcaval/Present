using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class SceneLoader : MonoBehaviour

{
    [SerializeField] private Animator transition;
    [SerializeField] private string sceneToLoad;
    [SerializeField] private float transitionTime;
    [SerializeField] int id = 0;

    public bool isInRange = false;
    [SerializeField] GameObject player = null;

    [Header("Dialogos")]
    [SerializeField] private DialogueManager manager = null;


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            // print("Estas dentro");
            isInRange = true;
            //Animacion press something
            GameObject[] guards = GameObject.FindGameObjectsWithTag("1");
            foreach (GameObject g in guards)
            {
                if (g != gameObject & g.GetComponent<guardBehaviour>().afterStun)
                {
                    g.GetComponent<guardBehaviour>().reset();
                    g.GetComponent<guardBehaviour>().afterStun = false;
                    print(g + " ______ ");
                }
            }

            guards = GameObject.FindGameObjectsWithTag("2");
            foreach (GameObject g in guards)
            {
                if (g != gameObject & g.GetComponent<guardBehaviour>().afterStun)
                {
                    g.GetComponent<guardBehaviour>().reset();
                    g.GetComponent<guardBehaviour>().afterStun = false;
                    print(g + " ______ ");
                }
            }

            guards = GameObject.FindGameObjectsWithTag("3");
            foreach (GameObject g in guards)
            {
                if (g != gameObject & g.GetComponent<guardBehaviour>().afterStun)
                {
                    g.GetComponent<guardBehaviour>().reset();
                    g.GetComponent<guardBehaviour>().afterStun = false;
                    print(g + " ______ ");
                }
            }

            GameObject.Find("spaceBar").GetComponent<SpriteRenderer>().enabled = true;
            GameObject.Find("spaceBar2").GetComponent<SpriteRenderer>().enabled = true;
            GameObject.Find("spaceBar3").GetComponent<SpriteRenderer>().enabled = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            // print("Byeee");
            isInRange = false;
            //Animacion press something stop
            GameObject.Find("spaceBar").GetComponent<SpriteRenderer>().enabled = false;
            GameObject.Find("spaceBar2").GetComponent<SpriteRenderer>().enabled = false;
            GameObject.Find("spaceBar3").GetComponent<SpriteRenderer>().enabled = false;
        }
    }

    void Update()
    {
        // print("hols");
        if (isInRange)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                print("Pulsaste espacio");
                if (id == player.GetComponent<PlayerMovement>().getNextClass())
                {
                    globales.spawnPos = transform.position;
                    StartCoroutine(LoadNextScene());
                }

            }
        }
    }

    public void ClassFinished()
    {
        globales.nextClass++;
        StartCoroutine(LoadNextScene());
    }

    IEnumerator LoadNextScene()
    {
        string scene = SceneManager.GetActiveScene().name;
        GameObject dialogosPostClase = GameObject.Find("DialogosPostClase");

        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(sceneToLoad);

        // Si es desde una clase activamos el dialogo post clase de ese SceneLoader
        // (cada uno es diferente, no es un objeto permanente)
        Dialogue aux = null;
        switch(scene)
        {
            case "Classroom1":
            aux = dialogosPostClase.GetComponent<DialogosPostClase>().dialogoUno;
            dialogosPostClase.GetComponent<DialogueTrigger>().StartDialogue(aux);
            break;
            case "Classroom2":
            aux = dialogosPostClase.GetComponent<DialogosPostClase>().dialogoDos;
            dialogosPostClase.GetComponent<DialogueTrigger>().StartDialogue(aux);
            break;
            default:
            break;
        }
    }
}
