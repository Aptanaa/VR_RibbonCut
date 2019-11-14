using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeSpawn : MonoBehaviour
{
    [SerializeField]
    GameObject partPrefab;

    [SerializeField]
    Transform startPoint, endPoint;

    [SerializeField]
    float heightDifference;

    [SerializeField]
    int numberOfParts;

    private LineRenderer line;

    private Transform[] parts;
    private Vector3[] pointsForLR;

    void Awake() {
        Spawn();
    }

    void Update() {
        for (int i = 0; i < numberOfParts; i++)
        {
            pointsForLR[i] = parts[i].position;
        }
        
        line.SetPositions(pointsForLR);
    }

    public void Spawn()
    {
        //Create Parent Object to hold rope parts
        GameObject parent = new GameObject();
        parent.transform.position = Vector3.zero;
        parent.transform.rotation = Quaternion.identity;
        parent.name = "RopeHolder";

        //Add Linerenderer
        line = parent.AddComponent(typeof(LineRenderer)) as LineRenderer;
        line.material = new Material(Shader.Find("Sprites/Default"));
        line.widthMultiplier = 0.2f;
        line.startColor = Color.red;
        line.endColor = Color.red;
        line.numCapVertices = 10;

        //Calculate artificial middlepoint
        Vector3 middlePoint = startPoint.position + (endPoint.position - startPoint.position) / 2;
        middlePoint += new Vector3(0, heightDifference, 0);

        //Init arrays
        parts = new Transform[numberOfParts];
        pointsForLR = new Vector3[numberOfParts];

        //TODO: Make sure that it spawns the last node on the end position instead of one before
        for(int x = 0; x < numberOfParts; x++)
        {
            Vector3 point = GetPointOnCurve(startPoint.position, middlePoint, endPoint.position, (1.0f / (float)numberOfParts) * x);

            GameObject part;
            part = Instantiate(partPrefab, point, Quaternion.identity, parent.transform);
            part.transform.eulerAngles = new Vector3(180, 0, 0);
            part.name = x.ToString();

            //Freeze X Rotation to prevent the colliders from "Falling over"
            part.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationX;

            parts[x] = part.transform;
            pointsForLR[x] = part.transform.position;

            //If it is the first object, freeze it in place
            if(x == 0)
            {
                Destroy(part.GetComponent<CharacterJoint>());
                part.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            }
            else
            {
                part.GetComponent<CharacterJoint>().connectedBody = parent.transform.GetChild(parent.transform.childCount - 2).GetComponent<Rigidbody>();
            }
        }

        //If it is the last object, freeze it in place
        parent.transform.GetChild(parent.transform.childCount - 1).GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;

        //Set initial information for the line renderer
        line.positionCount = numberOfParts;
        line.SetPositions(pointsForLR);
    }

    private Vector3 GetPointOnCurve (Vector3 start, Vector3 middle, Vector3 end, float t) {
		return Vector3.Lerp(Vector3.Lerp(start, middle, t), Vector3.Lerp(middle, end, t), t);
	}
}