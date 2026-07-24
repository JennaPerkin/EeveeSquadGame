using UnityEngine;

public class EndGame : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        Application.Quit();
    }
}
