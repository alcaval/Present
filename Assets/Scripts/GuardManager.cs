using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardManager : MonoBehaviour
{
    [SerializeField] GameObject[] guards;

    public GameObject[] getAllGuards() => guards;
}
