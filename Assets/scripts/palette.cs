using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class palette : MonoBehaviour
{
    //constructors
	private int maxRows;
	private int maxCols;
	private float tileSize;
	private GameObject notchPrefab;

	public List<Vector2>locs;

    // Start is called before the first frame update
    void Start()
    {

    }

    public void define(int rows, int cols, float tsize, GameObject npf){
        maxRows = rows;
        maxCols = cols;
        tileSize = tsize;
        notchPrefab = npf;

    }

    public void init(){
    	setLocs();
    	genGrid();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void printLocs(){
    	Debug.Log("PRINTING");
    	for (int i = 0; i<locs.Count; i++){
    		Debug.Log(locs[i]);
    	}
    }

    void setLocs(){
    	locs = new List<Vector2>();
    	for (int j = 0; j<maxCols; j++){
    		for (int i = 0; i<maxRows; i++){
    			locs.Add(new Vector2(i, j));
    		}
    	}
    }

    // public void shuffleLocs(int start, int end){
    // 	for (int i = start; i<end; i++){
    // 		Vector2 temp = locs[i];
    // 		int randomIndex = Random.Range(i, end);
    // 		locs[i] = locs[randomIndex];
    // 		locs[randomIndex] = temp;
    // 	}
    // }

    void genGrid(){

    	//pegList = new List<peg>();

	    for (int i = 0; i<maxCols; i++){ //rows
	        for (int j = 0; j<maxRows; j++){ //cols
	           	GameObject item = Instantiate(notchPrefab);
	           	item.transform.parent = gameObject.transform;
                //item.GetComponent<dropzone>().scaleChange = 1.2f;
                item.GetComponent<dropzone>().initScale = item.transform.localScale;
	           	placeAt(item, new Vector2(j, i));

	        }
	    }
    }

    public void placeAt(GameObject item, Vector2 loc){
    	int rows = maxCols;
		int cols = maxRows;

	    float gridWidth = cols*tileSize;
	    float gridHeight = rows*tileSize;
	    float gridX = gameObject.transform.position.x;
	    float gridY = gameObject.transform.position.y;

	    int i = (int)loc[0]; int j = (int)loc[1];
        float posX = (i*tileSize)-(gridWidth/2)+tileSize/2;
        float posY = (j * -tileSize)+(gridHeight/2)-tileSize/2;
        item.transform.position = new Vector3(posX+gridX, posY+gridY, 0);

    }
}
