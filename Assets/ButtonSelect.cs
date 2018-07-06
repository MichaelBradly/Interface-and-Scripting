using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSelect : MonoBehaviour {

    [SerializeField] Transform workEnvironment;
    public GameObject SelectedPrimitive;
    Vector3 cameraOffset;

	// Use this for initialization
	void Start () {
        cameraOffset = Vector3.back * 3;

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void MoveCameraToSelected(GameObject primitive)
    {
        SelectedPrimitive = GameObject.Find(primitive.name);
        if (SelectedPrimitive == null)
        {
            Vector3 position = new Vector3(Random.Range(-10.0f, 10.0f), 0, Random.Range(-10.0f, 10.0f));
            SelectedPrimitive = Instantiate(primitive, position, Quaternion.identity, workEnvironment);
            SelectedPrimitive.name = primitive.name;
            
        }
        Camera.main.transform.position = SelectedPrimitive.transform.position - cameraOffset;
        Camera.main.transform.LookAt(SelectedPrimitive.transform.position);

        LogUserInput.LogMessage("Viewing " + SelectedPrimitive.name.Split('_')[0] + " Model");
    }
}
