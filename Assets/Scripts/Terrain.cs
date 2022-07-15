using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * When the player's die lands on terrain,
 * it affects something onto player.
 * 
 */

public class Terrain : MonoBehaviour
{
    public enum TerrainType
    {
        Normal,
        Slippery,
        Sticky,
        Hazard,
        Bounce,
        NoGravity,
        InstantDestroy,
        Goal
    }

    [SerializeField] private TerrainType terrainType;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
