using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Choice
{
    public Sprite icon;
    public string name;

    [TextArea(1,10)]
    public string sentence;
    public string[] options;
}

/*
? Por qué hereda de Intervention?
Básicamente, por poder hacer Queues de Intervention y meter Choices ahí en mitad.
Es raro, porque heredas los miembros de Intervention pero no lo va a usar (creo).

*/
