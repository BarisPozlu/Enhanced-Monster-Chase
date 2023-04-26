using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dash : MonoBehaviour
{

    private Rigidbody2D playerBody;
    private Coroutine routine;

    private bool pressedK = false;
    public static bool dashing = false;
    public static bool canDash = true;

    private float dashSpeed = 25;

    // Start is called before the first frame update
    void Start()
    {
        playerBody = GetComponent<Rigidbody2D>();
        DashPlayer();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            pressedK = true;
        }
    }

    private void DashPlayer()
    {
        routine = StartCoroutine(DashRoutine());
    }

    private IEnumerator DashRoutine()
    {
        while (true)
        {
            canDash = true;
            yield return new WaitUntil(() => pressedK);

            dashing = true;
            canDash = false;
            AddDashSpeed(dashSpeed);

            yield return new WaitForSeconds(0.2f);

            dashing = false;
            AddDashSpeed(0);

            yield return new WaitForSeconds(5);

            pressedK = false;
        }
    }

    private void AddDashSpeed(float dashSpeed)
    {
        if (!Player.facingRight)
        {
            dashSpeed = -1 * dashSpeed;
        }
        playerBody.velocity = new Vector2(dashSpeed, playerBody.velocity.y);
    }

    private void OnDisable()
    {
        StopCoroutine(routine);
        Player.facingRight = true;
        dashing = false;
    }
}
