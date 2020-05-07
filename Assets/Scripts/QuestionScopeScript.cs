using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestionScopeScript : MonoBehaviour
{
    private ButtonState state = ButtonState.NotAvailable;
    public Button button;
    public GameMap gameMap;

    public int xCordinat;
    public int yCordinat;
    public ButtonState State 
    { 
        get => state;
        set
        {
            if (value == ButtonState.TrueAnsvered)
            {
                if(button == null)
                    button = gameObject.GetComponent<Button>();

                button.onClick.RemoveAllListeners();
                gameObject.GetComponent<Graphic>().color = Color.blue;

            }
            if(value == ButtonState.FalseAnsvered)
            {
                if (button == null)
                    button = gameObject.GetComponent<Button>();

                button.onClick.RemoveAllListeners();
                gameObject.GetComponent<Graphic>().color = new Color(0.5188679f, 0.06118724f, 0.5188679f);

            }
            if (value == ButtonState.NotAvailable)
            {
                if (button == null)
                    button = gameObject.GetComponent<Button>();

                gameObject.GetComponent<Graphic>().color = Color.gray;

                button.onClick.RemoveAllListeners();
            }
            if (value == ButtonState.NotUsed)
            {
                if (button == null)
                    button = gameObject.GetComponent<Button>();

                gameObject.GetComponent<Graphic>().color = Color.green;

                button.onClick.RemoveAllListeners();
                button.onClick.AddListener(Press);
                Debug.Log("Function is add "+ xCordinat +':' +yCordinat);
            }

            state = value;
            Debug.Log(Convert.ToString(xCordinat) + ':' + Convert.ToString(yCordinat) + ' ' + Convert.ToString(value)+ '=' + Convert.ToString(state));
        } 
    }

    public void Press()
    {
        Debug.Log("Button pressed");
        if(!(xCordinat == 0 && yCordinat ==0))
            gameMap.ViewQuestion(this);
        else
        {
            gameMap.PressButton(xCordinat, yCordinat);
            State = ButtonState.TrueAnsvered;
        }
    }

    public void TrueAnsvered()
    {
        gameMap.PressButton(xCordinat, yCordinat);
        State = ButtonState.TrueAnsvered;
    }
    public void FalseAnsvered()
    {
        State = ButtonState.FalseAnsvered;
    }

    // Start is called before the first frame update
    void Start()
    {
        button = gameObject.GetComponent<Button>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

public enum ButtonState :byte
{
    TrueAnsvered,
    NotUsed,
    NotAvailable,
    FalseAnsvered
}