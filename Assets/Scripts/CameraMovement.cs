using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private GameObject player;
    private Vector3 tempPosition;

    [SerializeField]
    private float maxX, minX;

    // Start is called before the first frame update
    private void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    private void Update()
    {

    }

    private void LateUpdate()
    {
        if (player == null) return;

        tempPosition = transform.position;
        tempPosition.x = player.transform.position.x;

        if (tempPosition.x > maxX)
        {
            tempPosition.x = maxX;
        }

        else if (tempPosition.x < minX)
        {
            tempPosition.x = minX;
        }

        transform.position = tempPosition;
    }
}