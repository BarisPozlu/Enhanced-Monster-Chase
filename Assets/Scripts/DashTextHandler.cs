using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DashTextHandler : MonoBehaviour
{
    [SerializeField] private Image dashText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Dash.canDash)
        {
            dashText.color = Color.green;
        }
        else
        {
            dashText.color = Color.red;
        }
    }
}
