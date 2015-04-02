﻿using UnityEngine;
using System.Collections;

public class SmoothCamera : MonoBehaviour 
{
    public float dampTime = 0.15f;
    public Transform target;

    private Camera mainCamera;
    private Vector3 velocity = Vector3.zero;

    void Start()
    {
		mainCamera = GetComponent<Camera>();
    }

	// Update is called once per frame
	void Update() 
    {
        if (target)
        {
			Vector3 point = mainCamera.WorldToViewportPoint(target.position);
			Vector3 delta = target.position - mainCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, point.z));
            Vector3 destination = transform.position + delta;
            transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime);
        }   
	}
}
