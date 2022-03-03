using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GuardiaEntrada : MonoBehaviour
{

    [SerializeField] private Animator transition;

    [SerializeField] private PlayerDialogueInteraction interaction;

    private void Start() 
    {
        interaction.enabled = true;
    }

    public void CambiarMetalGear()
    {
        interaction.enabled = false;
        StartCoroutine(LoadNextScene());
    }

    IEnumerator LoadNextScene()
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene("MetalGearHall");
    }
}
