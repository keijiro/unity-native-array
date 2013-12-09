using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;

public class PluginTest : MonoBehaviour
{
    public int iteration = 50000;

    int[] temp;

    [DllImport ("SamplePlugin")]
    static extern void TestFunction (int[] array, int length);

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
