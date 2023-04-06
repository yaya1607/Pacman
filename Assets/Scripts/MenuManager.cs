using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    
    public TextMeshProUGUI playText;

    private void Awake()
    {
        
    }
    private void Start()
    {
        if (File.Exists(Application.dataPath + "/SaveGame/Save.txt"))
        {
            playText.SetText("Continue");
        }
        else
        {
            playText.SetText("Play");
        }
    }
    public void Play()
    {
        SceneManager.LoadScene("Pacman");
    }
}
