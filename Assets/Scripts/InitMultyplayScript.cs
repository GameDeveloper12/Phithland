using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InitMultyplayScript : MonoBehaviour
{
    public InputField input;
    public Text text;
    public CoreMultyplayScript core;
    public ClientScript clientCore;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    
    void Awake()
    {

    }
    public void PressButtonHost()
    {
        Debug.Log("test");
        if (input.text != "")
        {
            gameObject.SetActive(false);
            core.gameObject.SetActive(true);
            core.username = input.text;
            core.textUsername.text = "1. " + input.text;
        }
        else
            text.gameObject.SetActive(true);
    }

    public void PressButtonClient()
    {
        Debug.Log("test");
        if (input.text != "")
        {
            gameObject.SetActive(false);
            clientCore.gameObject.SetActive(true);
        }
        else
            text.gameObject.SetActive(true);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
