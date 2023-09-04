using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;


public class PlaceTrackedImagesScript : MonoBehaviour
{
    private ARTrackedImageManager trackedImageManager;
    public GameObject[] ArPrefabs;
    private readonly Dictionary<string, GameObject> instansiatedPrefabs = new Dictionary<string, GameObject>();

    private void Awake()
    {
        trackedImageManager = GetComponent<ARTrackedImageManager>();
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
        //loopa iigenom nya bilder som har hittats
        foreach (var trackedimage in eventArgs.added)
        {
            var imageName = trackedimage.referenceImage.name;

            foreach (var curPrefab in ArPrefabs)
            {
                if (string.Compare(curPrefab.name, imageName, StringComparison.OrdinalIgnoreCase) == 0 && !instansiatedPrefabs.ContainsKey(imageName))
                {
                    var newPrefab = Instantiate(curPrefab, trackedimage.transform);
                    instansiatedPrefabs[imageName] = newPrefab;
                }
            }
        }

        //om befintliga objekt uppdateras
        foreach (var trackedImage in eventArgs.updated)
        {
            instansiatedPrefabs[trackedImage.referenceImage.name]
                .SetActive(trackedImage.trackingState == TrackingState.Tracking);
        }


        //om ett objekt raderas
        foreach (var trackedImage in eventArgs.removed)
        {
            Destroy(instansiatedPrefabs[trackedImage.referenceImage.name]);
            instansiatedPrefabs.Remove(trackedImage.referenceImage.name);
        }
    }


}
