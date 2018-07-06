using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FixedScrollBar : Scrollbar {

    private float midPoint;

    new void Start () {

        midPoint = 0.5f;

    }
	
	void Update () {
		if (!IsPressed())
        {
            value = midPoint;
        }
	}

}
