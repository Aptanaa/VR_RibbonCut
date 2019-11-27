using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienCutEventController : MonoBehaviour
{
    public void StartClapping() {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).GetComponent<Animator>().SetTrigger("TriggerClapping");
        }
    }
}
