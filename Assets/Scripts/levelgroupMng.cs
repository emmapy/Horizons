using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class levelgroupMng : MonoBehaviour
{
	public Color start;
	public Color end;
    // Start is called before the first frame update
    void Start()
    {
        //init();
    }

    public void init(){
    	//get number of children
    	//Debug.Log(gameObject.transform.childCount);

    	//determine row x col layout
    	//define TR, TL, BR, BL seed colors
    	//lerp and assign colors
    	assignColors();

        //read save data
        readData();



    }

    void readData(){
        player p = FindObjectOfType<player>();

        for (int i = 0; i<p.totalLevels; i++){
            GameObject child = transform.GetChild(i).transform.GetChild(0).gameObject;
            child.SetActive(p.unlocked[i]);
            child.transform.GetChild(0).gameObject.SetActive(p.completed[i]);
            if (child.activeInHierarchy && !child.transform.GetChild(0).gameObject.activeInHierarchy){ //a new level
                //Debug.Log(child.GetComponent<Animator>());
                child.GetComponent<Animator>().SetTrigger("appear");
                child.GetComponent<Animator>().SetBool("blink", true);
            } else if (child.activeInHierarchy){
                child.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
            }
        }
    }

    void assignColors(){
    	Transform child;
        Debug.Log("child count: " + transform.childCount);
    	for (int i = 0; i<transform.childCount; i++){
    		child = transform.GetChild(i).transform.GetChild(0);
    		child.GetComponent<Image>().color = getColor(i);
    	}
    }

    Color getColor(float index){
    	float length = transform.childCount;
    	float seed = (1/(length-1));
    	Color c = Color.Lerp(start, end, index*seed);
    	return c;

    }
}
