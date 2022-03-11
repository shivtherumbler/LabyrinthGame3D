using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardInput : InputModule
{
    Vector2 myInput;

    public KeyboardInput()
    {
        myInput = new Vector2();
    }

    public override Vector2 GetInput()
    {
        myInput.x = Input.GetAxis("Horizontal");
        myInput.y = Input.GetAxis("Vertical");
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
