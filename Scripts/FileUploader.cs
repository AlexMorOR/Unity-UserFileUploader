using System;
using System.Runtime.InteropServices;
using UnityEngine;

public class FileUploader : MonoBehaviour
{
    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    void FileRequestCallback(string path)
    {
        FileUploaderHelper.SetResult(path);
    }
}

public static class FileUploaderHelper
{
    static FileUploader fileUploaderObject;
    static Action<string> pathCallback;

    static FileUploaderHelper()
    {
        string methodName = "FileRequestCallback"; // �� ����� ������������ ���������, ����� �� ���������, ����������� :)
        string objectName = typeof(FileUploaderHelper).Name; // � ����� ����������

        var wrapperGameObject = new GameObject(objectName, typeof(FileUploader));
        fileUploaderObject = wrapperGameObject.GetComponent<FileUploader>();

        InitFileLoader(objectName, methodName);
    }

    /// <summary>
    /// ����������� ���� � ������������,
    /// ������ ���������� ��� ����� ������������!
    /// </summary>
    /// <param name="callback">����� ������ ����� ������ ����� �������������, � �������� ��������� ���������� Http ���� � �����</param>
    /// <param name="extensions">���������� ������, ������� ����� �������, ������: ".jpg, .jpeg, .png"</param>
    public static void RequestFile(Action<string> callback, string extensions = ".jpg, .jpeg, .png")
    {
        RequestUserFile(extensions);
        pathCallback = callback;
    }

    /// <summary>
    /// ��� ����������� �������������
    /// </summary>
    /// <param name="path">���� � �����</param>
    public static void SetResult(string path)
    {
        pathCallback.Invoke(path);
        Dispose();
    }

    private static void Dispose()
    {
        ResetFileLoader();
        pathCallback = null;
    }

    [DllImport("__Internal")]
    private static extern void InitFileLoader(string objectName, string methodName);

    [DllImport("__Internal")]
    private static extern void RequestUserFile(string extensions);

    [DllImport("__Internal")]
    private static extern void ResetFileLoader();
}
