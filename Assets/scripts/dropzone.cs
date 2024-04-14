using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dropzone : MonoBehaviour
{

    Animator anim;
    public Vector3 initScale;
    public float scaleChange;

    // Start is called before the first frame update
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("initDropzone") ) {
            gameObject.transform.localScale = new Vector3(initScale[0]*scaleChange, initScale[1]*scaleChange, 1);
        } 
    }

    void OnMouseDown(){
        hintMng h = FindObjectOfType<hintMng>();

        if (h.isOn){ 
            checkManager c = FindObjectOfType<checkManager>();
            peg p = GetComponent<peg>();
            c.revealHint(p);
            h.flip();
            h.decrement();
            //h.transform.GetChild(4).gameObject.SetActive(false);
            //GameObject.FindWithTag("message").SetActive(false);
            return;
        }
    	movement active = getActive();
    	if (active != null) {
            active.moveHere(gameObject.transform.position + Vector3.back);
            active.flipActive();
        }
    }

    movement getActive(){
        movement active = null;
        GameObject tagged = GameObject.FindWithTag("active");
        if (tagged == null) return active;
        active = tagged.GetComponent<movement>();
        return active;
    }
}
