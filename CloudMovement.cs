using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudMovement : MonoBehaviour
{
    public event Action OnCloudDestroy;

    [SerializeField]
    private int limitX = -1128;

    [SerializeField]
    [Min(1)]
    private float deafultSpeed = 2;

    private RectTransform rectTransform;

    private void Awake()
    {
        rectTransform = gameObject.GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(-Vector3.right * deafultSpeed * Time.deltaTime);
        //transform.position += -Vector3.right * deafultSpeed * Time.deltaTime;


        if (rectTransform.localPosition.x <= limitX)
        {
            OnCloudDestroy?.Invoke();
            Destroy(gameObject);
        }
    }
}
