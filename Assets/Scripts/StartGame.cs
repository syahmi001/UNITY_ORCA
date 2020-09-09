using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartGame : MonoBehaviour
{

    //public float timeLeft = 3.0f;
    public float timeLeft = 0;
    public Text startText; // used for showing countdown from 3, 2, 1 


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        timeLeft += Time.deltaTime;
        startText.text = (timeLeft).ToString("0") + " second(s)";
        if (timeLeft > 0)
        {
            //Do something useful or Load a new game scene depending on your use-case

        }
    }
}
