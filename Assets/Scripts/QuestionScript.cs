using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using System.Xml.Serialization;

public class QuestionScript : MonoBehaviour
{
    public Text questionText;
    public Transform ansver1;
    public Transform ansver2;
    public Transform ansver3;
    public Transform ansver4;

    public float lasting;

    private Transform[] ansvers;

    public Text timer;
    public float startTime = 30;
    private float curentTime;

    public GameMap gameMap;

    private Queue<Question> quests;
    private Question qurentQuest;

    private bool ansvered = false;

    private string path;
    void Start()
    {
    }

    void Update()
    {
        if (curentTime > 0 && !ansvered)
        {
            timer.text = Convert.ToString(Math.Ceiling(curentTime));
            curentTime -= Time.deltaTime;
            if(curentTime<=0)
            {
                StartCoroutine(FalseAnsver());
                curentTime = 0;
            }
        }
    }

    void Awake()
    {
        List<Question> listQuests = new List<Question>();
#if UNITY_ANDROID && !UNITY_EDITOR
        path = Path.Combine(Application.persistentDataPath, "quests.json");
#else
        path = Path.Combine(Application.dataPath, "quests.xml");
#endif
        if (File.Exists(path))
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<Question>));
            StreamReader reader = new StreamReader(path);
            listQuests = serializer.Deserialize(reader.BaseStream) as List<Question>;
            reader.Close();

            //listQuests = JsonUtility.FromJson<List<Question>>(File.ReadAllText(path));
        }
        else
        {

            Question buf = new Question();
            buf.question = "Test ansver";
            buf.ansver = new string[] { "1", "2", "3", "4" };
            buf.trueAnsver = 3;
            listQuests.Add(buf);
            listQuests.Add(buf);

            XmlSerializer serializer = new XmlSerializer(typeof(List<Question>));
            StreamWriter writer = new StreamWriter(path);
            serializer.Serialize(writer.BaseStream, listQuests);
            writer.Close();

            //File.WriteAllText(path, JsonUtility.ToJson(listQuests.ToArray()));
        }

        quests = new Queue<Question>(listQuests);

        ansvers = new Transform[] { ansver1, ansver2, ansver3, ansver4 };
        //NextQuestion();
    }

    public void NextQuestion()
    {
        if (quests.Count > 0)
        {
            qurentQuest = quests.Dequeue();
            SetAnsvers(qurentQuest);
            curentTime = startTime;
            for (int i = 0; i < 4; ++i)
                ansvers[i].GetComponent<Graphic>().color = Color.white;
            ansvered = false;
        }
        else
            StartCoroutine(Finish());
    }

    IEnumerator Finish()
    {
        questionText.text = "Вопросы кончились";
        for (int i = 0; i < 4; ++i)
            ansvers[i].gameObject.SetActive(false);
        yield return new WaitForSeconds(1.5f);
        gameMap.Finish();
    }

    void SetAnsvers(Question q)
    {
        Text text;
        questionText.text = q.question;

        for(int i = 0; i<4; ++i)
        {
            text = ansvers[i].Find("Text").GetComponent<Text>();
            text.text = q.ansver[i];
        }
    }

    IEnumerator FalseAnsver()
    {
        if (!ansvered)
            ansvers[qurentQuest.trueAnsver - 1].GetComponent<Graphic>().color = Color.green;
        ansvered = true;
        gameMap.curentScope.FalseAnsvered();
        yield return new WaitForSeconds(1.5f);
        gameMap.CheckAvalibleMap();
        gameObject.SetActive(false);
    }
    IEnumerator TrueAnsver()
    {
        if (!ansvered)
        {
            ansvers[qurentQuest.trueAnsver - 1].GetComponent<Graphic>().color = Color.green;
            gameMap.AddPoint();
        }
        ansvered = true;
        gameMap.curentScope.TrueAnsvered();
        yield return new WaitForSeconds(1.5f);
        gameMap.CheckAvalibleMap();
        gameObject.SetActive(false);
    }
    public void Ansver1()
    {
        if (!ansvered)
            if (qurentQuest.trueAnsver == 1)
                StartCoroutine(TrueAnsver());
            else
            {
                ansvers[0].GetComponent<Graphic>().color = Color.red;
                StartCoroutine(FalseAnsver());
            }
    }

    public void Ansver2()
    {
        if (!ansvered)
            if (qurentQuest.trueAnsver == 2)
                StartCoroutine(TrueAnsver());
            else
            {
                ansvers[1].GetComponent<Graphic>().color = Color.red;
                StartCoroutine(FalseAnsver());
            }
    }

    public void Ansver3()
    {
        if (!ansvered)
            if (qurentQuest.trueAnsver == 3)
                StartCoroutine(TrueAnsver());
            else
            {
                ansvers[2].GetComponent<Graphic>().color = Color.red;
                StartCoroutine(FalseAnsver());
            }
    }

    public void Ansver4()
    {
        if (!ansvered)
            if (qurentQuest.trueAnsver == 4)
                StartCoroutine(TrueAnsver());
            else
            {
                ansvers[3].GetComponent<Graphic>().color = Color.red;
                StartCoroutine(FalseAnsver());
            }
    }

    [Serializable]
    public class Question
    {
        public string question;
        public string[] ansver = new string[4];
        public byte trueAnsver;
    }
}

