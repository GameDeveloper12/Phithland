using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClientScript : MonoBehaviour
{
    public Text wait;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Awake()
    {
        StartCoroutine(Wait());
    }

    IEnumerator Wait()
    {
        wait.text = "Включение мультиплей режима...";
        yield return new WaitForSeconds(2);

        while (true)
        {
            wait.text = "Подождите.";
            yield return new WaitForSeconds(1);
            wait.text = "Подождите..";
            yield return new WaitForSeconds(1);
            wait.text = "Подождите...";
            yield return new WaitForSeconds(1);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
