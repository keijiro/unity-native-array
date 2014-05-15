using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;

public class PluginTest : MonoBehaviour
{
    public int iteration = 1000;

    int[] intArray;
    Vector3[] vector3Array;
    GCHandle vector3ArrayHandle;

#if UNITY_EDITOR || UNITY_STANDALONE_OSX

    [DllImport ("SamplePlugin")]
    static extern void WriteIntArray (int[] array, int length);

    [DllImport ("SamplePlugin")]
    static extern void WriteVector3Array (System.IntPtr array, int length);

#elif UNITY_IPHONE

    [DllImport ("__Internal")]
    static extern void WriteIntArray (int[] array, int length);

    [DllImport ("__Internal")]
    static extern void WriteVector3Array (System.IntPtr array, int length);

#endif

    void Start ()
    {
        intArray = new int [1024 * 3];
        vector3Array = new Vector3 [1024];
        vector3ArrayHandle = GCHandle.Alloc (vector3Array, GCHandleType.Pinned);
    }

    void OnDisable ()
    {
        if (vector3ArrayHandle.IsAllocated) vector3ArrayHandle.Free ();
    }

    void Update ()
    {
        for (var i = 0; i < iteration; i++)
        {
            WriteIntArray (intArray, intArray.Length);
            WriteVector3Array (vector3ArrayHandle.AddrOfPinnedObject(), vector3Array.Length);
        }

        for (var i = 0; i < intArray.Length; i++)
        {
            if (intArray[i] != i)
            {
                Debug.LogError("Array contents differ (intArray)");
                Debug.Break();
            }
        }

        for (var i = 0; i < vector3Array.Length; i++)
        {
            if (vector3Array[i] != new Vector3(i * 3, i * 3 + 1, i * 3 + 2))
            {
                Debug.LogError("Array contents differ (vector3Array)");
                Debug.Break();
            }
        }
    }
}
