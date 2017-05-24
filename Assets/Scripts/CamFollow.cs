using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Camera))]
public class CamFollow : MonoBehaviour {

    public GameObject target;
    public float lerpfactor = 0.1f;
    public Vector3 Offset = new Vector3(0, 0.7f, 0);

    public bool basic = true;
    Vector3 newPosition;
	
	void LateUpdate () {
        newPosition = transform.position - Offset;

	    if (basic) {
            newPosition = new Vector3(target.transform.position.x, target.transform.position.y, transform.position.z);
        }
        else {
            newPosition = new Vector3(Mathf.Lerp(newPosition.x, target.transform.position.x, lerpfactor), Mathf.Lerp(newPosition.y, target.transform.position.y, lerpfactor), transform.position.z);
        }

        transform.position = newPosition + Offset;
    }
}
