using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelData  {

    public Vector3 position;
    public Vector3 rotation;
    public Vector3 scale;

    public ModelData(Vector3 pos, Vector3 rot, Vector3 sc)
    {
        position = pos;
        rotation = rot;
        scale = sc;
    }

}
