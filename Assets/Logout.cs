using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Logout : MonoBehaviour {

    Button logoutBtn;
    Text labelBtn;
    [SerializeField] GameObject loginScreen;
    [SerializeField] GameObject workScreen;

	// Use this for initialization
	void Start () {
        logoutBtn = GetComponent<Button>();
        labelBtn = GetComponentInChildren<Text>();
        logoutBtn.onClick.AddListener(ReturnToLoginScreen);
        
    }
	
	// Update is called once per frame
	void Update () {
    }

    void ReturnToLoginScreen()
    {
        loginScreen.SetActive(true);
        workScreen.SetActive(false);
    }

    public void LabelButtonWithUser(string username)
    {
        labelBtn.name = "Logout " + username;
    }
}
