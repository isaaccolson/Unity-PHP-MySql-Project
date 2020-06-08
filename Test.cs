using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using System;

public class Test : MonoBehaviour
{
    public InputField nameField, dOBField, commentsField;
    public Button submitButton;
    private int currentId = -1;
    private TestManager testManager;

    public void CallTest()
    {
        testManager = FindObjectOfType<TestManager>();
        StartCoroutine(SubmitTest());
    }

    IEnumerator SubmitTest()
    {
        WWWForm form = new WWWForm();
        form.AddField("name", nameField.text);
        form.AddField("dob", dOBField.text);
        form.AddField("comments", commentsField.text);

        //UnityWebRequest www = UnityWebRequest.POST("http://localhost/sqlconnect/test.php", form);
        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost:8888/sqlconnect/test.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                string returnText = www.downloadHandler.text;
                if (returnText[0] == '0')
                {
                    Debug.Log("Test was sucessful. " + returnText);
                    int found = returnText.IndexOf(":");

                    testManager.subjectID = Int32.Parse(returnText.Substring(found + 1));
                    Debug.Log("ID: " + testManager.subjectID);
                    SceneManager.LoadScene(0);
                }
                else
                {
                    Debug.Log("Test failed. Error#" + www.downloadHandler.text);
                    SceneManager.LoadScene(1);
                }
            }
        }
    }

    public void VerifyInputs()
    {
        submitButton.interactable = nameField.text.Length > 4 && dOBField.text.Length > 5 && commentsField.text.Length > 1;
    }
    public void InputDate()
    {
        if (dOBField.text.Length == 2 || dOBField.text.Length == 5)
        {
            dOBField.text += "/";
            dOBField.caretPosition = dOBField.text.Length;
            //Debug.Log("changed name");
        }

    }

}
