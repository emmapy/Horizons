using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class colorMng : MonoBehaviour
{
	public int rows, cols;
	public Vector2[] starts;
	public Vector2[] ends;
	public List<Vector2> intersections;

    // Start is called before the first frame update
    void Start()
    {

       init(); 
    }

    void init(){
    	//check if starts length = ends length
    	//find the intersections

    	if (starts.Length != ends.Length){
    		Debug.Log("color Mng: incorrect beginning values");
    		Debug.Break();
    	}

    	for (int i = 0; i<starts.Length; i++){
    		for (int j = i+1; j<starts.Length; j++){
    			//compare starts[i] and ends[i] to all other pairs to check for matching j coordinates
    			//(y-y1) = m(x-x1) for both sets i and j, if m1 and m2 and 0 and inf, there is a match
    			if (intersects(starts[i], ends[i], starts[j], ends[j])){
    				starts[i] = reorder(starts[i], ends[i])[0];
    				ends[i] = reorder(starts[i], ends[i])[1];
    				starts[j] = reorder(starts[j], ends[j])[0];
    				ends[j] = reorder(starts[j], ends[j])[1];


    				float length = getLength(starts[i], ends[i]);
    				intersections.Add(getIntersection(starts[i], ends[i], starts[j], ends[j]));
    				
    			}

    		}
    	}
    	Debug.Log("total intersections: " + intersections.Count);
    }

    Vector2[] reorder(Vector2 A, Vector2 B){
    	Vector2[] inorder = {new Vector2(), new Vector2()};
    	if ((A[0] == B[0]) && (A[1]==B[1])) {
    		Debug.Log("same values"); 
    		inorder[0] = A; inorder[1] = B;
    	}
    	if ((A[0] <= B[0]) && (A[1]<=B[1])) {
    		inorder[0] = A; inorder[1] = B;
    	} else {
    		inorder[0] = B; inorder[1] = A;
    	}
    	return inorder;
    }

    Vector2 getIntersection(Vector2 start0, Vector2 end0, Vector2 start1, Vector2 end1){
    	if (mVal(start0, end0) == 0f && mVal(start1, end1) == 1000f){
    		return new Vector2(start0[0], start1[1]);
    	} else { 
    		return new Vector2(start1[0], start0[1]);
    	}
    }

    bool intersects(Vector2 start0, Vector2 end0, Vector2 start1, Vector2 end1){
    	if (mVal(start0, end0) == mVal(start1, end1)) return false;
    	else if ((mVal(start0, end0) == 0f && mVal(start1, end1) == 1000f) ||
    			(mVal(start0, end0) == 1000f && mVal(start1, end1) == 0)) return true;
    	return false;
    }

    float mVal(Vector2 start, Vector2 end){
    	if (start[0] == end[0]) return 0f;
    	else if (start[1] == end[1]) return 1000f;
    	else return 2;

    }

    int getIndex(Vector2 A, Vector2 B, Vector2 C){ //assuming a is index 0 and C is length AND IN CORRECT ORDER
    	float length = getLength(A, C);
    	if (mVal(A, C) == 0){
    		return (int)(B[0]-A[0]);
    	} else { //m is inf
    		return (int)(B[1]-A[1]);
    	}
    }

    float getLength(Vector2 A, Vector2 B){ //ASSUMING A AND B ARE IN THE CORRECT ORDER
    	float length = -1;
    	if (mVal(A, B) == 0){ //line is horizontal
    		length = (B[0]-A[0] + 1);
    	} else{ //line is vertical
    		length = ((B[1]-A[1] + 1));
    	}
    	return length;
    }

    float getRndM(int length){
    	float mMax = 255/length;
    	float mMin = (-255)/length;
    	float range = 255*2;
    	float min = -255;
    	float rndM = range*Random.value + min;
    	return rndM;
    }

}
