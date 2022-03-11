using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobileInput : InputModule
{
    Vector2 myInput;
 
    public MobileInput()
    {
        myInput = new Vector2();
    }

    public override Vector2 GetInput()
    {
        myInput.x = Input.acceleration.x;
        myInput.y = Input.acceleration.y;
        return myInput.normalized;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
