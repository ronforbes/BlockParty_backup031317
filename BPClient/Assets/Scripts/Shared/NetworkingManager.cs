using UnityEngine;
using System.Collections;
using Bitrave.Azure;
using System.Collections.Generic;
using System;

public class NetworkingManager : MonoBehaviour
{
    static NetworkingManager instance;
    public static NetworkingManager Instance
    {
        get
        {
            // Get the singleton instance of the Game Clock
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<NetworkingManager>();

                DontDestroyOnLoad(instance.gameObject);
            }

            return instance;
        }
    }

    public AzureMobileServices Service;

    void Awake()
    {
        // If this is the first instance, make it the singleton. If a singleton already exists and another reference is found, destroy it
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            if (this != instance)
                Destroy(this.gameObject);
        }
    }

    // Use this for initialization
    void Start ()
    {
        // Connect to the Block Party service
        //Service = new AzureMobileServices("http://localhost:49753", "tEtsHvgLHoRZKUATnELAkzCLWXARVl99");
        Service = new AzureMobileServices("http://blockparty.azure-mobile.net", "tEtsHvgLHoRZKUATnELAkzCLWXARVl99");
    }
}
