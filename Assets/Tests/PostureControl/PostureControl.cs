using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PostureControl : MonoBehaviour
{
    private void Update()
    {
        RaycastHit tmp;
        Physics.Raycast(transform.position, Vector3.down, out tmp, 100f);

        var forward = Vector3.Cross(Vector3.Cross(tmp.normal, transform.forward), tmp.normal);

        // å¸Ç´ÇïœçXÇ∑ÇÈ
        transform.LookAt(forward, tmp.normal);

        if (Input.GetKeyDown(KeyCode.A))
        {
            Debug.Log(forward);
            Debug.Log(tmp.normal);
        }
    }
    private void OnDrawGizmos()
    {
        Debug.DrawRay(transform.position, Vector3.down, Color.red, 100f);
    }
}