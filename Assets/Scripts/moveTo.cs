using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveTo : MonoBehaviour
{

	public Vector2 posNew;
	public Vector3 scaleNew;
	public float mPos;
	public float mScale;

	Vector2 posOld;
	Vector3 scaleOld;

	Vector2 posDif;
	Vector3 scaleDif;

    // Start is called before the first frame update
    void Start()
    {
        posOld = GetComponent<RectTransform>().localPosition;
        scaleOld = GetComponent<RectTransform>().localScale;

        posDif = posNew - posOld;
        scaleDif = scaleNew - scaleOld;
    }

    // Update is called once per frame
    void Update()
    {
    	if (GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("moveTo")){
    		GetComponent<RectTransform>().localPosition = posOld + (posDif * mPos);
    		GetComponent<RectTransform>().localScale = scaleOld + (scaleDif * mScale);

    	}
        
    }
}
