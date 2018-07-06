using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseInteraction : MonoBehaviour {

    GameObject mouseSelectedPrimitive;
    private bool validSelection;
    ButtonSelect buttonSelectRef;
    private bool isDragging;
    [SerializeField] float scaleMultipler;
    [SerializeField] float rotateSpeed;
    [SerializeField] Material standardPrimitiveMat;
    [SerializeField] Material selectedPrimitiveMat;

    void Start () {
        buttonSelectRef = GetComponent<ButtonSelect>();
        mouseSelectedPrimitive = gameObject;
        rotateSpeed = 35f;
        scaleMultipler = 1.015f;
    }
	
	void Update () {

        validSelection = mouseSelectedPrimitive.name != gameObject.name;

        if (Input.GetMouseButton(1) && validSelection)
        {
            AdjustRotation();
        }
        else if (Input.GetMouseButton(0) && validSelection)
        {
            AdjustPosition();
        }
        else if (Input.GetMouseButtonUp(0) && isDragging)
        {
            //Log User Drag Action 
            string actionOnModel = "Dragged " + mouseSelectedPrimitive.name.Split('_')[0];
            string dataFromAction = " to new position " + mouseSelectedPrimitive.transform.position.ToString();
            LogUserInput.LogMessage(actionOnModel + dataFromAction);
            isDragging = false;
        }
        else
        {
            HoverMouseSelect();
            AdjustScale();

            isDragging = false;
        }
    }

    void AdjustPosition()
    {
        isDragging = true;
        mouseSelectedPrimitive.transform.Translate(new Vector3(Input.GetAxis("Mouse X"), 0, Input.GetAxis("Mouse Y")) * Time.deltaTime * -rotateSpeed, Space.World);
    }

    void AdjustRotation()
    {
        mouseSelectedPrimitive.transform.Rotate(new Vector3(Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X"), 0) * Time.deltaTime * -rotateSpeed);

        //Log User Input
        string logMessage = "Rotating " + mouseSelectedPrimitive.name.Split('_')[0];
        LogUserInput.LogMessage(logMessage);
    }

    void AdjustScale()
    {

        if (!validSelection)
        {
            return;
        }

        string userAction;
        float scrollValue = Input.GetAxis("Mouse ScrollWheel");
        if (scrollValue > 0f)
        {
            mouseSelectedPrimitive.transform.localScale *= scaleMultipler;
            userAction = "Enlarging ";
        }
        else if (scrollValue < 0f)
        {
            mouseSelectedPrimitive.transform.localScale /= scaleMultipler;
            userAction = "Shrinking ";
        }
        else return;

        //Log User Input
        string logMessage = userAction + mouseSelectedPrimitive.name.Split('_')[0];
        LogUserInput.LogMessage(logMessage);
    }

    //Allows primitive object to be manipulated if mouse is hovering over.
    void HoverMouseSelect()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.transform.name == mouseSelectedPrimitive.name)
            {
                //Hit object is already selected
                return;
            }
            else if (hit.transform.name == buttonSelectRef.SelectedPrimitive.name)
            {
                SelectPrimitive(hit.transform);
            }
            Debug.DrawLine(ray.origin, hit.point);

        }
        else
        {
            DeselectPrimitive();
        }
    }

    void SelectPrimitive(Transform hitGameObject)
    {
        //Update SelectPrimitive reference
        mouseSelectedPrimitive = hitGameObject.gameObject;
        mouseSelectedPrimitive.GetComponent<Renderer>().material = selectedPrimitiveMat;
    }

    void DeselectPrimitive()
    {
        //Deselect primitive
        mouseSelectedPrimitive.GetComponent<Renderer>().material = standardPrimitiveMat;
        //Reset mouseSelecredPrimitive
        mouseSelectedPrimitive = gameObject;
    }


}
