using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class checkManager : MonoBehaviour
{
	Toggle toggle;
    bool win;

    // Start is called before the first frame update
    void Start()
    {
		//toggle = GameObject.Find("solToggle").GetComponent<Toggle>(); //UNCOMMENT TO USE FOR DEBUG
    }

    // Update is called once per frame
    void Update()
    {
    	//if space pressed
    	//invert

//--------------- UNCOMMENT TO USE FOR DEBUG AND UNCOMMENT COLOR UPDATES IN FUNCTIONS
		// if (Input.GetKeyUp(KeyCode.Space)) {
		// 	invert();
		// 	if (toggle.isOn) check();
  //           else reset();
		// }
		// if(Input.GetMouseButtonDown(0)){
		// 	if (toggle.isOn){
		// 		check();
		// 	}
		// 	if (!toggle.isOn){
		// 		reset();
		// 	}  
		// }

  //       if (win && !animating()){
  //           //Debug.Break();
  //           runWinAnim();
  //           win = false;
  //       }
        //----------------------------------

        //------------BELOW IS FOR NO DEBUG:
        if (Input.GetMouseButtonDown(0)){
            check();
        }

        if (win && !animating()){
            runWinAnim();
            runWinUI();
            win = false;
        }



    }

    void invert(){ //BUG:::: TOGGLE HAS ISSUES WITH COMBINED CLICK/SPACE CHANGES
    	toggle.isOn = !toggle.isOn;
    }

    void check(){
        bool correctSol = true;

    	swatch[] swatches = FindObjectsOfType<swatch>();
    	for (int i = 0; i<swatches.Length; i++){
    		if (swatches[i].transform.position == swatches[i].solution.transform.position + Vector3.back){
    			//swatches[i].solution.sprite.color = Color.green;
    		} else {
                correctSol = false;
                //swatches[i].solution.sprite.color = Color.white;
            }
    	}

        if (correctSol) {
            Debug.Log("****CORRECTSOL LISTENER****");
            win = true;
            updateSave();
        }
    }

    void updateSave(){
        player p = FindObjectOfType<player>();

        sceneManager s = FindObjectOfType<sceneManager>();
        p.completed[s.levelNumber] = true;
        p.unlocked[s.levelNumber+1] = true;
        if (p!=null) p.save();
    }

    // public void revealHint(peg p){
    //     swatch[] swatches = FindObjectsOfType<swatch>();
    //     for (int i = 0; i<swatches.Length; i++){
    //         if (swatches[i].solution == p){
    //             swatches[i].transform.position = p.transform.position + Vector3.back;

    //             // swatches[i].transform.GetChild(0).gameObject.SetActive(true);
    //             // swatches[i].GetComponent<movement>().enabled = false;
    //             sceneManager s = FindObjectOfType<sceneManager>();
    //             s.lockSwatch(swatches[i].transform);
    //             s.lockedSwatches.Add(swatches[i].loc);
    //         }
    //     }

    // }

    public void revealHint(peg p){

        swatch sol = getSolAt(p);
        Debug.Log("sol: " + sol);
        swatch swap = getSwatchOn(p);
        Debug.Log("swap: " + swap);

        if (swap != null){
            //swap them
            Vector3 temp = sol.transform.position;
            sol.transform.position = swap.transform.position;
            swap.transform.position = temp;
        } else {
            sol.transform.position = p.transform.position + Vector3.back;
        }

        sceneManager s = FindObjectOfType<sceneManager>();
        s.lockSwatch(sol.transform);
        s.lockedSwatches.Add(sol.loc);


    }

    swatch getSolAt(peg p){
       swatch[] swatches = FindObjectsOfType<swatch>();
        for (int i = 0; i<swatches.Length; i++){
            if (swatches[i].solution == p){
                return swatches[i];
            }
        } 
        return null;
    }

    swatch getSwatchOn(peg p){
        swatch[] swatches = FindObjectsOfType<swatch>();
        for (int i = 0; i<swatches.Length; i++){
            if (swatches[i].transform.position == p.transform.position + Vector3.back){
                return swatches[i];
            }
        }
        return null;
    }

    public peg getPegUnder(swatch s){
        peg[] pegs = FindObjectsOfType<peg>();
        for (int i = 0; i<pegs.Length; i++){
            if (s.transform.position == pegs[i].transform.position + Vector3.back){
                return pegs[i];
            }
        }
        return null;
    }
    void runWinAnim(){
        Debug.Log("run win anim");
        swatch[] swatches = FindObjectsOfType<swatch>();

        for (int i = 0; i<swatches.Length; i++){
            //swatches[i].GetComponent<Animator>().SetTrigger("correctSol");
            swatches[i].GetComponent<Animator>().Play("win", 0, 0f);
            //play win anim on level animator
            GameObject.FindWithTag("sceneManager").GetComponent<Animator>().SetBool("correctSol", true);
        }
    }

    void runWinUI(){
        Debug.Log("run win ui");
        Transform ui = FindObjectOfType<HorizontalLayoutGroup>().transform;
        //reset button
        ui.GetChild(0).GetComponent<Button>().interactable = false;
        //hint button
        ui.GetChild(1).GetComponent<Button>().interactable = false;
        int count = ui.GetChild(1).childCount;
        for (int i = 0; i<count; i++){
            ui.GetChild(1).transform.GetChild(i).gameObject.SetActive(false);
        }
        //home button
        ui.GetChild(2).GetComponent<Animator>().SetBool("blink", true);

    }

    void reset(){
    	swatch[] swatches = FindObjectsOfType<swatch>();
    	for (int i = 0; i<swatches.Length; i++){
			//swatches[i].solution.sprite.color = Color.white;
    	}
    }

    bool animating(){
        bool anim = false;
        swatch[] swatches = FindObjectsOfType<swatch>();
        for (int i = 0; i<swatches.Length; i++){
            //Debug.Log("this swatch is animating? " + swatches[i].animating);
            if (swatches[i].animating) anim = true;
        }
        return anim;
    }
}
