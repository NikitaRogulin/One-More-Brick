﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPoolable
{

    bool IsFree { get; }

    void ResetPoolable();
}
