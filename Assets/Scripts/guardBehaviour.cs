using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class guardBehaviour : MonoBehaviour
{

    private bool terminado = false;
    private bool resolved = false;
    [SerializeField] private GameObject player;
    [SerializeField] private Transform[] waypoints;
    [SerializeField] private Animator anim;
    private int wpIndex = 0;

    [SerializeField] private GameObject spawnPosition;
    [SerializeField] private Animator transition;
    [SerializeField] private Transform pflos;

    private LineOfSight los = null;
    bool anotherFlag = false;

    private Vector2 originalP = Vector2.zero;
    private bool stunnedF = false;
    private bool reseteable = true;
    public bool afterStun = false;
    [SerializeField] Dialogue afterStunDialogue = null;

    [SerializeField] private DialogueTrigger triggerDetention = null;
    private float speed = 2f;

    // Start is called before the first frame update
    void Start()
    {
        los = Instantiate(pflos, null).GetComponent<LineOfSight>();
        StartCoroutine(routine());

        if(tag != "1") reseteable = false;

        triggerDetention = GameObject.FindGameObjectWithTag("triggerDetention").GetComponent<DialogueTrigger>();
    }

    // Update is called once per frame
    void Update()
    {
        originalP = transform.position;
        los.setOrigin(transform.position);
    }

    IEnumerator routine()
    {
        speed = 2f;
        while(true){
            if(tag == "1" & player.transform.position.x > GameObject.FindGameObjectWithTag("Pass1").gameObject.transform.position.x){
                if(reseteable){
                    reset();
                }
            }if(tag == "2" & player.transform.position.x < GameObject.FindGameObjectWithTag("Pass2").gameObject.transform.position.x){ //Pasillo2
                if(reseteable){
                    reset();
                }
            }if(tag == "3" & player.transform.position.y > GameObject.FindGameObjectWithTag("Pass3").gameObject.transform.position.y){ //Pasillo3
                if(reseteable){
                    reset();
                }
            }if(tag == "Hub" & player.transform.position.x < GameObject.FindGameObjectWithTag("Pass1").gameObject.transform.position.x
                || player.transform.position.x > GameObject.FindGameObjectWithTag("Pass2").gameObject.transform.position.x
                || player.transform.position.y < GameObject.FindGameObjectWithTag("Pass3").gameObject.transform.position.y){ //HUB
                if(reseteable){
                    reset();
                }
            }else{
                reseteable = true;
            }

            los.SetAimDirection((waypoints[wpIndex].position - transform.position));
            if(wpIndex <= waypoints.Length - 1 & !los.lookForPlayer()) 
            {
                anim.SetBool("moving", true);
                if((waypoints[wpIndex].transform.position - transform.position).normalized.x < 0)
                {
                    this.GetComponent<SpriteRenderer>().flipX = true;
                }else{
                    this.GetComponent<SpriteRenderer>().flipX = false;
                }
                transform.position = Vector2.MoveTowards(transform.position, waypoints[wpIndex].transform.position, speed * Time.deltaTime);
            }

            if(Vector2.Distance(transform.position,waypoints[wpIndex].transform.position) < 0.1f & !los.lookForPlayer()){
                if(waypoints[wpIndex].tag == "turn"){
                    anim.SetBool("moving", false);
                    yield return lookToSide();
                    yield return lookToOtherSide();
                }

                wpIndex++;
                if(wpIndex > waypoints.Length - 1){
                    Array.Reverse(waypoints);
                    wpIndex = 0;
                }
            }
            
            if(los.lookForPlayer() & !resolved){
                los.SetAimDirection((player.transform.position - transform.position).normalized);
                if(!anotherFlag){
                    print("que la pasa");
                    player.GetComponent<PlayerMovement>().setSpeed(0f);
                    player.GetComponent<PlayerMovement>().stop = true;
                    //player.gameObject.GetComponentInChildren<BoxCollider2D>().gameObject.SetActive(false);
                    GameObject[] guards = GetComponentInParent<GuardManager>().getAllGuards();
                    foreach(GameObject g in guards){
                        if(g != gameObject & g.GetComponent<guardBehaviour>().afterStun){
                            g.GetComponent<guardBehaviour>().reset();
                            g.GetComponent<guardBehaviour>().afterStun = false;
                            print(g + " ______ " );
                        }
                    }
                }

                if(Vector2.Distance(transform.position, player.transform.position) > 1.5){
                    transform.position = Vector2.MoveTowards(transform.position, player.transform.position, 3 * Time.deltaTime);
                }
                else
                {
                    anim.SetBool("moving", false);
                    player.GetComponent<PlayerMovement>().setSpeed(0f); 

                    if(afterStun){
                        GameObject.FindGameObjectWithTag("DialogueManager").GetComponent<DialogueManager>().StartDialogue(GetComponent<DialogueTrigger>(), afterStunDialogue);
                        afterStun = false;
                    }

                    if(!terminado)
                    {
                        // if(afterStun){
                        //     print("joj");
                        //     GameObject.FindGameObjectWithTag("DialogueManager").GetComponent<DialogueManager>().StartDialogue(GetComponent<DialogueTrigger>(), afterStunDialogue);
                        // }else{
                            GetComponent<DialogueTrigger>().StartDialogue();
                            player.GetComponent<PlayerMovement>().getArea().gameObject.SetActive(false);
                        //}
                        terminado = true;
                    }
                }
            }

            if(stunnedF){
                yield return stunned();
                stunnedF = false;
                resolved = false;
                anotherFlag = true;
                los.activate();
            } 
            yield return null;
        }
    }

    IEnumerator lookToSide(){
        los.SetAimDirection((Vector3.left));
        yield return new WaitForSeconds(1.5f); 
        if(los.lookForPlayer()){
            print("la madre que me pario");
            yield break;
        }
    }

    IEnumerator lookToOtherSide(){
        los.SetAimDirection((Vector3.right));
        
        yield return new WaitForSeconds(1.5f);
        if(los.lookForPlayer()){
            print("la madre que me pario");
            yield break;
        }
        
    }

    IEnumerator stunned(){
        yield return new WaitForSeconds(1);
        player.GetComponent<PlayerMovement>().getArea().gameObject.SetActive(true);
        los.deactivate();
    }

    IEnumerator reseteo(){
        reset();
        yield return new WaitForSeconds(1);
    }

    public void Stun()
    {
        player.GetComponent<PlayerMovement>().setSpeed(5f);
        los.deactivate();
        //los.gameObject.SetActive(false);
        resolved = true;
        stunnedF = true;
        afterStun = true;
        
        // resolved = true;
    }

    public void DetentionRoom()
    {
        reset();
        GameObject[] guards = GetComponentInParent<GuardManager>().getAllGuards();
        foreach(GameObject g in guards){
            if(g != gameObject){
                g.GetComponent<guardBehaviour>().reset();
                g.GetComponent<guardBehaviour>().los.deactivate();
                print(g + " ______ " );
            }
        }
        // ! spawn in detention room y lanzar monologo de la prota sobre la info de la clase que le toca y la oportunidad perdida
        StartCoroutine(DetentionRoomAux());
    }

    IEnumerator DetentionRoomAux()
    {  
        player.GetComponent<PlayerMovement>().getArea().gameObject.SetActive(false);
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(2f);
        player.transform.position = spawnPosition.transform.position;
        player.GetComponent<PlayerMovement>().nextClass();
        //print(player.GetComponent<PlayerMovement>().getNextClass());
        transition.SetTrigger("End");
        player.GetComponent<PlayerMovement>().getArea().gameObject.SetActive(true);
        player.GetComponent<PlayerMovement>().setSpeed(5f);

        DialogosPostClase dialogosPostClase = GameObject.Find("DialogosPostClase").GetComponent<DialogosPostClase>();
        DialogueManager manager = GameObject.FindGameObjectWithTag("DialogueManager").GetComponent<DialogueManager>();
        switch(globales.nextClass)
        {
            case 2:
            triggerDetention.StartDialogue();
            manager.AddDialogue(dialogosPostClase.dialogoUno);
            break;
            case 3:
            triggerDetention.StartDialogue();
            manager.AddDialogue(dialogosPostClase.dialogoDos);
            break;
        }
        yield return new WaitForSeconds(3f);
        //player.GetComponentInChildren<BoxCollider2D>().gameObject.SetActive(true);
    }

    public void reset()
    {
        speed = 100f;
        los.deactivate();
        StopAllCoroutines();
        //transform.position = originalP;
        terminado = false;
        resolved = false;
        wpIndex = 0;
        anotherFlag = false;
        reseteable = false;
        StartCoroutine(routine());
    }

    public void setReseteable() { reseteable = true; }
}
