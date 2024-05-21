using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PullingJump : MonoBehaviour
{
    private Rigidbody rb;
    private Vector3 clickPosition;
    private float jumpPower = 10;

   void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        // èdóÕÇÃê›íË
        Physics.gravity = new Vector3(0, -9.81f, 0);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            clickPosition = Input.mousePosition;
        }

        if (Input.GetMouseButtonUp(0))
        {
            Vector3 dist = clickPosition - Input.mousePosition;
            if(dist.sqrMagnitude == 0) { return; }
            rb.velocity = dist.normalized * jumpPower;
        }
    }
}
