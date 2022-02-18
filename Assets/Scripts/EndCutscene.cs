using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndCutscene : MonoBehaviour
{
    [SerializeField]
    private GameObject player;

    [SerializeField]
    private Animator transition;

    [SerializeField]
    private Dialogue DialogueCero, DialogueUno, DialogueDos, DialogueTres;

    [SerializeField]
    private AudioSource source;
    [SerializeField]
    private AudioClip musicCero, musicUno, musicDos, musicTres;

    private Rigidbody2D rb;
    private bool move = false;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CutscenePart1());
        rb = player.GetComponent<Rigidbody2D>();

        print(globales.clasesAsistidas);

        switch (globales.clasesAsistidas)
        {
            case 0:
                source.clip = musicCero;
                break;
            case 1:
                source.clip = musicUno;
                break;
            case 2:
                source.clip = musicDos;
                break;
            case 3:
                source.clip = musicTres;
                break;
            default:
                source.clip = musicUno;
                break;
        }

        source.Play();
    }

    public void StartCutscenePart2() => StartCoroutine(CutscenePart2());

    private void FixedUpdate()
    {
        if (!move) return;
        rb.MovePosition(rb.position + Vector2.left * 5f * Time.fixedDeltaTime);
    }

    IEnumerator CutscenePart1()
    {

        print("Empieza el metodo cutscene");
        move = true;
        player.GetComponent<Animator>().SetBool("moveDown", true);
        yield return new WaitForSeconds(1.7f);
        move = false;
        player.GetComponent<Animator>().SetBool("moveDown", false);

        yield return new WaitForSeconds(1f);

        //DialogueTrigger trigger = GetComponent<DialogueTrigger>();
        print(globales.clasesAsistidas);

        switch (globales.clasesAsistidas)
        {
            case 0:
                print("Dialogo ha ido a 0 clases");
                GameObject.FindGameObjectWithTag("DialogueManager").GetComponent<DialogueManager>().StartDialogue(GetComponent<DialogueTrigger>(), DialogueCero);
                //manager.StartDialogue(trigger, DialogueCero);
                break;
            case 1:
                print("Dialogo ha ido a 1 clase");
                GameObject.FindGameObjectWithTag("DialogueManager").GetComponent<DialogueManager>().StartDialogue(GetComponent<DialogueTrigger>(), DialogueUno);
                //manager.StartDialogue(trigger, DialogueUno);
                break;
            case 2:
                print("Dialogo ha ido a 2 clases");
                GameObject.FindGameObjectWithTag("DialogueManager").GetComponent<DialogueManager>().StartDialogue(GetComponent<DialogueTrigger>(), DialogueDos);
                //manager.StartDialogue(trigger, DialogueDos);
                break;
            case 3:
                print("Dialogo ha ido a 3 clases");
                GameObject.FindGameObjectWithTag("DialogueManager").GetComponent<DialogueManager>().StartDialogue(GetComponent<DialogueTrigger>(), DialogueTres);
                //manager.StartDialogue(trigger, DialogueTres);
                break;
            default:
                print("Lol wat bug error");
                GameObject.FindGameObjectWithTag("DialogueManager").GetComponent<DialogueManager>().StartDialogue(GetComponent<DialogueTrigger>(), DialogueUno);
                //manager.StartDialogue(trigger, DialogueUno);
                break;
        }

        // ! Se asigna un trigger a Cutscene Manager para ejecutar una funcion post dialogo.
    }

    IEnumerator CutscenePart2()
    {
        print("Continua el metodo cutsene uwus");
        move = true;
        player.GetComponent<Animator>().SetBool("moveDown", true);
        yield return new WaitForSeconds(1.7f);
        move = false;
        player.GetComponent<Animator>().SetBool("moveDown", false);

        transition.SetTrigger("Start");
        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene("SchoolEntrance");
    }
}
