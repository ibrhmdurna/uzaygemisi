using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    [SerializeField] GameObject Enemy;
    [SerializeField] float Width;
    [SerializeField] float Height;
    [SerializeField] float Speed = 5f;
    [SerializeField] float CreateDelayTime = 0.7f;

    private bool movement = true;
    private float xMin, xMax;
    private float padding = 1f;

    // Start is called before the first frame update
    void Start()
    {
        float distance = transform.position.z - Camera.main.transform.position.z;
        Vector3 leftPoint = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distance));
        Vector3 rightPoint = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, distance));
        xMin = leftPoint.x;
        xMax = rightPoint.x;

        CreateOneByOneEnemy();
    }

    // Update is called once per frame
    void Update()
    {
        if (movement)
        {
            transform.position += Vector3.right * Speed * Time.deltaTime;
        }
        else
        {
            transform.position += Vector3.left * Speed * Time.deltaTime;
        }

        float rightLimit = transform.position.x + Width / 2;
        float leftLimit = transform.position.x - Width / 2;

        if (rightLimit > xMax)
            movement = false;
        else if(leftLimit < xMin)
            movement = true;

        if (IsAllEnemiesDead())
        {
            CreateOneByOneEnemy();
        }
    }

    private void CreateOneByOneEnemy()
    {
        Transform availablePosition = NextPosition();

        if (availablePosition)
        {
            GameObject EnemyObject = Instantiate(Enemy, availablePosition.transform.position, Quaternion.identity) as GameObject;
            EnemyObject.transform.parent = availablePosition;
        }

        if (NextPosition())
        {
            Invoke("CreateOneByOneEnemy", CreateDelayTime);
        }
    }

    private Transform NextPosition()
    {
        foreach (Transform ChildPosition in transform)
        {
            if (ChildPosition.childCount == 0)
                return ChildPosition;
        }

        return null;
    }

    private bool IsAllEnemiesDead()
    {
        foreach (Transform ChildPosition in transform)
        {
            if (ChildPosition.childCount > 0)
                return false;
        }

        return true;
    }

    private void CreateEnemies()
    {
        foreach (Transform child in transform)
        {
            GameObject EnemyObject = Instantiate(Enemy, child.position, Quaternion.identity) as GameObject;
            EnemyObject.transform.parent = child;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(Width, Height));
    }
}
