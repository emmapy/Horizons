using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class board : MonoBehaviour
{
	private int maxRows;
	private int maxCols;
	private float tileSize;

	private GameObject pegPrefab;
	private GameObject swatchPrefab;
	private GameObject palette;
	//public List<lineData> lineData;
    public List<swatchData> data;

	public void define(int cols, int rows, float tsize, GameObject ppf, GameObject spf, GameObject plt, List<swatchData> sd){
		maxCols = cols;
		maxRows = rows;
		tileSize = tsize;
		pegPrefab = ppf;
		swatchPrefab = spf;
		palette = plt;
		data = sd;
	}

    public void init(){
        genBoard();
    }

    // Start is called before the first frame update
    void Start()
    {
    	//palette.GetComponent<palette>().init();
        //genBoard();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void genBoard(){
        //Debug.Log("genboard");

    	//List<line> lines = setLines();

    	//List<swatchData> data = linesToData(lines);

    	palette P = palette.GetComponent<palette>();
    	// P.shuffleLocs(0, data.Count);
    	//myPalette.init();

    	for (int i = 0; i<data.Count; i++){

    	   	GameObject newSwatch = Instantiate(swatchPrefab);
    	   	GameObject newPeg = Instantiate(pegPrefab);
    	   	newSwatch.transform.parent = gameObject.transform;
    	   	newPeg.transform.parent = gameObject.transform;

    	   	swatch s = newSwatch.GetComponent<swatch>();
    	   	movement m = newSwatch.GetComponent<movement>();
    	   	peg p = newPeg.GetComponent<peg>();
            dropzone d = newPeg.GetComponent<dropzone>();

    	   	s.init(data[i].loc);
    	   	s.solution = p;
    	   	s.sprite.color = data[i].col;


            //m.scaleChange = 2f;
    	   	//s.scaleChange = 1.2f;
            s.initScale = m.transform.localScale;
            //d.scaleChange = 1.2f;
            d.initScale = d.transform.localScale;

    	   	placeAt(newPeg, data[i].loc);
    	   	placeAt(newSwatch, data[i].loc); //place on board
    	   	//P.placeAt(newSwatch, P.locs[i]); //place on palette

    	   	newSwatch.transform.position += Vector3.back;
    	}
    }

    void placeAt(GameObject item, Vector2 loc){
    	int cols = maxCols;
		int rows = maxRows;

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
