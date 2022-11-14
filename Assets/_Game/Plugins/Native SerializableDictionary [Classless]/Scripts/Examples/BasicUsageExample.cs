using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NativeSerializableDictionary;

/// <summary>
/// This class is intended purely to showcase basic usage examples outside of JSON and other custom data integrations.
/// </summary>
public class BasicUsageExample : MonoBehaviour
{
    [Header("Use the Context Menu to execute each usage test")]
    public bool ThisExistsForTheHeader;

    [ContextMenu("Test Creating Entries")]
    private void CreateItem()
    {
        SerializableDictionary<string, int> test1 = new SerializableDictionary<string, int>();
        //using AddDirect instead of Add allows us to skip needing to create a SerializableKVP container
        test1.AddDirect("test", 0);
        Debug.Log($"<b>SerializableDictionary:</b> <color=orange>[</color> <color=lime>\"{test1["test"].Key}\"</color> : <color=red>{test1["test"].Value}</color> <color=orange>]</color>");

        SerializableDictionaryBoxed<string, Vector3> test2 = new SerializableDictionaryBoxed<string, Vector3>();
        List<Vector3> boxedContainer = new List<Vector3>();
        boxedContainer.Add(new Vector3(1, 2, 3));
        boxedContainer.Add(new Vector3(6, 9, 12));
        //you can add either with the container made yourself (you may want the container to have multiple elements immediately)
        test2.AddDirect("boxed", boxedContainer);
        //or allow the container to be made automatically
        test2.AddDirect("unboxed", new Vector3(4, 2, 0));
        foreach (var kvp in test2)
            foreach (var v in kvp.Value.Values)
                Debug.Log($"<b>SerializableDictionaryBoxed:</b> <color=orange>[</color> <color=lime>\"{kvp.Key}\"</color> : <color=red>{v}</color> <color=orange>]</color>");
    }

    [ContextMenu("Test TryGetValue")]
    private void TestTryGetValue()
    {
        SerializableDictionary<string, int> test1 = new SerializableDictionary<string, int>();
        //using AddDirect instead of Add allows us to skip needing to create a SerializableKVP container
        test1.AddDirect("test", 99);
        int value = 0;
        bool result = test1.TryGetValue("test", ref value);
        Debug.Log($"unboxed correct key result: {result} -> Value: {value}");
        value = 0;
        result = test1.TryGetValue("wrongkey", ref value);
        Debug.Log($"unboxed incorrect key result: {result} -> Value: {value}");

        SerializableDictionaryBoxed<string, GameObject> test2 = new SerializableDictionaryBoxed<string, GameObject>();
        test2.AddDirect("test", new GameObject("dummy go"));
        GameObject go = null;
        result = test2.TryGetValue("test", ref go);
        Debug.Log($"boxed correct key result: {result} -> Value: {go.name}");
        DestroyImmediate(go);
        go = null;
        result = test2.TryGetValue("wrongkey", ref go);
        Debug.Log($"boxed incorrect key result: {result} -> Value: {go}");
    }

    [ContextMenu("Test GetValue")]
    private void TestGetValue()
    {
        SerializableDictionary<string, int> test1 = new SerializableDictionary<string, int>();
        //using AddDirect instead of Add allows us to skip needing to create a SerializableKVP container
        test1.AddDirect("test", 99);
        int value = 0;
        value = test1.GetValue("test");
        Debug.Log($"unboxed correct key result: {value}");
        value = 0;
        //I'm wrapping it in a try/catch so we can allow the test to fully complete
        //without the code stopping on the exception
        try
        {
            value = test1.GetValue("wrongkey");
            Debug.Log($"unboxed incorrect key result: {value}");
        }
        catch (System.NullReferenceException)
        {
            Debug.Log("A NullReferenceException is thrown due to the value not existing");
        }

        SerializableDictionaryBoxed<string, GameObject> test2 = new SerializableDictionaryBoxed<string, GameObject>();
        test2.AddDirect("test", new GameObject("dummy go"));
        GameObject go = null;
        go = test2.GetValue("test");
        Debug.Log($"boxed correct key result: {go.name}");
        DestroyImmediate(go);
        go = null;
        //Again, I'm wrapping it in a try/catch so we can allow the test to fully complete
        //without the code stopping on the exception
        try
        {
            go = test2.GetValue("wrongkey");
            Debug.Log($"boxed incorrect key result: {go}");
        }
        catch (System.NullReferenceException)
        {
            Debug.Log("A NullReferenceException is thrown due to the value not existing");
        }
    }
}
