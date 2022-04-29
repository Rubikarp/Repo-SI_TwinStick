using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HiveDrawer : MonoBehaviour
{
    [Header("Component")]
    public Hive hive;
    public BasicHealth hiveHealth;

    [Header("UI")]
    public TextMeshProUGUI pollenStockVisu;
    public Slider hiveHealthSlider;


}
