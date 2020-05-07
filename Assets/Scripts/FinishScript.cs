using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinishScript : MonoBehaviour 
{
    // Start is called before the first frame update
    public Text pointScope;

    private int points;

    public int Points 
    { 
        get => points; 
        set 
        { 
            points = value;
            pointScope.text = Convert.ToString(value);
        }
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
