using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DepthShower : MonoBehaviour
{
    [SerializeField] private Transform _parent;
    [SerializeField] private GameObject[] _depthPrefab;

    public void ShowDepths(ObstacleLayer[] obstacleLayers)
    {
        DepthCounter depthCounter = new DepthCounter();
        int[,] depths = depthCounter.GetDepths(obstacleLayers[0]);

        for (int w = 0; w < obstacleLayers[0].Width; ++w)
        {
            for (int h = 0; h < obstacleLayers[0].Height; ++h)
            {
                GameObject go = Instantiate(_depthPrefab[depths[w,h]], _parent);
                go.transform.position = obstacleLayers[0].Positions[w, h];
            }
        }
    }
}