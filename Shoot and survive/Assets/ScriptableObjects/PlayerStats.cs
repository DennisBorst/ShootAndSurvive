using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/PlayerStats", order = 1)]
public class PlayerStats : ScriptableObject
{
    //Player stats
    public int health;
    public float walkSpeed;
    public float sprintSpeed;
    public float cameraSensitivity;
}
