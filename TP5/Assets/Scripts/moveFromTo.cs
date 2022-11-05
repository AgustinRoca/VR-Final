using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveFromTo : MonoBehaviour
{
    // Adjust the speed for the application.
    public float speed = 1.0f;

    // The target (cylinder) position.
    //private Transform target;
    [SerializeField] private float x, y, z;
    [SerializeField] private float x1, y1, z1;
    Vector3 target;

    void Awake()
    {
        target = new Vector3(x, y, z);

        // Position the cube at the origin.
        transform.position = new Vector3(x1, y1,z1);
    }

    void Update()
    {
        // Move our position a step closer to the target.
        float step = speed * Time.deltaTime; // calculate distance to move
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(x,y,z), step);

        // Check if the position of the cube and sphere are approximately equal.
        if (Vector3.Distance(transform.position, target) < 0.001f)
        {
            // Swap the position of the cylinder.
            target *= -1.0f;
            Destroy(gameObject);
        }
    }
    public void setFrom(float x, float y, float z)
    {
        this.x1 = x;
        this.y1 = y;
        this.z1 = z;
        transform.position = new Vector3(x1, y1, z1);
    }
    public void setTo(float x, float y, float z)
    {
        this.x = x;
        this.y = y;
        this.z = z;
        target = new Vector3(x, y, z);

    }
}
