
using System;
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
        NewGame();
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
        soundManager.PlayMusic() ;
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
        }
        
        pacman.ResetState();
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


}
