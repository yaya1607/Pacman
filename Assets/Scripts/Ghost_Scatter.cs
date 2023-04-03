
using UnityEngine;

public class Ghost_Scatter : Ghost_Behavior
{
    private void OnTriggerEnter2D(Collider2D collider)
    {
        Node node = collider.GetComponent<Node>();
        if(node != null && this.enabled && !this.ghost.frightened.enabled)
        {
            int index = Random.Range(0, node.enableDirection.Count);

            if(node.enableDirection[index] == -this.ghost.movement.direction && node.enableDirection.Count >1)
            {
                index++;
                if(index >= node.enableDirection.Count)
                {
                    index = 0;
                }
            }this.ghost.movement.SetDirection(node.enableDirection[index]);
        }
        
    }
    private void Start()
    {
        Debug.Log("Scatter");
    }
    private void OnDisable()
    {
        this.ghost.chase.Enable();
    }
}
