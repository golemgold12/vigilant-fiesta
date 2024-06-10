using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyVitals : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] float Health = 0, MaxHealh = 1f;
    bool Alive = false;
    AudioSource source;
    EnemyAI Enemy;
    Rigidbody2D rb;
    void Start()
    {
        source = gameObject.GetComponent<AudioSource>();
        Health = MaxHealh;
        Alive = true;
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    public float GetHealth()
    {

        return Health;
    }
    private void OnParticleCollision(GameObject other)
    {
        Debug.Log("OKAY");
        if(other.gameObject.layer == 8 && Alive == true)
        {
            IncrementHealth();

        }
    }
    void IncrementHealth()
    {
        if(Health > MaxHealh + 1)
        {

            Health--;
        }
        else
        {
            Alive = false;
            KillEnemy();
        }

    }

    void KillEnemy()
    {
        if(rb != null)
        {
            source.PlayOneShot(source.clip);
            gameObject.GetComponent<Animator>().SetBool("Destroyed", true);
            rb.velocity = Vector2.zero;
            rb.AddForce(Vector2.down * 900f * rb.mass);

        }
        Destroy(this.gameObject, .7f);
    }
}
