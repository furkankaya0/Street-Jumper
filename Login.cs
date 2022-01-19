using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using System.Text.RegularExpressions;
using UnityEngine.SceneManagement;
public class Login : MonoBehaviour
{
	public GameObject username;
	public GameObject password;
	public string Username;
	private string Password;
	private String[] Lines;
	private string DecryptedPass;

	public void LoginButton()
	{
		bool UN = false;
		bool PW = false;
		if (Username != "")
		{
			if (System.IO.File.Exists(@"C:/UnityTestFolder/" + Username + ".txt"))
			{
				UN = true;
				Lines = System.IO.File.ReadAllLines(@"C:/UnityTestFolder/" + Username + ".txt");
			}
			else
			{
				Debug.LogWarning("Username Invaild");
			}
		}
		else
		{
			Debug.LogWarning("Username Field Empty");
		}
		if (Password != "")
		{
			if (System.IO.File.Exists(@"C:/UnityTestFolder/" + Username + ".txt"))
			{
				int i = 1;
				foreach (char c in Lines[2])
				{
					i++;
					char Decrypted = (char)(c / i);
					DecryptedPass += Decrypted.ToString();
				}
				if (Password == DecryptedPass)
				{
					PW = true;
				}
				else
				{
					Debug.LogWarning("Password Is invalid");
				}
			}
			else
			{
				Debug.LogWarning("Password Is invalid");
			}
		}
		else
		{
			Debug.LogWarning("Password Field Empty");
		}
		if (UN == true && PW == true)
		{
			username.GetComponent<InputField>().text = "";
			password.GetComponent<InputField>().text = "";
			print("Login Sucessful");
			SceneManager.LoadScene("Loading");
		}
	}
	// Update is called once per frame
	public void Update()
	{
		if (Input.GetKeyDown(KeyCode.Tab))
		{
			if (username.GetComponent<InputField>().isFocused)
			{
				password.GetComponent<InputField>().Select();
			}
		}
		if (Input.GetKeyDown(KeyCode.Return))
		{
			if (Password != "" && Password != "")
			{
				LoginButton();
			}
		}
		Username = username.GetComponent<InputField>().text;
		Password = password.GetComponent<InputField>().text;
	}
}
