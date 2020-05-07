using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMap : MonoBehaviour
{
    public Button prefabButton;
    public RectTransform mapContent;
    public Canvas canvasQuestions;
    public byte xCountButtons;
    public byte yCountButtons;
    private GameObject[,] mapButtons;

    private float curentTime;
    public float gameTime = 300;
    public Text time;

    public Text pointsScope;
    private int points = 0;

    public Canvas finishCanvas;
    private bool finish = false;

    public QuestionScopeScript curentScope;

    public void CheckAvalibleMap()
    {
        int countAvaleble = 0;
        foreach(var but in mapButtons)
            if (but.GetComponent<QuestionScopeScript>().State == ButtonState.NotUsed)
                ++countAvaleble;
        if (countAvaleble == 0)
            Finish();
    }

    public void Refrash()
    {
        if (finish)
        {
            foreach (var but in mapButtons)
                but.GetComponent<QuestionScopeScript>().State = ButtonState.NotAvailable;

            curentTime = gameTime;
            mapButtons[0, 0].GetComponent<QuestionScopeScript>().Press();
        }
        finish = false;
        points = 0;
        pointsScope.text = "0";
    }
    void Start()
    {
        mapButtons = new GameObject[xCountButtons, yCountButtons];

        var grid = mapContent.GetComponent<GridLayoutGroup>();
        var paddingX = grid.padding.left + grid.padding.right;
        var paddingY = grid.padding.top + grid.padding.bottom;
        var widthItems = grid.cellSize.x * xCountButtons + grid.spacing.x * (xCountButtons - 1);
        var heightItems = grid.cellSize.y * yCountButtons + grid.spacing.y * (yCountButtons - 1);

        mapContent.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, widthItems + paddingX);
        mapContent.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, heightItems + paddingY);

        for (int i = 0; i < xCountButtons* yCountButtons; ++i)
        {
            int x = i % xCountButtons, y = i / yCountButtons;
            GameObject inst = Instantiate(prefabButton.gameObject);
            mapButtons[x, y] = inst;
            var squer = inst.AddComponent<QuestionScopeScript>();
            squer.xCordinat = x;
            squer.yCordinat = y;
            squer.gameMap = this;

            inst.transform.SetParent(mapContent, false);
            //var but = inst.GetComponent<Button>();
            //but.onClick.AddListener(ViewQuestion);
        }

        curentTime = gameTime;
        mapButtons[0, 0].GetComponent<QuestionScopeScript>().Press();
    }

    public void ViewQuestion(QuestionScopeScript scope)
    {
        curentScope = scope;
        canvasQuestions.gameObject.SetActive(true);
        var questionScript = canvasQuestions.GetComponent<QuestionScript>();
        questionScript.gameMap = this;
        questionScript.NextQuestion();
    }

    public void AddPoint()
    {
        points += 1;
        pointsScope.text = Convert.ToString(points);
    }

    // Update is called once per frame
    void Update()
    {
        if (curentTime > 0 && !finish)
        {
            
            string Stime = Convert.ToString(Math.Ceiling(curentTime / 60)-1) + ':' + Convert.ToString(Math.Ceiling(curentTime % 60)-1);
            time.text = Stime;
            curentTime -= Time.deltaTime;
            if (curentTime <= 0)
            {
                Finish();
            }
        }
    }

    public void PressButton(int x, int y)
    {
        QuestionScopeScript bufButScript;
        //mapButtons[x, y].GetComponent<QuestionScopeScript>().Press();
        Debug.Log("Button pressed");

        if (x - 1 >= 0)
        {
            bufButScript = mapButtons[x - 1, y].GetComponent<QuestionScopeScript>();
            if (bufButScript.State == ButtonState.NotAvailable)
                bufButScript.State = ButtonState.NotUsed;
        }
        if (x + 1 <xCountButtons)
        {
            bufButScript = mapButtons[x + 1, y].GetComponent<QuestionScopeScript>();
            if (bufButScript.State == ButtonState.NotAvailable)
                bufButScript.State = ButtonState.NotUsed;
        }
        if (y - 1 >= 0)
        {
            bufButScript = mapButtons[x, y - 1].GetComponent<QuestionScopeScript>();
            if (bufButScript.State == ButtonState.NotAvailable)
                bufButScript.State = ButtonState.NotUsed;
        }
        if (y + 1 < yCountButtons)
        {
            bufButScript = mapButtons[x, y + 1].GetComponent<QuestionScopeScript>();
            if (bufButScript.State == ButtonState.NotAvailable)
                bufButScript.State = ButtonState.NotUsed;
        }
    }

    public void Finish()
    {
        canvasQuestions.gameObject.SetActive(false);
        gameObject.SetActive(false);
        finishCanvas.gameObject.SetActive(true);
        finishCanvas.GetComponent<FinishScript>().Points = points;
        finish = true;
    }
}