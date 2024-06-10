using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Rigidbody2D rb;
    [SerializeField] float maxSpeedX, maxSpeedY;
    [SerializeField] GameObject gun;
    [SerializeField] float acceleration;
    [SerializeField] float maxAccelration;
    [SerializeField] HealthScript h;
    //Animator anim;
    bool canPick = false;
    bool canFire = true;
    bool flip = false;
    bool db = false;
    bool canPick2 = false;
    enum Power { Clay, Aster, Rose, Peony, Lotus };
    [SerializeField] Power CurrentPower;
    float xMove, yMove;
    private PlayerBulletScript bull;
    private FlowerScript currentFlower;

    public GameObject[] projList;
    void Start()
    {
        canPick = true;
        db = false;
        maxAccelration = acceleration;
        acceleration = 0;
        bull = gameObject.GetComponent<PlayerBulletScript>();
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(h.alive == true)
        {
            ProcessInputs();

        }

        if (flip == false && xMove == -1)
        {

            gameObject.GetComponent<SpriteRenderer>().flipX = true;
            flip = !flip;
        }
        else if (flip == true && xMove == 1)
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
            flip = !flip;
        }
    }

    
    private void FixedUpdate()
    {
        Physmove();
       
    }
    void ProcessInputs()
    {
        yMove = Input.GetAxis("Vertical");
        xMove = Input.GetAxis("Horizontal");

        if (Input.GetKey(KeyCode.F) && currentFlower && canPick == true)
        {
            Debug.Log("Picking Flower");

            switch (currentFlower.FlowerType)
            {
                case 3:
                    break;

                case 2:
                    CurrentPower = Power.Rose;
                    Debug.Log("got it!");
                    break;

                case 1:
                    CurrentPower = Power.Aster;

                    break;

                case 0:
                    CurrentPower = Power.Clay;

                    break;

                default:
                    CurrentPower = Power.Clay;
                    
                    break;


            }
            canFire = true;
            canPick = false;
            currentFlower.Pick();
            Debug.Log("PICK!");
        }
        if (Input.GetKeyDown(KeyCode.K) && canFire == true && CurrentPower != Power.Clay) {

            UseFlower();
            canPick = true;
        
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {

            Application.Quit();
        }
    }


    void Physmove()
    {
        
        if(Mathf.Abs(rb.velocity.magnitude) < maxSpeedX * .05f && xMove != 0)
        {
            if (db == false)
            {


                StartCoroutine(LerpAcceleraton(.75f));
            }
        }

        if(Mathf.Abs(rb.velocity.magnitude) < maxSpeedX)
        {

            rb.AddForce(transform.right * xMove * acceleration * rb.mass);
            rb.AddForce(transform.up * yMove * acceleration * rb.mass);

        }
      
    }




    IEnumerator LerpAcceleraton(float duration)
    {
        db = true;
        acceleration = 0;
        float time = 0;
        float startValue = acceleration;

        while (time < duration)
        {
            acceleration = Mathf.Lerp(startValue, maxAccelration, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        db = false;
        acceleration = maxAccelration;
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == 3)
        {

            gameObject.GetComponent<Animator>().SetBool("Landing", true);

        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 3)
        {

            gameObject.GetComponent<Animator>().SetBool("Landing", false);

        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "Flor" && collision.gameObject.GetComponent<FlowerScript>() && currentFlower == null)
        {
            if(collision.gameObject.GetComponent<FlowerScript>().Pickable == true && canPick == true)
            {
                Debug.Log("Pick Flower...");
                currentFlower = collision.gameObject.GetComponent<FlowerScript>();
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Flor" && collision.gameObject.GetComponent<FlowerScript>())
        {
           
                currentFlower = null;
            
        }
    }
    void UseFlower()
    {
      
            switch (CurrentPower)
            {

                case Power.Clay:
                Debug.Log("Fire!");
                bull.FireShot(0f, 1200f, projList[0]);
                break;

                case Power.Aster:
                Debug.Log("Fire!");
                    bull.FireShot(0f,900f, projList[1]);

                    break;
                case Power.Rose:
                 Debug.Log("Fire!");
                 bull.FireShot(0f, 1000f, projList[2]);

                break;
        }
        
        canFire = false;
        CurrentPower = Power.Clay;
  
    }



















    /*
     * 
     * 
     *  void SideMover()
    {
        RaycastHit2D AgainstCheck = Physics2D.Raycast(gameObject.transform.position, gameObject.transform.right * xInput, 1.3f, groudLayers);
        float BackPedal = 1f;
        if(hit4.collider != null && JumpHeld == false)
        {
            BackPedal = 3f;


        }
        if (canSwordAttack == true && canBlockAttack == true || (canBlockAttack == false && canShoot == false && canSwordAttack == true))
        {
          
            if (AgainstCheck.collider == false)
            {
                if (xInput != 0)
                {
                    Anim.SetBool("Moving", true);
                }
                else
                {
                    Anim.SetBool("Moving", false);
                }

                if (alive == true)
                {


                    //rb.AddForce(Vector2.right * 200f * xInput);

                    //smoothyInputVelocity = Vector2.SmoothDamp(smoothyInputVelocity, inputss, ref smoothrefInputVelocity, smoothTime);
                    //rb.velocity = new Vector2(smoothyInputVelocity.x * (moveSpeed / 2), rb.velocity.y);
                  
                    
                        float targetSpeed = xInput * moveSpeed;
                        float difSpeed = targetSpeed - rb.velocity.x;
                        float accel = (Mathf.Abs(targetSpeed) > 0.1f) ? 25f : BackPedal;
                        //Debug.Log(accel);
                        float movement = Mathf.Pow(Mathf.Abs(difSpeed) * accel, 1f) * Mathf.Sign(difSpeed);
                        rb.AddForce(movement * Vector2.right);
                    


                }
               

            }

        }
        else if (canBlockAttack == false && canSwordAttack == false)
        {

            if (AgainstCheck.collider == false)
            {
                if (xInput != 0)
                {
                    Anim.SetBool("Moving", true);
                }
                else
                {
                    Anim.SetBool("Moving", false);
                }

                if (alive == true)
                {

                    //rb.AddForce(Vector2.right * 200f * xInput);

                    //smoothyInputVelocity = Vector2.SmoothDamp(smoothyInputVelocity, inputss, ref smoothrefInputVelocity, smoothTime);

                    //rb.velocity = new Vector2(smoothyInputVelocity.x * (moveSpeed / 2), rb.velocity.y);
                    
                        float targetSpeed = xInput * moveSpeed;
                        float difSpeed = targetSpeed - rb.velocity.x;
                        float accel = (Mathf.Abs(targetSpeed) > 0.1f) ? 25f : BackPedal;
                        float movement = Mathf.Pow(Mathf.Abs(difSpeed) * accel, 1f) * Mathf.Sign(difSpeed);
                        rb.AddForce(movement * Vector2.right);
                    
                }


            }

        }




    }
     */
}
