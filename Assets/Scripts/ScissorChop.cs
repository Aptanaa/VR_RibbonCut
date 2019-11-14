using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScissorChop : MonoBehaviour
{

    private Animator anim;

    private List<GameObject> collidedObjects;

    // Start is called before the first frame update
    void Start()
    {
        collidedObjects = new List<GameObject>();
        anim = GetComponent<Animator>();
    }

    //Gets started from ControllerGrab.cs
    IEnumerator OnCompleteChopAnimation() 
    {
        while(anim.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f)
            yield return null;


        //TODO: Handle edge cases
        if(collidedObjects.Count > 0) { 
            GameObject obj = collidedObjects[Mathf.FloorToInt(collidedObjects.Count / 2)];
            obj.GetComponent<CharacterJoint>().connectedBody = obj.transform.parent.GetChild(obj.transform.GetSiblingIndex() + 1).GetComponent<Rigidbody>();
        }
    }

    void OnTriggerEnter(Collider other) {
        if(other.GetComponent<CharacterJoint>() != null) collidedObjects.Add(other.transform.gameObject);
    }

    void OnTriggerExit(Collider other) {
        if(collidedObjects.Contains(other.transform.gameObject)) collidedObjects.Remove(other.transform.gameObject);
    }
}
