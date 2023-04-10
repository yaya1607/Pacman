using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    public Rigidbody2D rigidbody { get; private set; }
    public Movement movement { get; private set; }
    public Ghost_Chase chase {get; private set;}
    public Ghost_Scatter scatter { get; private set; }
    public Ghost_Home home { get; private set; }
    public Ghost_Frightened frightened { get; private set; }
    public Ghost_Behavior initialBehavior;
    public Transform target;
    public int points = 200;

    private void Awake()
    {
        this.rigidbody = GetComponent<Rigidbody2D>();
        this.movement = GetComponent<Movement>();
        this.home = GetComponent<Ghost_Home>();
        this.scatter = GetComponent<Ghost_Scatter>();
        this.chase = GetComponent<Ghost_Chase>();
        this.frightened = GetComponent<Ghost_Frightened>();
    }

    public void ResetState()
    {
        this.movement.ResetState();
        this.gameObject.SetActive(true);
        this.frightened.Disable();
        this.scatter.Enable();
        this.chase.Disable();
        if(this.initialBehavior != this.home)
        {
            this.home.Disable();
        }
        else
        {
            this.initialBehavior.Enable();
        }
    }
    private void OnCollisionEnter2D(Collision2D collision){
        if(collision.gameObject.layer == LayerMask.NameToLayer("Pacman"))
        {
            if (this.frightened.enabled)
            {
                FindAnyObjectByType<GameManager>().GhostEaten(this);
            }
            else
            {
                FindAnyObjectByType<GameManager>().PacmanEaten();
            }
             
        }   
    }
    public void SetPosition(Vector3 position)
    {
        this.rigidbody.position = position;
    }

}
