using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMechanics : MonoBehaviour
{
    [SerializeField] GameObject Bullet;
    [SerializeField] float Life = 100f;
    [SerializeField] float BulletSpeed = 7f;
    [SerializeField] float RateOfFire = 0.6f;
    [SerializeField] AudioClip FireSound;
    [SerializeField] AudioClip DeadSound;

    private int ScoreValue = 200;
    private UIManager uiManager;

    // Start is called before the first frame update
    void Start()
    {
        uiManager = GameObject.Find("UI Manager").GetComponent<UIManager>();
    }

    // Update is called once per frame
    void Update()
    {
        float possibility = RateOfFire * Time.deltaTime;
        if(Random.value < possibility)
        {
            Fire();
        }
    }

    private void Fire()
    {
        GameObject bulletObject = Instantiate(Bullet, transform.position + new Vector3(0, -0.3f, 0), Quaternion.identity) as GameObject;
        bulletObject.GetComponent<Rigidbody2D>().velocity = new Vector3(0, -BulletSpeed, 0);
        AudioSource.PlayClipAtPoint(FireSound, transform.position);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        BulletMechanics collisionBullet = collision.gameObject.GetComponent<BulletMechanics>();

        if (collisionBullet)
        {
            collisionBullet.OnDestroy();
            Life -= collisionBullet.GetDamage();

            if (Life <= 0)
            {
                Destroy(gameObject);
                uiManager.AddScore(ScoreValue);
                AudioSource.PlayClipAtPoint(DeadSound, transform.position);
            }
        }
    }
}
