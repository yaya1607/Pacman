
using System;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    
    public Pacman pacman;
    public Ghost[] ghost;
    public Transform pellets;
    public ShowText showText;

    public bool inGame { get; private set; }
    public String notification { get; private set; }
    public int score { get; private set; }
    public int ghostMultiplier { get; private set; }
    public int lives { get; private set; }
    public BackgroundMusic soundManager { get; private set; }
    private void Start()
    {
        
        if(File.Exists(Application.dataPath + "/SaveGame/Save.txt"))
        {
            LoadGame();
        }
        else
        {
            NewGame();
        }
    }

    private void Awake()
    {
        soundManager = GetComponentInChildren<BackgroundMusic>();
    }

    private void NewGame()
    {
        inGame = true;
        SetScore(0);
        SetLives(3);
        NewRound();
        soundManager.PlayMusic();
    }

    private void SetLives(int lives)
    {
        this.lives = lives;
        showText.SetLives(this.lives);
    }

    private void SetScore(int score)
    {
        this.score = score;
        showText.SetScore(this.score);
        
    }

    private void NewRound()
    {
        foreach (Transform pellet in this.pellets)
        {
            pellet.gameObject.SetActive(true);
        }
        ResetState();
    }

    private void ResetState()
    {
        ResetGhostMultiplier();
        for (int i=0; i< ghost.Length; i++)
        {
            ghost[i].ResetState();
            ghost[i].isLoad = false;
            ghost[i].movement.isLoad = false;
        }
        
        pacman.ResetState();
        pacman.movement.isLoad = false;
    }

    private void GameOver()
    {
        
        for (int i = 0; i < ghost.Length; i++)
        {
            ghost[i].gameObject.SetActive(false);
        }
        pacman.gameObject.SetActive(false);
        soundManager.PlayLoseSound();
        notification = "You lose\nPress any key to play again.";
        showText.SetNotification(notification);
        inGame = false;
    }

    public void GhostEaten(Ghost ghost)
    {
        this.SetScore(this.score + ghost.points);
        this.ghostMultiplier++;
    }

    public void PacmanEaten()
    {
        this.SetLives(this.lives - 1);
        if (lives <= 0)
        {
            GameOver();
        }
        else
        {
            soundManager.PlayDeathSound();
            pacman.gameObject.SetActive(false);
            Invoke(nameof(ResetState), 3f);
        }
    }

    public void PelletEaten(Pellet pellet)
    {
        pellet.gameObject.SetActive(false);
        SetScore(this.score + pellet.points);
        if (!HasRemainingPellets())
        {
            WinGame();
        }

    }

    private void WinGame()
    {
        notification = "You win\nPress any key to play again.";
        showText.SetNotification(notification);
        soundManager.PlayWinSound();
        this.pacman.gameObject.SetActive(false);
        inGame = false;
    }

    public void PowerPelletEaten( PowerPellet pellet)
    {
        for(int i = 0; i < ghost.Length; i++)
        {
            if (!ghost[i].home.enabled)
            {
                ghost[i].frightened.Enable(pellet.duration);
            }
        }
        PelletEaten(pellet);
        CancelInvoke();
        Invoke(nameof(ResetGhostMultiplier), pellet.duration);
    }

    private bool HasRemainingPellets()
    {
        foreach (Transform pellet in this.pellets)
        {
            if (pellet.gameObject.activeSelf)
            {
                return true;
            }
        }
        return false;
    }

    private void ResetGhostMultiplier()
    {
        this.ghostMultiplier = 1;
    }

    private void Update()
    {
        if (!inGame && Input.anyKeyDown )
        {
            NewGame();
            showText.TurnOffNotification();
        }
    }

    private void OnApplicationQuit()
    {
        if(inGame)
            SaveGame();
    }

    private void LoadGame()
    {
        inGame = true;
        int index = 0;
        string json = File.ReadAllText(Application.dataPath + "/SaveGame/Save.txt");
        SaveData loadData = JsonUtility.FromJson<SaveData>(json);
        this.pacman.SetPosition(loadData.pacmanPosition);
        this.pacman.movement.isLoad = true;
        this.pacman.movement.SetDirection( loadData.pacmanDirection,true);
        this.SetScore(loadData.score);
        this.SetLives(loadData.lives);
        foreach (Transform pellet in this.pellets)
        {
            if (loadData.pellets[index] == true)
            {
                pellet.gameObject.SetActive(true);
            }
            else
            {
                pellet.gameObject.SetActive(false);
            }  
            index++;
        }
        for(int i =0; i< ghost.Length; i++)
        {
            LoadGhostData(loadData,i);
        }
        File.Delete(Application.dataPath + "/SaveGame/Save.txt");
    }

    private void SaveGame()
    {
        SaveData saveData = new SaveData();
        saveData.pacmanDirection = this.pacman.movement.direction;
        saveData.pacmanPosition = this.pacman.transform.position;
        saveData.score = this.score;
        saveData.lives = this.lives;
        saveData.pellets = new bool[this.pellets.childCount];
        saveData.ghostPosition = new Vector3[this.ghost.Length];
        saveData.ghostDirection = new Vector2[this.ghost.Length];
        saveData.ghostHome = new bool[this.ghost.Length];
        saveData.ghostFrightened = new bool[this.ghost.Length];
        saveData.ghostChase = new bool[this.ghost.Length];
        saveData.ghostScatter = new bool[this.ghost.Length];
        saveData.ghostHomeDuration = new float[this.ghost.Length];
        saveData.ghostChaseDuration = new float[this.ghost.Length];
        saveData.ghostFrightenedDuration = new float[this.ghost.Length];
        saveData.ghostScatterDuration = new float[this.ghost.Length];

        int index = 0;
        foreach(Transform pellet in this.pellets)
        {
            if (pellet.gameObject.activeSelf)
            {
                saveData.pellets[index] = true;
            }
            else
            {
                saveData.pellets[index] = false;
            }
            index++;
        }
        for(int i =0; i < this.ghost.Length; i++)
        {
            SaveGhostData(saveData, i);
        }

        string json = JsonUtility.ToJson(saveData);
        File.WriteAllText(Application.dataPath+ "/SaveGame/Save.txt", json);
        Debug.Log(json);
    }

    private class SaveData
    {
        public Vector2 pacmanDirection;
        public Vector3 pacmanPosition;
        public int score;
        public int lives;
        public bool[] pellets;
        public Vector2[] ghostDirection;
        public Vector3[] ghostPosition;
        public bool[] ghostHome;
        public bool[] ghostFrightened;
        public bool[] ghostChase;
        public bool[] ghostScatter;
        public float[] ghostHomeDuration;
        public float[] ghostChaseDuration;
        public float[] ghostFrightenedDuration;
        public float[] ghostScatterDuration;
    }
    private void SaveGhostData(SaveData saveData, int index)
    {
        saveData.ghostDirection[index] = this.ghost[index].movement.direction;
        saveData.ghostPosition[index] = this.ghost[index].transform.position;
        saveData.ghostHome[index] = this.ghost[index].home.enabled;
        saveData.ghostChase[index] = this.ghost[index].chase.enabled;
        saveData.ghostFrightened[index] = this.ghost[index].frightened.enabled;
        saveData.ghostScatter[index] = this.ghost[index].scatter.enabled;
        saveData.ghostHomeDuration[index] = this.ghost[index].home.DurationRemaining();
        saveData.ghostChaseDuration[index] = this.ghost[index].chase.DurationRemaining();
        saveData.ghostFrightenedDuration[index] = this.ghost[index].frightened.DurationRemaining();
        saveData.ghostScatterDuration[index] = this.ghost[index].scatter.DurationRemaining();
    }
    private void LoadGhostData(SaveData loadData, int index)
    {
        this.ghost[index].isLoad = true;
        this.ghost[index].SetPosition(loadData.ghostPosition[index]);
        this.ghost[index].movement.isLoad = true;
        this.ghost[index].movement.SetDirection(loadData.ghostDirection[index], true);
        if (loadData.ghostHome[index])
            this.ghost[index].home.Enable(loadData.ghostHomeDuration[index]);
        else
            this.ghost[index].home.Disable();

        if (loadData.ghostChase[index])
            this.ghost[index].chase.Enable(loadData.ghostChaseDuration[index]);
        else
            this.ghost[index].chase.Disable();

        if (loadData.ghostFrightened[index])
            this.ghost[index].frightened.Enable(loadData.ghostFrightenedDuration[index]);
        else
            this.ghost[index].frightened.Disable();

        if (loadData.ghostScatter[index])
            this.ghost[index].scatter.Enable(loadData.ghostScatterDuration[index]);
        else
            this.ghost[index].scatter.Disable();
    }
}
