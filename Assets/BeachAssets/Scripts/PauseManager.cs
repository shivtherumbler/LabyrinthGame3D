using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    public bool isPaused;
    public GameObject pausemenu;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void PausePlay()
    {
        if (isPaused == true)
        {
            Time.timeScale = 1;
            isPaused = false;
            pausemenu.SetActive(false);
        }
        else
        {
            Time.timeScale = 0;
            isPaused = true;
            pausemenu.SetActive(true);
        }
    }

    public void Open(GameObject open)
    {
        open.SetActive(true);
    }

    public void Close(GameObject close)
    {
        close.SetActive(false);
    }

    public void Retry()
    {
        SceneManager.LoadScene("BeachLevel");
        Time.timeScale = 1;
    }

    public void Home()
    {
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1;
    }
}
