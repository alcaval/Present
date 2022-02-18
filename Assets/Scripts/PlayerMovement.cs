using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Animator anim;
    public float speed = 5f;
    public Rigidbody2D rb;
    private Vector2 movement = Vector2.zero;
    [SerializeField] Transform[] passages;
    [SerializeField] GameObject camara;
    [SerializeField] BoxCollider2D area;
    bool pasillo1 = true;
    bool hub = false;
    bool pasillo2 = false;
    bool pasillo3 = false;
    bool lerping = false;
    Vector3 bg = new Vector3();
    int nClass = 1;

    public bool stop = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        //print(transform.position.x);
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        if (movement.normalized.x < 0)
        {
            this.GetComponent<SpriteRenderer>().flipX = true;
        }
        else
        {
            this.GetComponent<SpriteRenderer>().flipX = false;
        }
        if (movement != Vector2.zero & !stop)
        {
            anim.SetBool("moveDown", true);
        }
        else
        {
            anim.SetBool("moveDown", false);
            if (this.GetComponent<SpriteRenderer>().flipX)
            {
                this.GetComponent<SpriteRenderer>().flipX = false;
            }
            else
            {
                this.GetComponent<SpriteRenderer>().flipX = true;
            }
        }
    }

    void FixedUpdate()
    {
        //anim.SetFloat("Speed",Mathf.Abs(movement.x * speed + movement.y * speed));
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);

        if(passages[0] == null) return;
    
        //DEL PASILLO 1 AL HUB
        if (transform.position.x >= passages[0].position.x && pasillo1)
        {
            lerping = true;
            bg = GameObject.FindGameObjectWithTag("bg2").transform.position;
            bg.z = -10;
            camara.transform.position = Vector3.Lerp(camara.transform.position, bg, 5 * Time.deltaTime);
            pasillo1 = false;
            hub = true;
            pasillo2 = false;
            pasillo3 = false;

            foreach(GameObject go in GameObject.FindGameObjectsWithTag("Hub")){
                go.GetComponent<guardBehaviour>().reset();
                go.GetComponent<guardBehaviour>().setReseteable();   
            }
        }

        //HUB A PASILLO 1
        if (transform.position.x <= passages[0].position.x && hub)
        {
            bg = GameObject.FindGameObjectWithTag("bg1").transform.position;
            bg.z = -10;
            camara.transform.position = Vector3.Lerp(camara.transform.position, bg, 5 * Time.deltaTime);
            pasillo1 = true;
            hub = false;
            pasillo2 = false;
            pasillo3 = false;

            foreach(GameObject go in GameObject.FindGameObjectsWithTag("1")){
                //print(go);
                go.GetComponent<guardBehaviour>().setReseteable();
            }
        }
        
        //HUB A PASILLO 2
        if (transform.position.x >= passages[1].position.x && hub)
        {
            bg = GameObject.FindGameObjectWithTag("bg3").transform.position;
            bg.z = -10;
            camara.transform.position = Vector3.Lerp(camara.transform.position, bg, 5 * Time.deltaTime);
            pasillo2 = true;
            hub = false;
            pasillo1 = false;
            pasillo3 = false;

            foreach(GameObject go in GameObject.FindGameObjectsWithTag("2")){
                go.GetComponent<guardBehaviour>().setReseteable();
            }
        }

        //PASILLO 2 A HUB
        if (transform.position.x <= passages[1].position.x && pasillo2)
        {
            bg = GameObject.FindGameObjectWithTag("bg2").transform.position;
            bg.z = -10;
            camara.transform.position = Vector3.Lerp(camara.transform.position, bg, 5 * Time.deltaTime);
            pasillo2 = false;
            hub = true;
            pasillo3 = false;
            pasillo1 = false;

            foreach(GameObject go in GameObject.FindGameObjectsWithTag("Hub")){
                go.GetComponent<guardBehaviour>().setReseteable();
            }
        }

        //HUB A PASILLO 3 transform.position.x == passages[2].position.x &
        if (transform.position.y <= passages[2].position.y && hub)
        {
            bg = GameObject.FindGameObjectWithTag("bg4").transform.position;
            bg.z = -10;
            camara.transform.position = Vector3.Lerp(camara.transform.position, bg, 5 * Time.deltaTime);
            pasillo3 = true;
            hub = false;
            pasillo2 = false;
            pasillo1 = false;

            foreach(GameObject go in GameObject.FindGameObjectsWithTag("3")){
                go.GetComponent<guardBehaviour>().setReseteable();
            }
        }

        //PASILLO 3 A HUB
        if (transform.position.y >= passages[2].position.y && pasillo3)
        {
            bg = GameObject.FindGameObjectWithTag("bg2").transform.position;
            bg.z = -10;
            camara.transform.position = Vector3.Lerp(camara.transform.position, bg, 5 * Time.deltaTime);
            pasillo3 = false;
            hub = true;

            foreach(GameObject go in GameObject.FindGameObjectsWithTag("Hub")){
                go.GetComponent<guardBehaviour>().setReseteable();
            }
        }
        // print(hub);

        if(lerping) camara.transform.position = Vector3.Lerp(camara.transform.position, bg, 5 * Time.deltaTime);
    }

    public void setSpeed(float s)
    {
        this.speed = s;
    }

    public void nextClass(){
        globales.nextClass++;
        //nClass++;
    }

    public int getNextClass() => globales.nextClass;
    public BoxCollider2D getArea() => area;
}
