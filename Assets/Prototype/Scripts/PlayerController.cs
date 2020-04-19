using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 10;
    public GameObject followerPrefab;

    Rigidbody rb;
    Vector3 motionVector;
    public LayerMask layerMask = 0 << -1;

    List<GameObject> followers = new List<GameObject>();

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        layerMask &= ~(1 << gameObject.layer);

        InvokeRepeating("SpawnFollower", 5, 5);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 inputVector = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        transform.Translate(Vector3.forward * inputVector.z * speed * Time.deltaTime);

        // Ground stick
        RaycastHit hit;
        Quaternion slopeRotation = Quaternion.LookRotation(Vector3.ProjectOnPlane(transform.forward, Vector3.up).normalized, Vector3.up);
        if (Physics.Raycast(transform.position, -Vector3.up, out hit, 1, layerMask.value))
        {
            slopeRotation = Quaternion.LookRotation(Vector3.ProjectOnPlane(transform.forward, hit.normal).normalized, hit.normal);
        }
        rb.rotation = Quaternion.Slerp(rb.rotation, slopeRotation, Time.deltaTime * 10) * Quaternion.Euler(0, inputVector.x * Time.deltaTime * speed * 20, 0);

        // View based motion
        //motionVector = Camera.main.transform.TransformVector(inputVector);
        //motionVector = Vector3.ProjectOnPlane(motionVector, Vector3.up).normalized;
        //motionVector.y = 0;
        //if (inputVector.magnitude > 0.1f)
        //{
        //    transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(motionVector), Time.deltaTime * 5);
        //}
        //rb.velocity = Vector3.Lerp(rb.velocity, motionVector * speed, Time.deltaTime * 5);

        //// Local motion
        
        //Vector3 velocity = Vector3.ProjectOnPlane(transform.forward, Vector3.up).normalized * inputVector.z * speed;
        //velocity.y = rb.velocity.y;
        //rb.velocity = Vector3.Lerp(rb.velocity, velocity, Time.deltaTime * 10);

        rb.AddForce(Physics.gravity * 2, ForceMode.Force);

        if (Input.GetButtonDown("Fire1"))
        {
            followers.RemoveAll(x => x == null);

            foreach(GameObject g in followers)
            {
                g.GetComponent<Follower>().Reverse();
            }
        }
    }

    void SpawnFollower()
    {
        GameObject g = Instantiate(followerPrefab, transform.position, transform.rotation);
        g.transform.Rotate(Vector3.up * Random.Range(0, 360));
        g.transform.Translate(Vector3.forward * Random.Range(7, 12));
        followers.Add(g);
    }

}