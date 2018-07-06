using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class Login : MonoBehaviour {
	public GameObject usernameHolder;
	public GameObject passwordHolder;
	private string Username;
	private string Password;

    private string form;
    private String[] Lines;
	private string DecryptedPass;

    UserData currLogUser;
    [SerializeField] GameObject loginSuccess;

	public void LoginButton(){
		bool UN = false;
		bool PW = false;
		if (Username != ""){
			if(System.IO.File.Exists(Username+".txt")){
				UN = true;
				Lines = System.IO.File.ReadAllLines(Username+".txt");
			} else {
                LogUserInput.LogMessage("Username Invaild");
			}
		} else {
            LogUserInput.LogMessage("Username Field Empty");
		}
		if (Password != ""){
			if (System.IO.File.Exists(Username+".txt")){

                currLogUser = JsonUtility.FromJson<UserData>(Lines[0]);
                DecryptedPass = SimpleEncryptString(currLogUser.encryptedPassword);

                if (Password == DecryptedPass){
					PW = true;
				} else {
                    LogUserInput.LogMessage("Password Is invalid");
				}
			} else {
                LogUserInput.LogMessage("Password Is invalid");
			}
		} else {
            LogUserInput.LogMessage("Password Field Empty");
		}
		if (UN == true&&PW == true){
			usernameHolder.GetComponent<InputField>().text = "";
			passwordHolder.GetComponent<InputField>().text = "";
            LogUserInput.LogMessage("Login Sucessful");

            loginSuccess.SetActive(true);
            UserDataManager.Login(currLogUser);
            gameObject.SetActive(false);
        }
	}

    public void RegisterButton()
    {
        bool UN = false;
        bool PW = false;

        if (Username != "")
        {
            if (!System.IO.File.Exists(Username + ".txt"))
            {
                UN = true;
            }
            else
            {
                LogUserInput.LogMessage("Username Taken");
            }
        }
        else
        {
            LogUserInput.LogMessage("Username field Empty");
        }

        if (Password != "")
        {
            if (Password.Length > 5)
            {
                PW = true;
            }
            else
            {
                LogUserInput.LogMessage("Password Must Be atleast 6 Characters long");
            }
        }
        else
        {
            LogUserInput.LogMessage("Password Field Empty");
        }

        if (UN == true && PW == true)
        {
            Password = SimpleDecryptString(Password);

            UserData newRegisteredUser = new UserData(Username, Password);
            string form = JsonUtility.ToJson(newRegisteredUser);
            
            System.IO.File.WriteAllText(Username + ".txt", form);

            usernameHolder.GetComponent<InputField>().text = "";
            passwordHolder.GetComponent<InputField>().text = "";
            LogUserInput.LogMessage("Registration Complete");
        }
    }

    string SimpleEncryptString(string encryptedPw)
    {
        string DecryptedPw = "";
        int i = 1;
        foreach (char c in encryptedPw)
        {
            i++;
            char Decrypted = (char)(c / i);
            DecryptedPw += Decrypted.ToString();
        }
        return DecryptedPw;
    }
    string SimpleDecryptString(string pw)
    {
        string EncryptedPw = "";
        int i = 1;
        foreach (char c in pw)
        {
            i++;
            char Encrypted = (char)(c * i);
            EncryptedPw += Encrypted.ToString();
        }
        return EncryptedPw;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (usernameHolder.GetComponent<InputField>().isFocused)
            {
                passwordHolder.GetComponent<InputField>().Select();
            }
        }
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (Password != "" && Password != "")
            {
                LoginButton();
            }
        }
        Username = usernameHolder.GetComponent<InputField>().text;
        Password = passwordHolder.GetComponent<InputField>().text;
	}
}


