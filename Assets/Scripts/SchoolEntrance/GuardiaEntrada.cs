using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GuardiaEntrada : MonoBehaviour
{

    [SerializeField] private Animator transition;

    public void CambiarMetalGear()
    {
        StartCoroutine(LoadNextScene());
    }

    IEnumerator LoadNextScene()
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene("MetalGearHall");
    }
}
