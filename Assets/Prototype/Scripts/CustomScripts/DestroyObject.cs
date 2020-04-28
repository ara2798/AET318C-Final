using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObject : MonoBehaviour
{
    public List<GameObject> objectsToDestroy;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            foreach (GameObject objectToDestroy in objectsToDestroy)
            {
                Destroy(objectToDestroy);
            }
        }
    }
}
