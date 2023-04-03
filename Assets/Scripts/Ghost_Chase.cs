
using UnityEngine;

public class Ghost_Chase : Ghost_Behavior
{
    private void OnTriggerEnter2D(Collider2D collider)
    {
        Node node = collider.GetComponent<Node>();
        if (node != null && this.enabled && !this.ghost.frightened.enabled)
        {
            Vector2 direction = Vector2.zero;
            float minDistance = float.MaxValue;

            foreach ( Vector2 enableDirection in node.enableDirection)
            {
                Vector3 newPosition = this.transform.position + new Vector3(enableDirection.x, enableDirection.y);
                float distance = (this.ghost.target.position - newPosition).sqrMagnitude;
                if(distance < minDistance)
                {
                    direction = enableDirection;
                    minDistance = distance;
                }
            }
            this.ghost.movement.SetDirection(direction);
        }

    }
    private void OnDisable()
    {
        this.ghost.scatter.Enable();
    }

}
