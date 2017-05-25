using UnityEngine;
using System.Collections;

public class TopdownMouselook : MonoBehaviour {

    void Update() {
        Rotate();
    }

    void Rotate() {
        //http://answers.unity3d.com/questions/585035/lookat-2d-equivalent-.html

        Vector3 diff = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        diff.Normalize();

        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90f);
    }
}
