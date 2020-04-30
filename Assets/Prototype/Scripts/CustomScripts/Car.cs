using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : Receiver
{
    public bool looping = false;
    public float speed = 12f;
    public List<GameObject> targets;
    int index = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (activated)
        {
            Vector3 targetPos = targets[index].transform.position;
            //targetPos.y = transform.position.y;
            Vector3 delta = targetPos - transform.position;
            if (delta.magnitude < 2.5)
            {
                index++;
                if (index >= targets.Count)
                {
                    if (!looping)
                    {
                        Deactivate();
                    }
                    else
                    {
                        index = 0;
                    }
                }
            }
            Quaternion targetRot = Quaternion.LookRotation(delta);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, Time.deltaTime * 5);
            //transform.position = Vector3.Lerp(transform.position, targetPos, Time.deltaTime * speed);
            transform.position = transform.position + Vector3.Normalize(delta) * speed * Time.deltaTime;
        }
    }
}
