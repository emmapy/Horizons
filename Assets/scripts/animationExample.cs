using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animationExample : MonoBehaviour
{
    Animator anim;
    bool selected;
 
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        selected = false;
    }
    void Update()
    {

        if (Input.GetMouseButtonDown(0) && !selected)
        {
        	Debug.Log("clicked");
            //anim.SetTrigger("selected");
            anim.SetBool("selected", true);
            selected = true;
        } else if (Input.GetMouseButtonDown(0) && selected)
        {
        	Debug.Log("clicked");
            anim.SetBool("selected", false);
            selected = true;
        }

    }
}
