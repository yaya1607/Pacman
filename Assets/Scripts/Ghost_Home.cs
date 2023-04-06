using System.Collections;
using UnityEngine;

public class Ghost_Home : Ghost_Behavior
{
    public Transform inside;
    public Transform outside;

    private void OnDisable()
    {
        if (this.gameObject.activeSelf)
        {
            StartCoroutine(ExitTransition());
        }
    }

    private void OnEnable()
    {
        if (this.gameObject.activeSelf)
        {
            StopAllCoroutines();
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(this.enabled && collision.gameObject.layer == LayerMask.NameToLayer("Obstacle"))
        {
            this.ghost.movement.SetDirection(new Vector2(-this.ghost.movement.direction.x, -this.ghost.movement.direction.y),true);
        }
    }

    private IEnumerator ExitTransition()
    {
        this.ghost.movement.SetDirection(Vector2.up, true);
        this.ghost.movement.rigidbody.isKinematic = true;
        this.ghost.movement.enabled = false;

        Vector3 position = this.transform.position;
        float duration = 0.5f;
        float elapsed = 0f;

        while(elapsed < duration)
        {
            Vector3 newPosition = Vector3.Lerp(position, this.inside.position, elapsed/duration);
            newPosition.z = position.z;
            this.ghost.transform.position = newPosition;
            elapsed += Time.deltaTime;
            yield return null;
        }

        elapsed = 0f;

        while (elapsed < duration)
        {
            Vector3 newPosition = Vector3.Lerp(this.inside.position, this.outside.position, elapsed / duration);
            newPosition.z = position.z;
            this.ghost.transform.position = newPosition;
            elapsed += Time.deltaTime;
            yield return null;
        }


        this.ghost.movement.SetDirection(new Vector2(Random.value < 0.5 ? -1f : 1f, 0f), true);
        this.ghost.movement.rigidbody.isKinematic = false;
        this.ghost.movement.enabled = true;
    } 
}
