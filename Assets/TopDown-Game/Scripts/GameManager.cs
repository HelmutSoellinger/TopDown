using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public List<GameObject> BagsToDestroy;
    public List<GameObject> StonesToDestroy;
    public List<GameObject> Stones2ToDestroy;

    public int bagDestroyCount = 4;
    public int stoneDestroyCount = 4;
    public int stone2DestroyCount = 2;

    // Das Spotlight Prefab, das nach unten leuchtet
    public GameObject spotlightPrefab;


    void Start()
    {
     

        ObjectCount(BagsToDestroy.Count, bagDestroyCount);
        ObjectCount(StonesToDestroy.Count, stoneDestroyCount);
        ObjectCount(Stones2ToDestroy.Count, stone2DestroyCount);

        ResetAndDisableBagColliders(BagsToDestroy, bagDestroyCount);
        DestroyObjects(StonesToDestroy, stoneDestroyCount);
        DestroyObjects(Stones2ToDestroy, stone2DestroyCount);
    }

    void ObjectCount(int objectCount, int destroyCount)
    {
        if (objectCount < destroyCount)
        {
            Debug.LogError("Nicht genügend GameObjects in der Liste für die gewünschte Anzahl an Zerstörungen.");
            return;
        }
    }

    public void ResetAndDisableBagColliders(List<GameObject> availableObjects, int disableCount)
    {
      
            // Reset all colliders to not be triggers and enabled
            foreach (GameObject obj in BagsToDestroy)
            {
                Collider collider = obj.GetComponent<Collider>();
                if (collider != null)
                {
                    collider.isTrigger = false; // Reset the collider to non-trigger
                    collider.enabled = true; // Make sure the collider is enabled
                }
            }

            for (int i = 0; i < disableCount; i++)
            {
                int randomIndex = Random.Range(0, availableObjects.Count);
                GameObject objToDisable = availableObjects[randomIndex];

                // Speichere die Weltposition des Objekts
                Vector3 worldPosition = objToDisable.transform.position;

                // Hole den Collider des Objekts
                Collider collider = objToDisable.GetComponent<Collider>();
                if (collider != null)
            {
                // Setze den Collider als Trigger, damit er keine Kollision mehr hat, aber Trigger-Events auslöst
                collider.isTrigger = true; // Make the collider a trigger (no physical collision)
            }

                // Deaktivieren des MeshRenderers, um das Objekt unsichtbar zu machen
                MeshRenderer meshRenderer = objToDisable.GetComponent<MeshRenderer>();
                if (meshRenderer != null)
            {
                meshRenderer.enabled = false; // Make the object invisible
            }

            // Erstelle das Spotlight etwas unterhalb der Position des Objekts
            if (spotlightPrefab != null)
            {
                Vector3 spotlightPosition = worldPosition - Vector3.up * 2; // Positioniere das Spotlight etwas tiefer
                Instantiate(spotlightPrefab, spotlightPosition, Quaternion.Euler(90, 0, 0)); // Spotlight nach unten gerichtet
            }

                // Entferne das Objekt aus der Liste, damit es nicht erneut bearbeitet wird
                availableObjects.RemoveAt(randomIndex);
            }
        }


    public void DestroyObjects(List<GameObject> availableObjects, int destroyCount)
    {
        for (int i = 0; i < destroyCount; i++)
        {
            int randomIndex = Random.Range(0, availableObjects.Count);
            GameObject objToDestroy = availableObjects[randomIndex];

            // Speichere die Weltposition des zerstörten Objekts
            Vector3 worldPosition = objToDestroy.transform.position;


            // Erstelle das Spotlight etwas unterhalb der Position des zerstörten Objekts
            if (spotlightPrefab != null)
            {
                Vector3 spotlightPosition = worldPosition - Vector3.up * 2; // Positioniere das Spotlight etwas tiefer
                Instantiate(spotlightPrefab, spotlightPosition, Quaternion.Euler(90, 0, 0)); // Spotlight nach unten gerichted
            }

            // Zerstöre das ausgewählte GameObject
            Destroy(objToDestroy);

            availableObjects.RemoveAt(randomIndex);
        }
    }

  
}
