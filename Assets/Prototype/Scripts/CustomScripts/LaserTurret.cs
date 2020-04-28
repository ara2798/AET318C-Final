using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserTurret : Receiver
{
    public List<GameObject> barrels;
    public GameObject laserPrefab;
    private Transform target;
    float interval = 1.2f;
    float timer;
    // Start is called before the first frame update
    void Start()
    {
        timer = 0f;
        target = GameObject.FindGameObjectWithTag("Player").transform;
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
            Vector3 destination = target.position - transform.forward * 7.5f + Vector3.down * 0.75f;
            Debug.Log(destination);
            GameObject laserObject = Instantiate(laserPrefab, origin, Quaternion.Euler(90,0,0));
            Laser laser = laserObject.GetComponent<Laser>();
            laser.Shoot(destination - origin, 120);
        }
    }
}
