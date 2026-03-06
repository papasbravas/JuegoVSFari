using UnityEngine;

public class HitBoss : MonoBehaviour
{
    public int damage;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerHealth>().TakeDamage(damage);
            Debug.Log("HAGO DAÑO A PLAYER");
            GetComponent<SphereCollider>().enabled = false;
        }
    }
}
