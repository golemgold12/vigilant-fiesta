using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] bool alive = true;
    [SerializeField] bool canfire = false;
   [SerializeField] GameObject gunBarrel;
    [SerializeField] ParticleSystem party;
    public float InitialBoost = 200f;
    
    [SerializeField] Rigidbody2D rb;
    enum Power { Shooter, Bomber, Crawler, Crazy, Boss };
    [SerializeField] Power ourPower;
    [SerializeField] float shootDelay = .2f, inaccuracy = 10f, realshootDelay = 2f;
    RaycastHit2D shit;
    [SerializeField] int shootAmount = 3;
    int flipVal = 1;
   public LayerMask shitmsk;
    void Start()
    {
        flipVal = 1;
        rb = gameObject.GetComponent<Rigidbody2D>();
        if (gunBarrel)
        {
            party = gunBarrel.GetComponent<ParticleSystem>();
        }
        if (rb)
        {

            rb.AddForce(transform.right * InitialBoost * rb.mass);
        }
    }

    private void FixedUpdate()
    {
    }

    // Update is called once per frame
    void Update()
    {


        shit = Physics2D.Raycast(gameObject.transform.position, Vector2.right * flipVal,3f, ~shitmsk);
        Debug.DrawRay(gameObject.transform.position, Vector2.right * 3f);

        if (shit.collider != null && shit.collider.gameObject != gameObject)
        {
            FlipAndAddForce();
            Debug.Log(shit.collider);
        }
        if (alive && party && gunBarrel)
        {
            ProcessAI();

        }



    }
   void FlipAndAddForce()
    {
        flipVal = -flipVal;
        gameObject.GetComponent<SpriteRenderer>().flipX = !gameObject.GetComponent<SpriteRenderer>().flipX;
        rb.AddForce(transform.right * InitialBoost * 2 * rb.mass * flipVal);

    }
    /*
     * 
     * 
     * 
    IEnumerator LerpFunction(float endValue, float duration)
    {
        float time = 0;
        float startValue = valueToChange;

        while (time < duration)
        {
            valueToChange = Mathf.Lerp(startValue, endValue, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        valueToChange = endValue;
    }
    */
    void ProcessAI()
    {

        if(canfire == false)
        {

            canfire = true;

            switch (ourPower)
            {

                case Power.Shooter:
                    Debug.Log("Failure.");

                    StartCoroutine(ShooterCoro());
                    break;

                default:
                    Debug.Log("Failure.");
                    break;
            }
        }

    }

    IEnumerator ShooterCoro()
    {

        float OGRotation = gunBarrel.transform.localEulerAngles.z;

        for (int i = 0; i < shootAmount; i++)
        {

            gunBarrel.transform.Rotate(new Vector3(0, 0, Random.Range(-inaccuracy, inaccuracy)));
            yield return new WaitForSeconds(shootDelay);
            party.Emit(1);
            gunBarrel.transform.localEulerAngles = new Vector3(0, 0, OGRotation);
        }
        yield return null;
        yield return new WaitForSeconds(realshootDelay);

        canfire = false;
    }
}
