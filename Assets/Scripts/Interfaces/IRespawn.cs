using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IRespawn
{
    float RespawnHeight { get; }

    void Respawn();

}
