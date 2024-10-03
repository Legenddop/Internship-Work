using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class ObjectManager : MonoBehaviour
{
    public GameObject[] objectPrefabs;  // Array to store multiple prefabs
    private int currentPrefabIndex = 0; // Index of the currently selected prefab
    private GameObject selectedObject;
    private bool canSpawn = false;

    void Start()
    {
        PlaneFinderBehaviour planeFinder = FindObjectOfType<PlaneFinderBehaviour>();
        if (planeFinder != null)
        {
            planeFinder.OnInteractiveHitTest.AddListener(OnInteractiveHitTest);
        }
    }

    void Update()
    {
        // Check if the left mouse button is clicked to select an object
        if (Input.GetMouseButtonDown(0))
        {
            SelectObject();
        }
        if (selectedObject != null)
        {
            if (Input.GetMouseButton(1)) 
            {
                MoveObject();
            }
        }
    }

    // Toggle spawn mode on and off
    public void ToggleSpawnMode()
    {
        canSpawn = !canSpawn;
    }

    // Switch to the next prefab
    public void NextPrefab()
    {
        currentPrefabIndex = (currentPrefabIndex + 1) % objectPrefabs.Length;
    }

    // Switch to the previous prefab
    public void PreviousPrefab()
    {
        currentPrefabIndex = (currentPrefabIndex - 1 + objectPrefabs.Length) % objectPrefabs.Length;
    }

    // Method to handle interactive hit test from the Plane Finder
    public void OnInteractiveHitTest(HitTestResult result)
    {
        if (canSpawn)
        {
            SpawnObject(result.Position);
        }
    }

    // Method to spawn an object at a specific position
    private void SpawnObject(Vector3 position)
    {
        // Instantiate the currently selected prefab at the given position
        GameObject newObject = Instantiate(objectPrefabs[currentPrefabIndex], position, Quaternion.identity);

        // Add AnchorBehaviour to the new object if it doesn't already have one
        AnchorBehaviour anchorBehaviour = newObject.GetComponent<AnchorBehaviour>();
        if (anchorBehaviour == null)
        {
            anchorBehaviour = newObject.AddComponent<AnchorBehaviour>();
        }

        // Set the anchor for the object
        anchorBehaviour.enabled = true;
    }

    // Method to select an object
    private void SelectObject()
    {
        // Cast a ray from the camera to the mouse position
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            // Check if the hit object has the tag "SpawnedObject"
            if (hit.collider.gameObject.CompareTag("SpawnedObject"))
            {
                // Set the selected object
                selectedObject = hit.collider.gameObject;
            }
        }
    }

    // Method to move the selected object
    private void MoveObject()
    {
        // Example movement logic (you can replace this with your own logic)
        Vector3 newPosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.WorldToScreenPoint(selectedObject.transform.position).z));
        selectedObject.transform.position = newPosition;
    }
}
