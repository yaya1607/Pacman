
using UnityEngine;

public class Passager : MonoBehaviour
{
    public Transform connection;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.transform.position = connection.transform.position; 
    }
}
