using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPower
{
    void Activate();
    void Deactivate();
    bool GetActive();
}
