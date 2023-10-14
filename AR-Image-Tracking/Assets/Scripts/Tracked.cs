using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class Tracked : MonoBehaviour
{
    [SerializeField] // ��Ÿ ����
    private GameObject[] trackedPrefabs; // ��Ÿ ����
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
            UpdateImage(trackedImage);
        }

        foreach (var trackedImage in eventArgs.updated)
        {
            // ī�޶� �̹����� �νĵǾ� ������Ʈ ���� ��
            UpdateImage(trackedImage);
        }

        foreach (var trackedImage in eventArgs.removed)
        {
            spawnedObjects[trackedImage.name].SetActive(false);
        }
    }

    void UpdateImage(ARTrackedImage trackedImage)
    {
        string name = trackedImage.referenceImage.name;
        GameObject trackedObject = spawnedObjects[name];

        if (trackedImage.trackingState == TrackingState.Tracking)
        {
            trackedObject.transform.position = trackedImage.transform.position;
            trackedObject.transform.rotation = trackedImage.transform.rotation;

            trackedObject.SetActive(true); // ��Ÿ ����
        }
        else
        {
            trackedObject.SetActive(false);
        }
    }
}
