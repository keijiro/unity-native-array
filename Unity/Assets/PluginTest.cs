using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;

public class PluginTest : MonoBehaviour
{
    public int iteration = 50000;

    int[] temp;

#if UNITY_EDITOR || UNITY_STANDALONE_OSX
    [DllImport ("SamplePlugin")]
    static extern void TestFunction (int[] array, int length);
#elif UNITY_IPHONE
	[DllImport ("__Internal")]
	static extern void TestFunction (int[] array, int length);
#endif

    void Start ()
    {
        temp = new int[1024 * 2];
    }

    void Update ()
    {
        for (var i = 0; i < iteration; i++)
        {
            TestFunction (temp, temp.Length);
        }
    }
}
