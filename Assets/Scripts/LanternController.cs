using System.Linq;
using UnityEngine;

public class LanternController : MonoBehaviour
{
    public float emissionAmplifier;
    Color originalEmissionColor;
    Material lightMaterial;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Raycast downwards to find ground.
        // If ground is found, move there. Otherwise, delete self.
        if (Physics.Raycast(transform.position, Vector2.down, out RaycastHit hit, 100f))
        {
            transform.position = new Vector3(transform.position.x, hit.point.y, transform.position.z);
        }
        else
        {
            Destroy(gameObject);
        }

        Renderer renderer = GetComponent<Renderer>();
        if (renderer.materials.Length >= 1)
        {
            lightMaterial = renderer.materials[1];
            originalEmissionColor = lightMaterial.GetColor("_EmissionColor");
        }
    }

    void OnTriggerStay(Collider other)
    {
        // subtract distance from max distance
        float distance = 8 - Vector3.Distance(transform.position, other.transform.position);
        // modify intensity by distance and amplifier
        lightMaterial.SetColor("_EmissionColor", originalEmissionColor * emissionAmplifier * distance);
    }
}
