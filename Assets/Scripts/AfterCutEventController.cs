using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AfterCutEventController : MonoBehaviour
{
    public void OnCutEventParticleBehaviour() {
        StartCoroutine("ParticleBehaviour");
    }

    private IEnumerator ParticleBehaviour() {
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform tmp = transform.GetChild(i);
            AudioSource aus = tmp.GetComponent<AudioSource>();
            ParticleSystem ps = tmp.GetComponent<ParticleSystem>();
            
            ps.Play();
            aus.Play();
            yield return new WaitForSeconds(GlobalStateController.particleDelay);
            aus.Stop();
            ps.Stop();
        }

        // yield return new WaitForSeconds(4f);

        // for (int j = 0; j < transform.childCount; j++)
        // {
        //     transform.GetChild(j).GetComponent<ParticleSystem>().Stop();
        // }
    }

    public void OnCutEventBallonBehaviour() {
        StartCoroutine("BallonBehaviour");
    }

    private IEnumerator BallonBehaviour() {

        for (int j = 0; j < transform.childCount; j++)
        {
            transform.GetChild(j).GetComponent<Rigidbody>().AddForce(new Vector3(Random.Range(GlobalStateController.ballonRandomRange.x, GlobalStateController.ballonRandomRange.y), GlobalStateController.ballonAscendingRate, Random.Range(GlobalStateController.ballonRandomRange.x, GlobalStateController.ballonRandomRange.y)));
            yield return new WaitForSeconds(GlobalStateController.particleDelay);
        }
        yield return new WaitForFixedUpdate();
    }
}
