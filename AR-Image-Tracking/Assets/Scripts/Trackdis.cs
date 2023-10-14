using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class Trackdis : MonoBehaviour
{
    [SerializeField]
    private GameObject[] trackedPrefabs;
    // 이미지 인식 시 출력할 프리팹 리스트

    private Dictionary<string, GameObject> spawnedObjects = new Dictionary<string, GameObject>();
    private ARTrackedImageManager trackedImageManager;
    // 이미지 인식 시 출력되는 오브젝트 리스트

    void Awake()
    {
        trackedImageManager = GetComponent<ARTrackedImageManager>();
        // AR Session Origin 오브젝트에 컴포넌트로 적용했을 때 사용 가능

        foreach (GameObject prefab in trackedPrefabs)
        {
            GameObject clone = Instantiate(prefab);
            clone.name = prefab.name;
            clone.SetActive(false);
            spawnedObjects.Add(clone.name, clone);
        }
    }

    void OnEnable()
    {
        trackedImageManager.trackedImagesChanged += OnTrackedImagesChanged;
    }

    void OnDisable()
    {
        trackedImageManager.trackedImagesChanged -= OnTrackedImagesChanged;
    }

    void OnTrackedImagesChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        foreach (var trackedImage in eventArgs.added)
        {
            // 카메라에 이미지가 인식되었을 때
            UpdateImage(trackedImage, true);
        }

        foreach (var trackedImage in eventArgs.updated)
        {
            // 카메라에 이미지가 인식되어 업데이트 중일 때
            UpdateImage(trackedImage, true);
        }

        foreach (var trackedImage in eventArgs.removed)
        {
            // 카메라에 이미지가 사라질 때
            UpdateImage(trackedImage, false);
        }
    }

    void UpdateImage(ARTrackedImage trackedImage, bool isTracked)
    {
        string name = trackedImage.referenceImage.name;
        GameObject trackedObject = spawnedObjects[name];

        if (isTracked && trackedImage.trackingState == TrackingState.Tracking)
        {
            trackedObject.transform.position = trackedImage.transform.position;
            trackedObject.transform.rotation = trackedImage.transform.rotation;
            trackedObject.SetActive(true);
        }
        else
        {
            trackedObject.SetActive(false);
        }
    }
}
