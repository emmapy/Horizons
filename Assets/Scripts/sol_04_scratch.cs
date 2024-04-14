using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sol_04_scratch : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        swatch s = (swatch)FindObjectOfType(typeof(swatch));
        s.init(new Vector2(1f, 1f));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
