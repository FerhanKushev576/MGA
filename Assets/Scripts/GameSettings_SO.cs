﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameSettings", menuName = "ScriptableObject/GameSettings", order = 1)]
public class GameSettings_SO : ScriptableObject
{
    public float speedIncreasePerPoint = 0.1f;
    public float invulnerabilityTime = 2f;

    // Ground Tile
    public float tileSpeed;
    
    // Ground Spawner
    public int startingEmptyTiles = 3;
    public int mapSize = 15;
    
    // Player
    public float jumpForce;
    public float horizontalSpeed;
    
    // Coin
    public float chanceToBeGolden = 0.2f;
    public int worthOfGolden = 5;
}
