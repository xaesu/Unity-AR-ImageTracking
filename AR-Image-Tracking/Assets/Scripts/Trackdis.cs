using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class Trackdis : MonoBehaviour
{
    [SerializeField]
    private GameObject[] trackedPrefabs;
    // �̹��� �ν� �� ����� ������ ����Ʈ

    private Dictionary<string, GameObject> spawnedObjects = new Dictionary<string, GameObject>();
    private ARTrackedImageManager trackedImageManager;
    // �̹��� �ν� �� ��µǴ� ������Ʈ ����Ʈ

    void Awake()
    {
        trackedImageManager = GetComponent<ARTrackedImageManager>();
        // AR Session Origin ������Ʈ�� ������Ʈ�� �������� �� ��� ����

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
            // ī�޶� �̹����� �νĵǾ��� ��
            UpdateImage(trackedImage, true);
        }

        foreach (var trackedImage in eventArgs.updated)
        {
            // ī�޶� �̹����� �νĵǾ� ������Ʈ ���� ��
            UpdateImage(trackedImage, true);
        }

        foreach (var trackedImage in eventArgs.removed)
        {
            // ī�޶� �̹����� ����� ��
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
