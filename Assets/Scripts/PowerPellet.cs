
using UnityEngine;

public class PowerPellet : Pellet
{
    public float duration = 8.0f;
    protected override void Eaten()
    {
        FindObjectOfType<GameManager>().PowerPelletEaten(this);
    }
}
