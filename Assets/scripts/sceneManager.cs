using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class sceneManager : MonoBehaviour
{
    public int levelNumber;

	public GameObject boardPrefab;
	public GameObject palettePrefab;
	public GameObject pegPrefab;
	public GameObject swatchPrefab;
    public GameObject notchPrefab;

    public bool soundOn;
	public int maxRows;
	public int maxCols;
	//public float tileSize; //auto
    //public float margins; //auto
    //public float padding; //auto
	public List<lineData> lineData; //to be initialized into line objects
	public List<Vector2> lockedSwatches;

    public GameObject paletteGO;
    public GameObject boardGO;

    // Start is called before the first frame update
    void Start()
    {
        //SOUND:
        //determine if sound is enabled or not
        //rather than feeding in a variable, just check to see if bkg music is playing
        try{if (GameObject.FindGameObjectWithTag("soundManager").GetComponent<AudioSource>().isPlaying) soundOn = true;} catch {}
        
        //READ LINE DEFINITIONS:
        //initialize lines such that there are no duplicating swatches
    	List<line> lines = setLines(); //read lineData and convert them to line objects
    	List<swatchData> dataList = linesToData(lines); //convert these lines objects into swatch data
        
        //LAYOUT FORMATTING:
        //format scale/size of board depending on the dimensions of the puzzle
        Vector3 paletteLoc = new Vector3();
        Vector3 boardLoc = new Vector3();
        float scale = 1;
        float tileSize = 1;
        int rows = -1; int cols = -1;

        if (dataList.Count<5 || dataList.Count>50){
            Debug.Log("invalid total swatches");
        } else if (maxRows<=5 || maxCols<=4){ //small board
            Debug.Log("SMALL BOARD: count = " + dataList.Count);
            rows = 2;
            cols = Mathf.FloorToInt((dataList.Count+1)/2);
            paletteLoc = new Vector3(0, 3f, 0);
            boardLoc = new Vector3(0, -1f, 0);
        } else if (maxCols<=7 && maxRows<=9){ //med board
            Debug.Log("MEDIUM BOARD: count = " + dataList.Count);
            rows = 4;
            cols = Mathf.FloorToInt((dataList.Count+3)/4);
            scale = .6f;
            paletteLoc = new Vector3(0, 3.5f, 0);
            boardLoc = new Vector3(0, -1f, 0);
        } else if (maxRows<=9 && maxCols<=9 ){ //large board
            Debug.Log("LARGE BOARD: count = " + dataList.Count);
            rows = 4;
            cols = Mathf.FloorToInt((dataList.Count+3)/4);
            scale = .4f;
            paletteLoc = new Vector3(0, 3.5f, 0);
            boardLoc = new Vector3(0, -1f, 0);
        }

        Vector2 palDims = new Vector2(cols, rows);

        //DEFINING OBJECTS:

        //*** tileSize refers to the PADDING size on each tile, it does not change 
        //the pixel size of the sprite itself.
        //*** changing the local scale scales the entire object down but MUST BE DONE BEFORE
        // PLACING THE SWATCHES

		//define palette
        paletteGO = Instantiate(palettePrefab, paletteLoc, Quaternion.identity);
        palette P = paletteGO.GetComponent<palette>();
        P.define((int)palDims[0], (int)palDims[1], tileSize, notchPrefab);
        P.init();

		//define board
        boardGO = Instantiate(boardPrefab, boardLoc, Quaternion.identity);
        board B = boardGO.GetComponent<board>();
        B.define(maxCols, maxRows, tileSize, pegPrefab, swatchPrefab, paletteGO, dataList);
        B.init();

        //set scale
        P.transform.localScale = new Vector3(scale, scale, 1);
        B.transform.localScale = new Vector3(scale, scale, 1);

        Debug.Log("****LEVEL START LISTENER****");

        //read through list of locked locations and call lockswatch
       	List<swatch> L = B.GetComponentsInChildren<swatch>().ToList();
        for (int i = 0; i<lockedSwatches.Count; i++){
        	swatch s = L.Find(x=> x.loc == lockedSwatches[i]);
        	lockSwatch(s.transform);
        }

        //place swatches randomly in starting positon on the palette
        initSwatches();        

    }

    public void lockSwatch(Transform T){
    	T.GetComponent<movement>().enabled = false;
    	T.GetChild(0).gameObject.SetActive(true);
    }

    public void initSwatches(){ //create swatch objects and place on palette
        GameObject B = boardGO;
        GameObject P = paletteGO;
        peg[] pegs = P.GetComponentsInChildren<peg>(); //pegs are used bc notches are just a dif sprite - they use the same script
        Debug.Log("pegs length: " + pegs.Length);
        //use filtered movement, not swatch, to skip locked swatches
        movement[] M = Array.FindAll(B.GetComponentsInChildren<movement>(), mvm =>mvm.enabled); 

        //Debug.Log("(init swatches) movement length: " + M.Length);
        Transform T = null;
        M = rndArray(M); //randomize the array of moveable swatches
        for (int i = 0; i<M.Length; i++){ //place on palette
            T = M[i].GetComponent<Transform>();
            T.position = pegs[i].GetComponent<Transform>().position + Vector3.back;
        }
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

    List<line> setLines(){ //read lineData and convert it into line objects
    	List<line> lines = new List<line>();
    	for (int i = 0; i<lineData.Count; i++){
    		lines.Add(new line(lineData[i].start, lineData[i].end, lineData[i].A, lineData[i].B));
    	}

    	for (int i = 0; i<lines.Count; i++){
    		lines[i].init();
    	}
    	return lines;
    }

    List<swatchData> linesToData(List<line> lines){ //read lines and convert into swatch data
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
