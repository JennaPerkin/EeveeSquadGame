using UnityEngine;

public class EndGame : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        Application.Quit();
    }
}
