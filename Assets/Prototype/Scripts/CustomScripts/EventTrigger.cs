using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventTrigger : MonoBehaviour
{
    public List<GameObject> receiverObjs;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            foreach (GameObject receiverObj in receiverObjs)
            {
                Receiver receiver = receiverObj.GetComponent<Receiver>();
                if (receiver != null)
                {
                    if (!receiver.activated)
                    {
                        receiver.Activate();
                    } else
                    {
                        receiver.Deactivate();
                    }
                    
                }
            }
            
        }
    }
}
