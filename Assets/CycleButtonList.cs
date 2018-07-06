using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CycleButtonList : ScrollRect
{
    private bool isCycling;
    private float cyclingWaitTime;
    private int firstChildIndex;
    private int lastChildIndex;

    new void Awake() {
        isCycling = false;
        cyclingWaitTime = 0.2f;
        firstChildIndex = 0;
        lastChildIndex = content.childCount-1;
    }
	
	void Update () {

        if (!isCycling)
        {
            if (verticalScrollbar.value < 0.5f)
            {
                StartCoroutine("CycleDown");
            }
            else if (verticalScrollbar.value > 0.5f)
            {
                StartCoroutine("CycleUp");
            }
            else
            {
                StopAllCoroutines();
            }
        }
        
    }

    public override void OnDrag(PointerEventData eventData)
    {
        //removes ability to drag on viewport Gameobject
        return;
    }

    IEnumerator CycleUp()
    {
        isCycling = true;
        yield return new WaitForSeconds(cyclingWaitTime);
        content.GetChild(firstChildIndex).SetAsLastSibling();
        isCycling = false;
    }
    IEnumerator CycleDown()
    {
        isCycling = true;
        yield return new WaitForSeconds(cyclingWaitTime);
        content.GetChild(lastChildIndex).SetAsFirstSibling();
        isCycling = false;
    }

}
