using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    private float speed;
    private Rigidbody2D monsterBody;
    private bool allowPassing;

    // Start is called before the first frame update
    void Start()
    {
        speed = Random.Range(4, 10);
        monsterBody = GetComponent<Rigidbody2D>();
        allowPassing = transform.position.x >= 103;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        HandleMonsterMovement();
        Move();
    }

    private void Move()
    {
        monsterBody.velocity = new Vector2(speed, monsterBody.velocity.y);
    }

    private void Rotate180()
    {
        transform.Rotate(Vector3.up, 180);
    }

    private void HandleMonsterMovement()
    {
        if (transform.position.x >= 103 && monsterBody.velocity.x > 0 && allowPassing)
        {
            allowPassing = false;
            Rotate180();
            speed = -1 * speed;
        }
    }
}