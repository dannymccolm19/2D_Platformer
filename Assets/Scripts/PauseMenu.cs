using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public Button exit;
    public Button resume;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        resume.onClick.AddListener(ToggleMenu);
        exit.onClick.AddListener(ExitGame);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ToggleMenu()
    {			
        if (gameObject.activeSelf){
            gameObject.SetActive(false);
            player.SetActive(true);
        }	
        else{
            gameObject.SetActive(true);
            player.SetActive(false);
        }      
    }

    void ExitGame(){
        Application.Quit();
    }
}
