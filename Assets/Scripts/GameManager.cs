
using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Pacman pacman;
    public Ghost[] ghost;
    public Transform pellets;
    private int score;

    public int GetScore()
    {
        return score;
    }

    private void SetScore(int value)
    {
        score = value;
    }

    private int lives;

    public int GetLives()
    {
        return lives;
    }

    private void SetLives(int value)
    {
        lives = value;
    }

    private void Start()
    {
        NewGame();
    }

    private void NewGame()
    {
        SetScore(0);
        SetLives(3);
        NewRoute();
    }

    private void NewRoute()
    {
        foreach (Transform pellet in this.pellets)
        {
            pellet.gameObject.SetActive(true);
        }
        ResetRoute();
    }

    private void ResetRoute()
    {
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
            Invoke(nameof(ResetRoute), 3f);
        }
    }
    private void Update()
    {
        if (lives <= 0 && Input.anyKeyDown)
        {
            NewGame();
        }
    }
}
