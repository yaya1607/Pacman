
using System;
using UnityEngine;

public class Ghost_Frightened : Ghost_Behavior
{
    public SpriteRenderer body;
    public SpriteRenderer eyes;
    public SpriteRenderer blue;
    public SpriteRenderer white;
    public bool eaten { get; private set; }

    public override void Enable(float duration)
    {
        base.Enable(duration);
        this.body.enabled = false;
        this.eyes.enabled = false;
        this.blue.enabled = true;
        this.white.enabled = false;

        Invoke(nameof(Flash),duration/2.0f);
    }
    public override void Disable()
    {
        base.Disable();

        this.body.enabled = true;
        this.eyes.enabled = true;
        this.blue.enabled = false;
        this.white.enabled = false;
    }
    private void Flash()
    {
        if (!eaten)
        {
            this.blue.enabled = false;
            this.white.enabled = true;
        }
    }
    private void OnEnable()
    {
        this.ghost.movement.speedMultiplier = 0.5f;
        this.eaten = false;
    }

    private void OnDisable()
    {
        this.ghost.movement.speedMultiplier = 1f;
        this.eaten = false;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Pacman"))
        {
            if (this.enabled)
            {
                this.Eaten();
            }

        }
    }

    private void Eaten()
    {
        this.eaten = true;

        this.body.enabled = false;
        this.eyes.enabled = true;
        this.blue.enabled = false;
        this.white.enabled = false;
        Vector3 position = this.ghost.home.inside.position;
        position.z = this.ghost.transform.position.z;
        this.ghost.transform.position = position;
        this.ghost.home.Enable(this.duration);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        Node node = collider.GetComponent<Node>();
        if (node != null && this.enabled)
        {
            Vector2 direction = Vector2.zero;
            float maxDistance = float.MinValue;

            foreach (Vector2 enableDirection in node.enableDirection)
            {
                Vector3 newPosition = this.transform.position + new Vector3(enableDirection.x, enableDirection.y);
                float distance = (this.ghost.target.position - newPosition).sqrMagnitude;
                if (distance > maxDistance)
                {
                    direction = enableDirection;
                    maxDistance = distance;
                }
            }
            this.ghost.movement.SetDirection(direction);
        }

    }
}
