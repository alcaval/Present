using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSystem : MonoBehaviour
{

    [SerializeField] GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player.transform.position = globales.spawnPos;
    }
}
