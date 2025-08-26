// Moth Harper
// Controls the lamp behavior by manipulating the emission based on player proximity.

using System.Linq;
using UnityEngine;

public class LanternController : MonoBehaviour
{
    // the amount the light increases/decreases per unit of distance
    public float increaseSpeed;
    // the maximum intensity for the lamp to reach
    public float maxBrightness;

    // max distance the lantern detects the player at
    float maxDistance;
    // material of the lit part of the latern
    Material lightMaterial;
    // original color of the lit part of the latern
    Color originalEmissionColor;


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

        // cache the emission material 
        Renderer renderer = GetComponent<Renderer>();
        if (renderer.materials.Length >= 1)
        {
            lightMaterial = renderer.materials[1];
            originalEmissionColor = lightMaterial.GetColor("_EmissionColor");
        }

        // cache the max distance
        maxDistance = GetComponent<SphereCollider>().radius;
    }

    void OnTriggerStay(Collider other)
    {
        // find distance from max player
        float distance = Vector3.Distance(transform.position, other.transform.position);
        distance = Mathf.Clamp(distance, 0, maxDistance);
        // modify light intensity by distance and amplifiers
        lightMaterial.SetColor("_EmissionColor", originalEmissionColor * ((increaseSpeed * distance) + maxBrightness));
        print(lightMaterial.GetColor("_EmissionColor"));
    }
}
