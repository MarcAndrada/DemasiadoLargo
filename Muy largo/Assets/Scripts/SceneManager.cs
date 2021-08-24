using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
 

    public static void FadeIn (GameObject _blackScreen){

        Animator animator = _blackScreen.GetComponent<Animator>();

        animator.SetTrigger("FadeIn");
    }

    public static void FadeOut(GameObject _blackScreen)
    {

        Animator animator = _blackScreen.GetComponent<Animator>();

        animator.SetTrigger("FadeOut");
    }
}
