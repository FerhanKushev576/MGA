using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Donut : Collectible
{
    private void Awake()
    {
        PlayerEntered += (p) =>
        {
            p.StartInvulnerability();
            GetComponent<AudioSource>()?.Play();
        };
    }
}
