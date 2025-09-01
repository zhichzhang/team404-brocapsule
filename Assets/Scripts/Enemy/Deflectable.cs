using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.UIElements;

public interface Deflectable
{
    public int getID() { return 0; }
    public bool isDrop() { return true; }
}
