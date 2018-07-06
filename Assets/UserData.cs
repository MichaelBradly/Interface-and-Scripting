using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserData{

    public string userName;
    public string encryptedPassword;
    public Dictionary<string, ModelData> models; 

    public UserData(string UN, string PW)
    {
        userName = UN;
        encryptedPassword = PW;
    }

}
