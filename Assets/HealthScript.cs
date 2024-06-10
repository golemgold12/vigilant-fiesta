using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using Cinemachine;
using TMPro;
public class HealthScript : MonoBehaviour
{
    // Start is called before the first frame update

    public bool alive = true;
    bool db = false;
    [SerializeField] int CurrentShield = 2000;
    [SerializeField] int MaxShield = 2000;
    [SerializeField] SpriteRenderer sprite;
    [SerializeField] TMP_Text Health;
    [SerializeField] TMP_Text Ammo;

    void Start()
    {
        CurrentShield = MaxShield;
        Health.text = "Health: " + CurrentShield.ToString();
        db = false;
    }

    // Update is called once per frame
    private void OnParticleCollision(GameObject other)
    {
        if (alive == true && db == false)
        {

         
            if(other.gameObject.tag == "EnemyProj")
            {
                db = true;
                StartCoroutine(DbThing(50));
            }
        }
    }
  void   OnCollisionEnter2D(Collision2D colli)
    {
        if (alive == true && db == false)
        {


            if (colli.gameObject.tag == "EnemyProj")
            {
                db = true;
                StartCoroutine(DbThing(50));
            }
        }
    }

    IEnumerator DbThing(int Damage = 33)
    {
        yield return null;
        if(CurrentShield - Damage > 0)
        {

            CurrentShield -= Damage;
            sprite.color = Color.black;
            yield return new WaitForSeconds(.1f);
            sprite.color = Color.white;
            Health.text = "Health: " + CurrentShield.ToString();

        }
        else if(alive == true)
        {
            alive = false;
            CurrentShield -= Damage;

            Health.text = "Health: " + CurrentShield.ToString();

            StartCoroutine(gameOver());
        }
        yield return new WaitForSeconds(.1f);
        db = false;
    }


    IEnumerator gameOver()
    {
        gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;
        gameObject.GetComponent<AudioSource>().PlayOneShot(gameObject.GetComponent<AudioSource>().clip);
        sprite.color = Color.black;

        //status.text = "GAME OVER";
        // status.enabled = true;
        yield return new WaitForSeconds(3.5f);
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);

    }
}
