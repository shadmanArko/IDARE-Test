using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class RandomColorGenerator : MonoBehaviour
{
    /// <summary>
    /// coloring the objects
    /// </summary>
    private void Awake()
    {

        GetComponent<MeshRenderer>().material.color = Random.ColorHSV();

    }
}
