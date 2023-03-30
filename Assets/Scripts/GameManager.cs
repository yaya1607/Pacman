
using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Pacman pacman;
    public Ghost[] ghost;
    public Transform pellets;
    public int score { get; private set; }
    public int ghostMultiplier { get; private set; }
    public int lives { get; private set; }
    private void Start()
    {
        NewGame();
    }

    private void NewGame()
    {
        SetScore(0);
        SetLives(3);
        NewRound();
    }

    private void SetLives(int lives)
    {
        this.lives = lives;
    }

    private void SetScore(int score)
    {
        this.score = score;
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
            ghost[i].gameObject.SetActive(true);
        }
        pacman.gameObject.SetActive(true);
    }

    private void GameOver()
    {
        
        for (int i = 0; i < ghost.Length; i++)
        {
            ghost[i].gameObject.SetActive(false);
        }
        pacman.gameObject.SetActive(false);
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
            Invoke(nameof(ResetState), 3f);
        }
    }

    public void PelletEaten(Pellet pellet)
    {
        pellet.gameObject.SetActive(false);
        SetScore(this.score + pellet.points);
        Debug.Log("Score: " +score);
        if (!HasRemainingPellets())
        {
            this.pacman.gameObject.SetActive(false);
            Invoke(nameof(NewGame), 3f);
        }

    }

    public void PowerPelletEaten( PowerPellet pellet)
    {
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
        if (lives <= 0 && Input.anyKeyDown)
        {
            NewGame();
        }
    }


}
