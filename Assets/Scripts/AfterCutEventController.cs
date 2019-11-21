using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AfterCutEventController : MonoBehaviour
{
    [SerializeField]
    private float ballonAscendingRate;

    public void OnCutEventParticleBehaviour() {
        StartCoroutine("ParticleBehaviour");
    }

    private IEnumerator ParticleBehaviour() {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).GetComponent<ParticleSystem>().Play();
        }

        yield return new WaitForSeconds(4f);

        for (int j = 0; j < transform.childCount; j++)
        {
            transform.GetChild(j).GetComponent<ParticleSystem>().Stop();
        }
    }

    public void OnCutEventBallonBehaviour() {
        StartCoroutine("BallonBehaviour");
    }

    private IEnumerator BallonBehaviour() {
        Vector3 addVector = new Vector3(0, ballonAscendingRate, 0);

        while(transform.GetChild(0).transform.position.y < 15) {
            for (int j = 0; j < transform.childCount; j++)
            {
                transform.GetChild(j).transform.position += addVector * Time.fixedDeltaTime;
            }
            yield return new WaitForFixedUpdate();
        }
    }
}
