using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowText : MonoBehaviour
{
    public Text score;
    public Text lives;
    public Text notification;
    public Text highScore;

    public void SetScore( int score)
    {
        this.score.text = "Score: "+score;
    }
    public void SetLives(int lives)
    {
        this.lives.text = "Lives: x"+lives;
    }
    public void SetNotification(string notification)
    {
        this.notification.enabled = true;
        this.notification.text = notification;
    }
    public void SetHighScore(int highScore)
    {
        this.highScore.text = "Hi-Score:\n"+ highScore;
    }
    public void TurnOffNotification()
    {
        this.notification.enabled = false;
    }
}
