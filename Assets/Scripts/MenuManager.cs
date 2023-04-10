using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public Button continueButton;

    private void Awake()
    {
        
    }
    private void Start()
    {
        if (File.Exists(Application.dataPath + "/SaveGame/Save.txt"))
        {
            continueButton.gameObject.SetActive(true);
        }
        else
        {
            continueButton.gameObject.SetActive(false);
        }    
    }
    public void NewGame()
    {
        if (File.Exists(Application.dataPath + "/SaveGame/Save.txt"))
        {
            File.Delete(Application.dataPath + "/SaveGame/Save.txt");
        }
        Continue();
    }
    public void Continue()
    {
        SceneManager.LoadScene("Pacman");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
