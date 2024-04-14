using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class menuLevelManager : MonoBehaviour
{

	public GameObject boardPrefab;
	public GameObject palettePrefab;
	public GameObject pegPrefab;
	public GameObject swatchPrefab;
    public GameObject notchPrefab;

    public bool soundOn;
	public int maxRows;
	public int maxCols;
	//public float tileSize; //auto
    public float margins; //auto
    public float padding; //auto
	public List<lineData> lineData; //to be interpreted into lines

    // Start is called before the first frame update
    void Start()
    {
        //determine if sound is enabled or not
        //rather than feeding in a variable, just check to see if bkg music is playing
        //if (GameObject.FindGameObjectWithTag("soundManager").GetComponent<AudioSource>().isPlaying) soundOn = true;
        
        //initialize lines such that there are no duplicating swatches
    	List<line> lines = setLines(); //read the public line data and convert them to line objects
    	List<swatchData> dataList = linesToData(lines); //convert these lines objects into swatch data
        //--------------------------------------------
        //LAYOUT FORMATTING:

        Vector3 paletteLoc = new Vector3();
        Vector3 boardLoc = new Vector3();
        float scale = 1;
        float tileSize = 1;
        int rows = -1; int cols = -1;

        if (dataList.Count<5 || dataList.Count>50){
            Debug.Log("invalid total swatches");
        } else if (dataList.Count<18){ //small board
            Debug.Log("SMALL BOARD: count = " + dataList.Count);
            rows = 2;
            cols = Mathf.FloorToInt((dataList.Count+1)/2);
            paletteLoc = new Vector3(0, 2, 0);
            boardLoc = new Vector3(0, -1, 0);
        } else if (dataList.Count<=30){ //med board
            Debug.Log("MEDIUM BOARD: count = " + dataList.Count);
            rows = 3;
            cols = Mathf.FloorToInt((dataList.Count+2)/3);
            paletteLoc = new Vector3(0, 2.5f, 0);
            boardLoc = new Vector3(0, -2f, 0);
        } else if (dataList.Count >= 30){ //large board
            Debug.Log("LARGE BOARD: count = " + dataList.Count);
            rows = 4;
            cols = Mathf.FloorToInt((dataList.Count+3)/4);
            scale = .8f;
            paletteLoc = new Vector3(0, 2.5f, 0);
            boardLoc = new Vector3(0, -2f, 0);
        }

        Vector2 palDims = new Vector2(cols, rows);

        //----------------------------------------------
        //DEFINING OBJECTS:

        //*** tileSize refers to the PADDING size on each tile, it does not change 
        //the pixel size of the sprite itself.
        //*** changing the local scale scales the entire object down but MUST BE DONE BEFORE
        // PLACING THE SWATCHES

		//define palette
        GameObject paletteGO = Instantiate(palettePrefab, paletteLoc, Quaternion.identity);
        palette P = paletteGO.GetComponent<palette>();
        P.define((int)palDims[0], (int)palDims[1], tileSize, notchPrefab);
        P.init();

		//define board
        GameObject boardGO = Instantiate(boardPrefab, boardLoc, Quaternion.identity);
        board B = boardGO.GetComponent<board>();
        B.define(maxCols, maxRows, tileSize, pegPrefab, swatchPrefab, paletteGO, dataList);
        B.init();

        //set scale
        P.transform.localScale = new Vector3(scale, scale, 1);
        B.transform.localScale = new Vector3(scale, scale, 1);

        //animate first, then lock and initialize
        Debug.Log("****LEVEL START LISTENER****");
        //animStart(boardGO, paletteGO);

        //lock swatches and enable a lock symbol
        // B.transform.GetChild(0).GetComponent<movement>().enabled = false;
        // GameObject child = B.transform.GetChild(0).GetChild(0).gameObject;
        // child.SetActive(true);

        lockSwatch(B.transform.GetChild(0));
        lockSwatch(B.transform.GetChild(8));



        //place swatches in starting positon
        initSwatches(boardGO, paletteGO);

    }

    void lockSwatch(Transform T){
    	//GameObject GO = T.gameObject;
    	T.GetComponent<movement>().enabled = false;
    	T.GetChild(0).gameObject.SetActive(true);


    }

    // void animStart(GameObject B, GameObject P){
    // 	peg[] pegs = B.GetComponentsInChildren<peg>();
    // 	swatch[] swatches = B.GetComponentsInChildren<swatch>(); //movement holds animator commands
    // 	peg[] notches = P.GetComponentsInChildren<peg>();

    // 	//animate pegs and swatches
    // 	for (int i = 0; i<swatches.Length; i++){
    // 		//animate each peg
    // 		//animate each swatch
    // 		//swatches[i].anim = swatches[i].GetComponent<Animator>();
    // 		//swatches[i].anim.SetTrigger("init");

    // 	}
    // 	//animate notches
    // 	// for (int i = 0; i<y; i++){
    // 	// 	//animate each notch
    // 	// }
    // }


    void initSwatches(GameObject B, GameObject P){
        peg[] pegs = P.GetComponentsInChildren<peg>(); //pegs are used bc notches are just a dif sprite - they use the same script
        Debug.Log("pegs length: " + pegs.Length);
        //use filtered movement, not swatch, to skip locked swatches
        movement[] M = Array.FindAll(B.GetComponentsInChildren<movement>(), mvm =>mvm.enabled); 

        //Debug.Log("(init swatches) movement length: " + M.Length);
        Transform T = null;
        M = rndArray(M);
        for (int i = 0; i<M.Length; i++){
            T = M[i].GetComponent<Transform>();
            T.position = pegs[i].GetComponent<Transform>().position + Vector3.back;
        }
        Debug.Log(B.transform.childCount);
    }

    movement[] rndArray(movement[] S){

        for (int i = 0; i<S.Length; i++){
            movement temp = S[i];
            int rndIndex = UnityEngine.Random.Range(i, S.Length);
            S[i] = S[rndIndex];
            S[rndIndex] = temp;
        }

        return S;
    }

    Vector2 setPaletteDims(int count){
        int rows; int cols;

        if (count<5 || count > 50) {
            Debug.Log("invalid total swatches");
            return new Vector2(-1, -1);
        }
        //small board: 2 rows, max of 9 cols
        if (count>=5 && count< 18){
            Debug.Log("SMALL BOARD: count = " + count);
            rows = 2;
            cols = Mathf.FloorToInt((count+1)/2);
            return new Vector2(cols, rows);
        }
        //med board: 3 rows, 8-10 cols
        if (count>=18 && count<=30){
            Debug.Log("MED BOARD: count = " + count);
            rows = 3;
            cols = Mathf.FloorToInt((count+2)/3);
            return new Vector2(cols, rows);
        }
        //large board: 5 rows, 6-10 cols
        if (count>=31 && count<=50){
            Debug.Log("LARGE BOARD: count = " + count);
            rows = 4;
            cols = Mathf.FloorToInt((count+3)/4);
            return new Vector2(cols, rows);
        }
        return new Vector2(-1, -1);
        //small palette: 2 rows X, min cols (count: 5->18)
        //large palette: 3->5 rows, 8->10 columns (24->50)
    }

    List<line> setLines(){
    	List<line> lines = new List<line>();
    	for (int i = 0; i<lineData.Count; i++){
    		lines.Add(new line(lineData[i].start, lineData[i].end, lineData[i].A, lineData[i].B));
    	}

    	for (int i = 0; i<lines.Count; i++){
    		lines[i].init();
    	}
    	return lines;
    }

    List<swatchData> linesToData(List<line> lines){
    	List<swatchData> dataList = new List<swatchData>();

    	for (int i = 0; i<lines.Count; i++){ //for all lines (line[i])
    		for (int j = 0; j<lines[i].data.Count; j++){ //for all data points per line (data[j])
    			//Debug.Log(lines[i].data[j].loc + ", " + lines[i].data[j].col);
    			if(dataList.Find(x => x.loc==lines[i].data[j].loc)==null){
    				dataList.Add(lines[i].data[j]);
    			} else Debug.Log("skipping");
    		}
    	}
    	return dataList;
    }
}
