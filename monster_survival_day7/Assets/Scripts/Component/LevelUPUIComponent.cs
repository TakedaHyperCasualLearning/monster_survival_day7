using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelUPUIComponent : MonoBehaviour
{
    [SerializeField] private List<Button> levelUPButtonList = new List<Button>();
    [SerializeField] private int levelUpKindCount;

    public List<Button> LevelUPButtonList { get => levelUPButtonList; set => levelUPButtonList = value; }
    public int LevelUpKindCount { get => levelUpKindCount; set => levelUpKindCount = value; }
}
