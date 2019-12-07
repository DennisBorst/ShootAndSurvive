using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrintScanUser : MonoBehaviour
{
    PrintScanner printScanner;

    private void Start()
    {
        printScanner = new PrintScanner();
        float[] data = new float[5 * 5] {
            1, 2, 3, 4, 5,
            6, 7, 8, 9, 10,
            11, 12, 13, 14, 15,
            16, 17, 18, 19, 20,
            21, 22, 23, 24, 25,
        };
        printScanner.Print(data, 5, 5);
        printScanner.dataReceived += PrintData;
    }

    void PrintData( string data ) {
        Debug.Log(data);
    }
}