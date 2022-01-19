using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudSpawner : MonoBehaviour
{
    [SerializeField]
    private List<CloudMovement> cloudPrefabs = new List<CloudMovement>();

    private int startX = 1141;
    private int minY = -100, maxY = 75;
    private void Start()
    {
        foreach (CloudMovement cloud in FindObjectsOfType<CloudMovement>())
        {
            cloud.OnCloudDestroy += () => StartCoroutine(SpawnNewCloud());
        }
    }

    private IEnumerator SpawnNewCloud()
    {
        yield return new WaitForSeconds(UnityEngine.Random.Range(0.1f, 0.5f));
        CloudMovement prefab = cloudPrefabs[UnityEngine.Random.Range(0, cloudPrefabs.Count)];
        Vector2 newPosition = new Vector2(startX, UnityEngine.Random.Range(minY, maxY));
        CloudMovement newCloud = Instantiate(prefab.gameObject, Vector3.zero, Quaternion.identity).GetComponent<CloudMovement>();
        
        newCloud.transform.SetParent(transform, false);
        newCloud.GetComponent<RectTransform>().localPosition = newPosition;
        newCloud.OnCloudDestroy += () => StartCoroutine(SpawnNewCloud());
    }
}
