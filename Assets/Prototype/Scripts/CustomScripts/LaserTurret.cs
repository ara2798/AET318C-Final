using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserTurret : Receiver
{
    public List<GameObject> barrels;
    public GameObject laserPrefab;
    private Transform target;
    public float downOffset = 0.75f;
    public float forwardOffset = 7.5f;
    float interval = 1.2f;
    float timer;
    Quaternion laserRot;
    
    // Start is called before the first frame update
    void Start()
    {
        timer = 0.2f;
        target = GameObject.FindGameObjectWithTag("Player").transform;
        Debug.Log(transform.forward);
        if (Mathf.Approximately(Mathf.Abs(transform.forward.z), 1f))
        {
            Debug.Log("Twisted on the x axis");
            laserRot = Quaternion.Euler(90, 0, 0);
        }
        else
        {
            Debug.Log("Twisted on the z axis");
            laserRot = Quaternion.Euler(0, 0, 90);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (activated)
        {
            if (timer <= 0f)
            {
                timer = interval;
                Fire();
            } else
            {
                timer -= Time.deltaTime;
            }
        }
    }

    private void Fire()
    {
        foreach (GameObject barrel in barrels)
        {
            Vector3 origin = barrel.transform.position;
            Vector3 destination = target.position - transform.forward * forwardOffset + Vector3.down * downOffset;
            GameObject laserObject = Instantiate(laserPrefab, origin, laserRot);
            Laser laser = laserObject.GetComponent<Laser>();
            laser.Shoot(destination - origin, 120);
        }
    }
}
