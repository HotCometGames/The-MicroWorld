using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class BacteriaData
{
    public int id;
    public int sprite;
    public int health;
    public int damage;
    public float agility;
    public int exp;
    public int addedHealth;
    public int addedDamage;
    public int addedAgility;
}

[System.Serializable]
public class GameData
{
    public List<BacteriaData> Bacterias = new List<BacteriaData>();
    public List<BacteriaData> temporaryBacteria = new List<BacteriaData>();
    public int level = 1;
    public int currentCutScene = 0;
    public int coins = 0;
    public bool started = false;
}
