using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopePartVisualControl : MonoBehaviour
{

    private CharacterJoint joint;
    private LineRenderer line;

    private Vector3[] points;

    // Start is called before the first frame update
    void Awake()
    {
        joint = GetComponent<CharacterJoint>();
        line = GetComponent<LineRenderer>();

        points = new Vector3[2];

        UpdateLineRenderer();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateLineRenderer();
    }

    private void UpdateLineRenderer() {
        if(joint != null && line != null) {
            points[0] = this.transform.position;
            if(joint.connectedBody != null) points[1] = joint.connectedBody.transform.position;
            else points[1] = this.transform.position;
        
            line.positionCount = 2;
            line.SetPositions(points);
        }
    }
}
