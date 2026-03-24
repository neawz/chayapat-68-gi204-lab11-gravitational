using System.Collections.Generic;
using UnityEngine;

public class Gravity : MonoBehaviour
{
    Rigidbody rb;
    const float g = 0.00674f; // Gravitational Constant 6.674
    public static List<Gravity> otherObjectsList = new List<Gravity>();

    [SerializeField] bool planet = false;
    [SerializeField] int orbitSpeed = 1000;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        if (otherObjectsList == null)
        {
            otherObjectsList = new List<Gravity>();
        }
        // Add 'THIS' Object to list
        otherObjectsList.Add(this);

        if (!planet)
        {
            rb.AddForce(Vector3.left * orbitSpeed);
        }
    }

    private void FixedUpdate()
    {
        foreach (var obj in otherObjectsList)
        {
            if (obj != this)
            {
                Attract(obj);
            }
        }
    }

    void Attract(Gravity other)
    {
        Rigidbody otherRb = other.rb;
        
        // Get Direction between 2 Obj
        Vector3 direction = rb.position - otherRb.position;

        // Get Distance between 2 Obj
        float distance = direction.magnitude;

        if (distance == 0f) return;

        // F = g * (m1 * m2) / r * r
        float forceMagnitude = g * (rb.mass * otherRb.mass) / Mathf.Pow(distance, 2);

        Vector3 gForce = forceMagnitude * direction.normalized;

        otherRb.AddForce(gForce);
    }
}
