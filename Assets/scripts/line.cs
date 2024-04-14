 	using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class line : MonoBehaviour
{

	public List<swatchData> data;
	//public List<swatchData> seed;
	public Vector2 start;
	public Vector2 end;
	public Color A;
	public Color B;

	private float length;
	private bool illegal;

	public line(Vector2 s, Vector2 e, Color c1, Color c2){
		start = s;
		end = e;
		A = c1;
		B = c2;
	}

	public void init(){
		sort();
		setLen();
		fillData();

		//fillLocs();
		//illegal
		//fillColors();
	}

	void fillData(){
		List<Vector2> locs = getLocs();
		List<Color> colors = getColors();
		data = new List<swatchData>();

		for (float i = 0; i<length; i++){
			data.Add(new swatchData(locs[(int)i], colors[(int)i])); 
		}
	}

	List<Color> getColors(){
		List<Color> cols = new List<Color>();
		for (float i = 0; i<length; i++){
			cols.Add(lerpAt(0, length, A, B, i));
		}
		return cols;
	}

	//here and there are indeces of the line, a and b are their respective colors
	Color lerpAt(float here, float there, Color a, Color b, float i){ //i is the counter, relative to the start of lerp
		float seed = (1/(there-1));
		Color c = Color.Lerp(a, b, i*seed);
		return c;
	}

	// void lerp(int start, int end){
	// 	Debug.Log("filling colors (start, end): " + start + ", " + end);
	// 	colors = new List<Color>();

	// 	float seed = 1/length;
	// 	for (float i = start; i<=end; i++){
	// 		colors.Add(Color.Lerp(A, B, i*seed));
	// 	}
	// }

    List<Vector2> getLocs(){
    	List<Vector2> list = new List<Vector2>();

    	//priority left->right, top->bottom
    	if (start[1] == end[1]){
    		for (float i = start[0]; i<=end[0]; i++){
    			//Debug.Log("horizontal");
    			//new location is: new Vector2(i, start[1]);
    			list.Add(new Vector2(i, start[1]));
    		}
    	}
    	if (start[0] == end[0]){
    		for (float i = start[1]; i<=end[1]; i++){
    			//Debug.Log("vertical");
    			//new location is: new Vector2(start[0], i);
    			list.Add(new Vector2(start[0], i));
    		}
    	}
    	return list;
    }


    void setLen(){
    	float rise = end[1]-start[1];
    	float run = end[0] - start[0];

    	if (rise !=0 && run != 0){
    		length = -1;
    	} else if (rise == 0) length = run+1;
    	else if (run == 0) length = rise+1;
    	//Debug.Log("LENGTH = " + length);
    }

    void sort(){
    	if (end[0]<start[0]) swap();
    	else if (end[1]<start[1]) swap();
    }	

    void swap(){
    	Vector2 temp = start;
    	start = end;
    	end = temp;
    }

}

[System.Serializable]
public class lineData{
	public Vector2 start;
	public Vector2 end;
	public Color A;
	public Color B;	
	public lineData(){

	}
}
    public class swatchData{
    	public Vector2 loc;
    	public Color col;
    	public swatchData(Vector2 l, Color c){
    		loc = l;
    		col = c;
    	}

    	public void print(){
    		Debug.Log("location: " + loc + ", color: " + col);
    	}
    }