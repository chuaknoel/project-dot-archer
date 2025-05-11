using System;
using System.Collections;
using System.Collections.Generic;

[Serializable]
public class MoveStatData
{
    public bool enable;
    public float moveSpeed;

    public MoveStatData(float moveSpeed)
    {
        this.moveSpeed = moveSpeed;
    }
}
