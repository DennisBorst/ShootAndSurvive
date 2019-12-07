using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;


public class PrintScanner : MonoBehaviour
{
    public event StringDataEvent dataReceived;

    IPrinter printer;
    IScanner scanner;

    public PrintScanner()
    {
        //Replace with other implementations once they are available
        // you could also have external settings (.ini?) decide what to do here
        printer = new DebugPrinter();
        scanner = new DebugScanner();

        scanner.dataReceived += ParseData;
    }

    public void Print(float[] data, int w, int h)
    {
        printer.Print(data, w, h);
    }

    void ParseData(float[] data, int w, int h)
    {
        StringBuilder sb = new StringBuilder();
        for ( int y = 0; y < h; ++y )
        {
            sb.Append("\n");
            sb.Append($"{y}: ");
            for (int x = 0; x < w; ++x)
            {
                int index = y * h + x;
                sb.Append(data[index]);
                sb.Append(", ");
            }
        }

        dataReceived?.Invoke(sb.ToString());
    }
}