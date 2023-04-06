
using UnityEngine;

public class Pacman : MonoBehaviour
{
    public Movement movement { get; private set; }
    public Rigidbody2D rigidbody { get; private set; }
    
    private void Awake()
    {
        movement = GetComponent<Movement>();
        rigidbody = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            this.movement.SetDirection(Vector2.up);
        }
        else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            this.movement.SetDirection(Vector2.left);
        }
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            this.movement.SetDirection(Vector2.down);
        }
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            this.movement.SetDirection(Vector2.right);
        }
        float angle = Mathf.Atan2(movement.direction.y, movement.direction.x);
        this.transform.rotation = Quaternion.AngleAxis(angle * Mathf.Rad2Deg, Vector3.forward);
    }
    public void ResetState()
    {
        this.movement.ResetState();
        this.gameObject.SetActive(true);
    }

    public void SetPosition(Vector3 position)
    {
        this.rigidbody.position = position;
    }
}
