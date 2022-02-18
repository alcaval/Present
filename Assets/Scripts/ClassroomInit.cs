using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ClassroomInit : MonoBehaviour
{

    [SerializeField] Dialogue classroomDialogue;
    // Start is called before the first frame update
    void Start()
    {
        globales.clasesAsistidas++;
        GameObject.FindGameObjectWithTag("DialogueManager").GetComponent<DialogueManager>().StartDialogue(GetComponent<DialogueTrigger>(), classroomDialogue);
    }
}
