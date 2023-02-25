using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChanger : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if(!collision.gameObject.GetComponent<ColorChanger>())
        {
            collision.gameObject.GetComponent<MeshRenderer>().material.color = gameObject.GetComponent<MeshRenderer>().material.color;
        }
    }
}
