using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CuttingToolBehaviour : MonoBehaviour
{
    
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

    IEnumerator CutCoroutine() 
    {
        //If it is the scissor just play the animation
        if(anim != null) {
            anim.SetTrigger("TriggerChopAnimation");

            while(anim.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f)
                yield return new WaitForEndOfFrame();
        }

        yield return new WaitForEndOfFrame();

        //No matter what tool it is, execute the ribbon cut
        RibbonCut();
    }

    private void RibbonCut() {
        //TODO: Handle edge cases
        if(collidedObjects.Count > 0) { 
            //Get middle object of all valid objects
            GameObject obj = collidedObjects[Mathf.FloorToInt(collidedObjects.Count / 2)];
            
            // Update the cut character joint, which also leads to a visual update due to RopePartVisualControl.cs
            obj.GetComponent<CharacterJoint>().connectedBody = obj.transform.parent.GetChild(obj.transform.GetSiblingIndex() + 1).GetComponent<Rigidbody>();

            //Make sure to only fire the particle effects once
            if(GlobalStateController.firstCut) {
                GlobalStateController.firstCut = false; 
                CutEvent.Invoke();
            } 
        }
    }

    void OnTriggerEnter(Collider other) {
        if(other.GetComponent<CharacterJoint>() != null) {
            collidedObjects.Add(other.transform.gameObject);

            StartCoroutine("CutCoroutine");
        }
    }

    void OnTriggerExit(Collider other) {
        if(collidedObjects.Contains(other.transform.gameObject)) collidedObjects.Remove(other.transform.gameObject);
    }
}
