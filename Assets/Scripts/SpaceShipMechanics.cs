using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShipMechanics : MonoBehaviour
{
    [SerializeField] float Life = 400f;
    [SerializeField] float Speed = 1f;
    [SerializeField] GameObject Bullet;
    [SerializeField] float BulletSpeed = 9f;
    [SerializeField] AudioClip FireSound;
    [SerializeField] AudioClip DeadSound;

    private float xMin, xMax;
    private float padding = 1f;
    private UIManager uiManager;

    // Start is called before the first frame update
    void Start()
    {
        float distance = transform.position.z - Camera.main.transform.position.z;
        Vector3 leftPoint = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distance));
        Vector3 rightPoint = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, distance));
        xMin = leftPoint.x + padding;
        xMax = rightPoint.x - padding;

        uiManager = GameObject.Find("UI Manager").GetComponent<UIManager>();
    }

    // Update is called once per frame
    void Update()
    {
        ShipEntries();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            InvokeRepeating("Fire", 0.000001f, 0.2f);
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            CancelInvoke("Fire");
        }
    }

    void ShipEntries()
    {
        float newX = Mathf.Clamp(transform.position.x, xMin, xMax);
        transform.position = new Vector3(newX, transform.position.y, transform.position.z);

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            //transform.position += new Vector3(-Speed * Time.deltaTime, 0f, 0f);

            transform.position += Vector3.left * Speed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            //transform.position += new Vector3(Speed * Time.deltaTime, 0f, 0f);

            transform.position += Vector3.right * Speed * Time.deltaTime;
        }
    }

    void Fire()
    {
        GameObject bulletObject = Instantiate(Bullet, transform.position + new Vector3(0, 0.4f, 0), Quaternion.identity) as GameObject;
        bulletObject.GetComponent<Rigidbody2D>().velocity = new Vector3(0, BulletSpeed, 0);
        AudioSource.PlayClipAtPoint(FireSound, transform.position);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        BulletMechanics collisionBullet = collision.gameObject.GetComponent<BulletMechanics>();

        if (collisionBullet)
        {
            collisionBullet.OnDestroy();
            Life -= collisionBullet.GetDamage();
            uiManager.ChangedValue(Life);

            if (Life <= 0)
            {
                Destroy(gameObject);
                AudioSource.PlayClipAtPoint(DeadSound, transform.position);
            }
        }
    }
}
