using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AutoPlayer : MonoBehaviour
{
    public bool cursorVisible;
    public float resetHeight = -50;
    public List<GameObject> waypoints = new List<GameObject>();

    const float speed = 10;

    bool grounded;
    int index;
    Rigidbody rb;
    LayerMask layerMask = -1;

    void Start()
    {
        Time.fixedDeltaTime = 1 / 100f;
        rb = GetComponent<Rigidbody>();
        waypoints.RemoveAll(x => x == null);
        layerMask &= ~(1 << gameObject.layer);
        InvokeRepeating("Clock", 1, 1);
        Cursor.visible = cursorVisible;
    }

    void Update()
    {
        if (waypoints.Count > 0)
        {
            Vector3 v = waypoints[index].transform.position; v.y = transform.position.y;
            Quaternion r = Quaternion.LookRotation(v - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, r, Time.deltaTime * 5);
        }

        if (Input.GetButtonDown("Jump") && grounded)
        {
            rb.AddForce(Vector3.up * 10, ForceMode.VelocityChange);
        }
    }

    void FixedUpdate()
    {
        grounded = Physics.CheckSphere(transform.position - transform.up * 0.2f, 0.4f, layerMask.value);

        Vector3 velocity = transform.TransformDirection(Vector3.forward) * speed;
        velocity.y = rb.velocity.y;
        if (grounded) { rb.velocity = velocity; }
        rb.AddForce(Physics.gravity * 2, ForceMode.Force);
    }

    void OnTriggerEnter(Collider c)
    {
        if (c.gameObject == waypoints[index])
        {
            if (waypoints.Count > 1) Destroy(waypoints[index]); // Change to fix level loading
            waypoints.RemoveAt(0);
        }
    }

    void Clock()
    {
        if (transform.position.y < resetHeight) { SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); }
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(new Vector3(0, resetHeight, 0), new Vector3(50, 0, 50));
    }
}
