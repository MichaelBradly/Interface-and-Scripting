using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserDataManager : MonoBehaviour {

    public static UserData currUserData;
    public static bool isLoggedIn = false;
    private static ButtonSelect buttonRef;

    [SerializeField] private Button logoutBtn;
    private Text labelLogout;
    [SerializeField] private Text labelModel;
    [SerializeField] private Text labelPosition;
    [SerializeField] private Text labelRotation;
    [SerializeField] private Text labelScale;
    private GameObject currSelected;

    // Use this for initialization
    void Start () {
        buttonRef = GetComponent<ButtonSelect>();
        logoutBtn.onClick.AddListener(Logout);
        labelLogout = logoutBtn.GetComponentInChildren<Text>();
    }
	
	// Update is called once per frame
	void Update () {
        if (isLoggedIn)
        {
            labelLogout.text = "Logout " + currUserData.userName;

            currSelected = buttonRef.SelectedPrimitive;
            if (currSelected != null)
            {
                
                labelModel.text = currSelected.name.Split('_')[0];
                labelPosition.text = currSelected.transform.position.ToString();
                labelRotation.text = currSelected.transform.eulerAngles.ToString();
                labelScale.text = currSelected.transform.localScale.x.ToString();

                ModelData selectedModel = new ModelData(currSelected.transform.position, currSelected.transform.eulerAngles, currSelected.transform.localScale);
                if (currUserData.models == null)
                {
                    currUserData.models = new Dictionary<string, ModelData>();
                }
                if (currUserData.models.ContainsKey(labelModel.text))
                {
                    currUserData.models[labelModel.text] = selectedModel;
                }
                else
                {
                    currUserData.models.Add(labelModel.text, selectedModel);
                }
            }
        }
    }

    public void Logout()
    {
        isLoggedIn = false;
        LogUserInput.LogMessage(currUserData.userName + " has logged off");

        //Save data to local
        string form = JsonUtility.ToJson(currUserData);
        processModelData(currUserData.models, form);
        LogUserInput.LogMessage(currUserData.userName + " data saved");

        //Reset runtime Data
        currUserData = null;
        labelModel.text = "";
        labelPosition.text = "";
        labelRotation.text = "";
        labelScale.text = "";
    }

    public static void Login(UserData user)
    {
        isLoggedIn = true;
        LogUserInput.LogMessage(user.userName + " has logged on");
        currUserData = user;
        UnpackModelData();
    }

    void processModelData(Dictionary<string,ModelData> dict, string output)
    {
        foreach (KeyValuePair<string,ModelData> pair in dict)
        {
            output += "\r\n";
            output += pair.Key + ";" + pair.Value.position.ToString() + ";" + pair.Value.rotation.ToString() + ";" + pair.Value.scale.ToString();

            //Wipe user model data
            Destroy(GameObject.Find(pair.Key + "_Primitive"));
        }

        System.IO.File.WriteAllText(currUserData.userName + ".txt", output);
    }

    public static void UnpackModelData()
    {
        currUserData.models = new Dictionary<string, ModelData>();

        //Read text data from user file
        string[] Lines = System.IO.File.ReadAllLines(currUserData.userName + ".txt");
        for (int i=1; i < Lines.Length; i++)
        {
            //Create ModelData
            string[] raw_data = Lines[i].Split(';');
            ModelData data = new ModelData(StringToVector3(raw_data[1]), StringToVector3(raw_data[2]), StringToVector3(raw_data[3]));
            currUserData.models.Add(raw_data[0], data);

            //Spawn Primitive models from prefab button, update with stored data
            GameObject button = GameObject.Find(raw_data[0]);
            button.GetComponentInParent<Button>().onClick.Invoke();
            buttonRef.SelectedPrimitive.transform.SetPositionAndRotation(data.position, Quaternion.Euler(data.rotation));
            buttonRef.SelectedPrimitive.transform.localScale = data.scale;
        }

    }

    public static Vector3 StringToVector3(string sVector)
    {
        // Remove the parentheses
        if (sVector.StartsWith("(") && sVector.EndsWith(")"))
        {
            sVector = sVector.Substring(1, sVector.Length - 2);
        }

        // split the items
        string[] sArray = sVector.Split(',');

        // store as a Vector3
        Vector3 result = new Vector3(
            float.Parse(sArray[0]),
            float.Parse(sArray[1]),
            float.Parse(sArray[2]));

        return result;
    }
}
