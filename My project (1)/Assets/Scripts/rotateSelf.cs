using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotateSelf : MonoBehaviour
{
    [SerializeField]
    private float tireSpeed;

    private void FixedUpdate()
    {
        transform.rotation = Quaternion.Euler(transform.rotation.x + (tireSpeed * Time.fixedDeltaTime), transform.rotation.y, transform.rotation.z);
    }
}
