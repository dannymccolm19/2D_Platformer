using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FinishScreen : MonoBehaviour
{
    public TMP_Text textm;
    public PlayerController player;
    int score = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        score = player.GetScore();
        string msg = "Congratulations, you escaped the lab!  You found " + score.ToString() + "/3 sweets";
        textm.text = msg;
    }
}
