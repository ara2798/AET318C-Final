using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayTurret : Receiver
{
    public GameObject rayOriginObj;
    LineRenderer lineRenderer;
    Vector3 origin;
    float counter = 0f;
    float step = 0.2f;
    float maxDistance = 40f;
    
    // Start is called before the first frame update
    void Start()
    {
        origin = rayOriginObj.transform.position;
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.startWidth = 0.25f;
        lineRenderer.endWidth = 0.25f;
        lineRenderer.SetPosition(0, origin);
    }

    // Update is called once per frame
    void Update()
    {
        if (activated)
        {
            if (counter <= maxDistance)
            {
                counter += step;
            }
            Vector3 point = origin + rayOriginObj.transform.forward * counter;;
            lineRenderer.SetPosition(1, point);
            RaycastHit hit;
            if (Physics.Raycast(origin, point - origin,out hit, maxDistance,LayerMask.GetMask("Player")))
            {
                
                Player player = hit.transform.gameObject.GetComponent<Player>();
                player.changeHealth(-1);
            }
        } else
        {
            counter = 0f;
            lineRenderer.SetPosition(1, origin);
        }
    }
}
