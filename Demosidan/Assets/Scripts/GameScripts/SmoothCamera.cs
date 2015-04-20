﻿using UnityEngine;
using System.Collections;

public class SmoothCamera : MonoBehaviour 
{
    public float dampTime = 0.15f;
    
	private GameObject target;
    private Camera mainCamera;
    private Vector3 velocity = Vector3.zero;

    void Start()
    {
		mainCamera = GetComponent<Camera>();
		target = GameObject.FindGameObjectWithTag ("Player");
    }

	// Update is called once per frame
	void Update() 
    {
        if (target)
        {
			Vector3 point = mainCamera.WorldToViewportPoint(target.transform.position);
			Vector3 delta = target.transform.position - mainCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, point.z));
            Vector3 destination = transform.position + delta;
            transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime);
        }   
	}
}