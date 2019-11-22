using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplauseController : MonoBehaviour
{   

    [SerializeField]
    private int applauseLoopCount;
    private AudioSource aus;

    void Awake() {
        aus = transform.GetComponent<AudioSource>();
    }

    public void OnCutEventApplauseBehaviour() {
        StartCoroutine("ApplauseBehaviour");
    }

    private IEnumerator ApplauseBehaviour() {
        aus.Play();
        yield return new WaitForSeconds(aus.clip.length * applauseLoopCount);
        aus.Stop();
    }
}
