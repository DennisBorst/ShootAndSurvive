using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public delegate void DataEvent(float[] data, int w, int h);
public delegate void StringDataEvent(string data);

/// <summary>
/// Printer expects some data to print
/// </summary>
public interface IPrinter
{
    /// <summary>
    /// Print provided data
    /// </summary>
    /// <returns>True if printing is successful</returns>
    bool Print(float[] data, int w, int h);
}

/// <summary>
/// Scanner returns some data when scanned
/// </summary>
public interface IScanner
{
    event DataEvent dataReceived;
}


//Debug implementations

public class DebugPrinter : IPrinter
{
    public bool Print(float[] data, int w, int h)
    {
        //DO SOMETHING
        StringBuilder sb = new StringBuilder();
        for (int y = 0; y < h; ++y)
        {
            sb.Append("\n");
            sb.Append($"{y}: ");
            for (int x = 0; x < w; ++x)
            {
                int index = y * w + x;
                sb.Append(data[index]);
                sb.Append(", ");
            }
        }
        Debug.Log(sb.ToString());
        return true;
    }
}

public class DebugScanner : IScanner
{
    class DummyBehaviour : MonoBehaviour { }

    public event DataEvent dataReceived;

    GameObject scanner;
    MonoBehaviour scannerBehaviour;

    public DebugScanner()
    {
        scanner = new GameObject("_scanner");
        scannerBehaviour = scanner.AddComponent<DummyBehaviour>();
        scannerBehaviour.StartCoroutine(RandomScan());
    }

    IEnumerator RandomScan()
    {
        while (Application.isPlaying)
        {
            yield return new WaitForSeconds(Random.Range(1, 5));
            GenerateRandomScan();
        }
    }

    void GenerateRandomScan()
    {
        float[] data = new float[5 * 5];
        for (int i = 0; i < data.Length; ++i)
        {
            data[i] = Random.Range(0,9);
        }

        dataReceived(data, 5, 5);
    }
}