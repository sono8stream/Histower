using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectTester : MonoBehaviour
{
    [SerializeField]
    GameObject[] effects;

    Counter indexCounter;

    // Use this for initialization
    void Start()
    {
        indexCounter = new Counter(effects.Length);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            Instantiate(effects[indexCounter.Now]);
            if (indexCounter.Count())
            {
                indexCounter.Initialize();
            }
        }
    }
}