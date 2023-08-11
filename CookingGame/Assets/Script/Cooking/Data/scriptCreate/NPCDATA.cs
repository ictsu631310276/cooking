using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New NPC",menuName ="Item/Create New NPC")]
public class NPCDATA : ScriptableObject
{
    public int idNPC;
    public string nameNPC;
    public int abilityType;
    public Sprite icon;
    public GameObject model;
    public int loveValue = 0;
}
