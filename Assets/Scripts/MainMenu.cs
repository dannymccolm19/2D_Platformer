using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Button exit;
    public Button start;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        start.onClick.AddListener(StartGame);
        exit.onClick.AddListener(ExitGame);
        player.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void StartGame(){
        gameObject.SetActive(false);
        player.SetActive(true);
        //Instantiate(player, new Vector3(-7.11f, -3.608f, 0), Quaternion.identity );
    }

    void ExitGame(){
        Application.Quit();
    }
}
