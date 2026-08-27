#nullable disable

using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class introWallPlastTime : MonoBehaviour
{
    public Animator anim;
    void Start()
    {
        StartCoroutine(toMenu());
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) { StartCoroutine(skipToMenu()); }    
    }
    IEnumerator toMenu() { yield return new WaitForSeconds(13.5f); StartCoroutine(skipToMenu()); }
    IEnumerator skipToMenu() { anim.SetBool("skipped", true); yield return new WaitForSeconds(.5f); SceneManager.LoadScene("testMenu"); }
}
