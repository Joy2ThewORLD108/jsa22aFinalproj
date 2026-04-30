
using System.Collections;
using UnityEngine;

public class SkyDropper : MonoBehaviour
{
    [Header("Drop Settings")]
    [Tooltip("The prefab you want to drop from the sky.")]
    public GameObject objectToDrop;

    [Tooltip("Time in seconds between each drop.")]
    public float dropRate = 0.5f;

    [Header("Spawn Area Settings")]
    [Tooltip("How high up the objects should spawn.")]
    public float spawnHeight = 20f;

    [Tooltip("How wide the random spawn area is on the X axis.")]
    public float areaWidth = 10f;

    [Tooltip("How deep the random spawn area is on the Z axis.")]
    public float areaDepth = 10f;

    [Header("Cleanup")]
    [Tooltip("How long (in seconds) before the dropped object is destroyed to prevent lag.")]
    public float lifeSpan = 10f;

    void Start()
    {
        if (objectToDrop == null)
        {
            Debug.LogError("SkyDropper: No object assigned to drop! Please assign a prefab in the inspector.");
            return;
        }

        // Start the continuous dropping loop
        StartCoroutine(DropRoutine());
    }

    IEnumerator DropRoutine()
    {
        // This loop will run endlessly while the script is active
        while (true)
        {
            // 1. Calculate a random position in the sky
            float randomX = Random.Range(-areaWidth / 2f, areaWidth / 2f);
            float randomZ = Random.Range(-areaDepth / 2f, areaDepth / 2f);

            // Apply the random offsets to the spawner's current position
            Vector3 spawnPosition = new Vector3(
                transform.position.x + randomX,
                spawnHeight,
                transform.position.z + randomZ
            );

            // 2. Spawn the object
            GameObject droppedItem = Instantiate(objectToDrop, spawnPosition, Quaternion.identity);

            // 3. Destroy the object after a set time so your game doesn't lag
            Destroy(droppedItem, lifeSpan);

            // 4. Wait for the specified amount of time before dropping the next one
            yield return new WaitForSeconds(dropRate);
        }
    }

    // This draws a helpful visual box in the Unity Editor so you can see where objects will spawn
    void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(0, 1, 0, 0.3f); // Semi-transparent green
        Vector3 center = new Vector3(transform.position.x, spawnHeight, transform.position.z);
        Vector3 size = new Vector3(areaWidth, 1f, areaDepth);
        Gizmos.DrawCube(center, size);
    }
}