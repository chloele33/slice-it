using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(Rigidbody))]
public class Slice : MonoBehaviour
{
    public Material capMaterial;

    private void OnCollisionEnter(Collision collision)
    {
        GameObject victim = collision.collider.gameObject;
        GameObject[] pieces = BLINDED_AM_ME.MeshCut.Cut(victim, transform.position, transform.right, capMaterial);

        if (!pieces[1].GetComponent<Rigidbody>())
        {
            pieces[1].AddComponent<Rigidbody>();
        }

        Destroy(pieces[1], 1);
    }
}
