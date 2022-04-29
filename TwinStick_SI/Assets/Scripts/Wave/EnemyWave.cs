using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new_EnnemyWave", menuName = "Wave/EnnemyWave")]
public class EnemyWave : ScriptableObject
{
    public List<EnnemyGroup> groups = new List<EnnemyGroup>();
}
