using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateManager : MonoBehaviour
{

    private List <GameObject> currentCollisions = new List <GameObject> ();

    void OnTriggerEnter (Collider col) {
        // Add the GameObject collided with to the list.
        if(col.gameObject.name.Contains("blue donut")){
            //print("donut");
        }
        currentCollisions.Add (col.gameObject);
    }
 
    void OnTriggerExit (Collider col) {
        // Remove the GameObject collided with from the list.
        currentCollisions.Remove (col.gameObject);
    }

    public List<GameObject> objectsOnPlate(){
        return currentCollisions;
    }

}
