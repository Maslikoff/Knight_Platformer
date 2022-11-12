using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField] private GameObject bullet;
    [SerializeField] private float fireForse;
    public Transform firePoint;

    private PlayerMovement PlayerContr;

    private void Start()
    {
        PlayerContr = gameObject.GetComponent<PlayerMovement>();
    }

    public void Shoot(float direction)
    {
        float ange = PlayerContr.isRight ? 0f : 180f;
        GameObject currentBullet = Instantiate(bullet, firePoint.position, Quaternion.Euler(new Vector3(0f, 0f, ange)));
        Rigidbody2D currentBulletVelocity = currentBullet.GetComponent<Rigidbody2D>();

        if (direction >= 0 && PlayerContr.isRight)
            currentBulletVelocity.velocity = new Vector2(fireForse * 1, currentBulletVelocity.velocity.y);
        else
            currentBulletVelocity.velocity = new Vector2(fireForse * -1, currentBulletVelocity.velocity.y);
    }
}
