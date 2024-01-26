using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Item/Create New Sickness")]
public class CreateSicknessScript : ScriptableObject
{
    public int id;
    public int startSicknessLevel;
    public int[] patternPress;
    public int[] declineLife;
    public float[] timeToDeclineLife;
    public GameObject[] modleSickness;
}
