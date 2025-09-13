using System.Security.Cryptography;
using UnityEngine;

public class GhostScript : MonoBehaviour
{
    [SerializeField] public GameObject Ghost;

    void Start()
    {
        Ghost.transform.position = new Vector3(14.67f, -1.649f, -6.1f);
        Ghost.transform.rotation = Quaternion.Euler(0f, 124.4f, 0f);
    }

    void Update()
    {
        return;
    }
}
