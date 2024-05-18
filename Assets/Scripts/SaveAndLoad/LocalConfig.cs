// �����ļ���д
using System.IO;
// ����json���л��ͷ����л�
using Newtonsoft.Json;
// Application.persistentDataPath����������
using UnityEngine;
// �޸�0��ʹ���ֵ������ռ�
using System.Collections.Generic;

public class LocalConfig
{

    // �޸�1������usersData��������
    public static Dictionary<string, UserData> usersData = new Dictionary<string, UserData>();

    // ����1��ѡ��һЩ�������������ַ���ע�Ᵽ�ܣ�
    public static char[] keyChars = {'a', 'b', 'c', 'd', 'e'};

    // ����2�� ���ܷ���
    public static string Encrypt(string data)
    {
        char [] dataChars = data.ToCharArray();
        for (int i=0; i<dataChars.Length; i++)
        {
            char dataChar = dataChars[i];
            char keyChar = keyChars[i % keyChars.Length];
            // �ص㣺 ͨ�����õ��µ��ַ�
            char newChar = (char)(dataChar ^ keyChar);
            dataChars[i] = newChar;
        }
        return new string(dataChars);
    }

    // ����3�� ���ܷ���
    public static string Decrypt(string data)
    {
        return Encrypt(data);
    }

    // �����û������ı�
    public static void SaveUserData(UserData userData)
    {
        // ��persistentDataPath�´���һ��/users�ļ��У��������
        if(!File.Exists(Application.persistentDataPath + "/users"))
        {
            System.IO.Directory.CreateDirectory(Application.persistentDataPath + "/users");
        }

        // �޸�2�����滺������
        usersData[userData.name] = userData;

        // ת���û�����ΪJSON�ַ���
        string jsonData = JsonConvert.SerializeObject(userData);
#if UNITY_EDITOR
        // ����4
        jsonData = Encrypt(jsonData);
#endif
        // ��JSON�ַ���д���ļ��У��ļ���ΪuserData.name��
        File.WriteAllText(Application.persistentDataPath + string.Format("/users/{0}.json", userData.name), jsonData);
        
        PrintUserData(userData);
    }

    private static void PrintUserData(UserData data){
        Debug.Log("UserData Name: " + data.name);
        Debug.Log("UserData Current Level: " + data.currentLevel);

        // ��� LevelStatus ��ÿ����ֵ��
        foreach (var kvp in data.LevelStatus)
        {
            Debug.Log("Level: " + kvp.Key + ", Unlocked: " + kvp.Value);
        }
    }


    // ��ȡ�û����ݵ��ڴ�
    public static UserData LoadUserData(string userName)
    {
        // �޸�3�� ���ȴӻ�����ȡ���ݣ������Ǵ��ı��ļ��ж�ȡ
        if(usersData.ContainsKey(userName))
        {
            return usersData[userName];
        }

        string path = Application.persistentDataPath + string.Format("/users/{0}.json", userName);
        // ����û������ļ��Ƿ����
        if(File.Exists(path))
        {
            // ���ı��ļ��м���JSON�ַ���
            string jsonData = File.ReadAllText(path);
#if UNITY_EDITOR
            // ����5
            jsonData = Decrypt(jsonData);
#endif
            // ��JSON�ַ���ת��Ϊ�û��ڴ�����
            UserData userData = JsonConvert.DeserializeObject<UserData>(jsonData);
            return userData;
        }
        else
        {
            return null;
        }
    }
}


public class UserData
{
    public string name;
    public string currentLevel;
    public Dictionary<string,bool> LevelStatus;

    public UserData(){
        name = "user";
        currentLevel = "level 1";
        LevelStatus = new Dictionary<string, bool>();
    }
}