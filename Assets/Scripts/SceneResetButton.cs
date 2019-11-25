using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneResetButton : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "Controller") {
            GlobalStateController.firstCut = true;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    } 
}
