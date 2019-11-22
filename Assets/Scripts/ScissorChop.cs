﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ScissorChop : MonoBehaviour
{

    private bool firstCut = true;
    
    [SerializeField]
    private UnityEvent CutEvent;

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
        anim.SetTrigger("TriggerChopAnimation");

        while(anim.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f)
            yield return new WaitForEndOfFrame();


        //TODO: Handle edge cases
        if(collidedObjects.Count > 0) { 
            GameObject obj = collidedObjects[Mathf.FloorToInt(collidedObjects.Count / 2)];
            obj.GetComponent<CharacterJoint>().connectedBody = obj.transform.parent.GetChild(obj.transform.GetSiblingIndex() + 1).GetComponent<Rigidbody>();

            if(firstCut) {
                firstCut = false; 
                CutEvent.Invoke();
            } 
        }
    }

    void OnTriggerEnter(Collider other) {
        if(other.GetComponent<CharacterJoint>() != null) collidedObjects.Add(other.transform.gameObject);
    }

    void OnTriggerExit(Collider other) {
        if(collidedObjects.Contains(other.transform.gameObject)) collidedObjects.Remove(other.transform.gameObject);
    }
}
