using UnityEngine;

public class EnemyDealDamage : MonoBehaviour
{
    public HealthSystemAttribute healthScript;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                Debug.Log("CollidesWithPlayer");
                healthScript = collision.gameObject.GetComponent<HealthSystemAttribute>();
                healthScript.health -= 1;
            }
        }
    }
}
