using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class TrackedImageManager : MonoBehaviour
{
    private ARTrackedImageManager trackedImageManager;

    [SerializeField]
    private GameObject model1Prefab;

    [SerializeField]
    private GameObject model2Prefab;

    [SerializeField]
    private GameObject model3Prefab;

    private Dictionary<string, GameObject> spawnedPrefabs = new Dictionary<string, GameObject>();
    private GameObject currentPrefab;

    private void Awake()
    {
        trackedImageManager = GetComponent<ARTrackedImageManager>();

        // Instantiate the prefabs and add them to the dictionary
        InstantiatePrefab("Marker1", model1Prefab);
        InstantiatePrefab("Marker2", model2Prefab);
        InstantiatePrefab("Marker3", model3Prefab);
    }

    private void OnEnable()
    {
        trackedImageManager.trackedImagesChanged += OnTrackedImagesChanged;
    }

    private void OnDisable()
    {
        trackedImageManager.trackedImagesChanged -= OnTrackedImagesChanged;
    }

    private void OnTrackedImagesChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        foreach (var trackedImage in eventArgs.added)
        {
            UpdateImage(trackedImage);
        }

        foreach (var trackedImage in eventArgs.updated)
        {
            UpdateImage(trackedImage);
        }

        foreach (var trackedImage in eventArgs.removed)
        {
            RemovePrefab(trackedImage.referenceImage.name);
        }
    }

    private void UpdateImage(ARTrackedImage trackedImage)
    {
        string imageName = trackedImage.referenceImage.name;
        GameObject prefab = GetPrefab(imageName);

        if (prefab != null)
        {
            if (currentPrefab != prefab)
            {
                // Deactivate current prefab if it's different from the new one
                DeactivateCurrentPrefab();
                currentPrefab = prefab;
            }

            // Update the prefab's position and rotation to match the tracked image
            prefab.transform.position = trackedImage.transform.position;
            prefab.transform.rotation = trackedImage.transform.rotation;

            // Activate the prefab
            prefab.SetActive(true);
        }
        else
        {
            DeactivateCurrentPrefab();
        }
    }

    private GameObject GetPrefab(string imageName)
    {
        GameObject prefab;
        spawnedPrefabs.TryGetValue(imageName, out prefab);
        return prefab;
    }

    private void InstantiatePrefab(string imageName, GameObject prefab)
    {
        if (prefab != null)
        {
            GameObject spawnedObject = Instantiate(prefab, Vector3.zero, Quaternion.identity);
            spawnedObject.SetActive(false);
            spawnedPrefabs.Add(imageName, spawnedObject);
        }
    }

    private void RemovePrefab(string imageName)
    {
        if (currentPrefab != null && currentPrefab.name == imageName)
        {
            DeactivateCurrentPrefab();
            currentPrefab = null;
        }

        GameObject prefab;
        if (spawnedPrefabs.TryGetValue(imageName, out prefab))
        {
            Destroy(prefab);
            spawnedPrefabs.Remove(imageName);
        }
    }

    private void DeactivateCurrentPrefab()
    {
        if (currentPrefab != null)
        {
            currentPrefab.SetActive(false);
        }
    }
}
