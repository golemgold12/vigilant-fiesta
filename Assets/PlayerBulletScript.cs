using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletScript : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] GameObject GunBarrel;
    [SerializeField] PlayerScript plr;
    [SerializeField] GameObject BulletContainer;
    float xFudge = 10f, yFudge = 10f;
    bool canFire = true;
    public void FireShot(float xForce = 0f, float yForce = 30f, GameObject bullet = null)
    {
        StartCoroutine(FireShotThing(xForce, yForce, bullet, .1f));

    }

    IEnumerator FireShotThing(float xForce = 0f, float yForce = 30f, GameObject bullet = null, float waitTime = .1f)
    {
        yield return new WaitForSeconds(waitTime);

        if (bullet != null && BulletContainer != null)
        {
            GameObject newBullet = Instantiate(bullet, BulletContainer.transform);
            newBullet.transform.position = GunBarrel.transform.position;
            Rigidbody2D bulletRb = newBullet.GetComponent<Rigidbody2D>();
            if (bulletRb)
            {
                bulletRb.velocity = Vector2.zero;
                bulletRb.AddForce(GunBarrel.transform.up * yForce * bulletRb.mass);

            }

            Destroy(newBullet, 5f);

        }

        yield return null;
    }
}
