using Unity.VisualScripting;
using UnityEngine;

public class SkillTreeSystemSettings : MonoBehaviour
{
    [Header("Options")]
    [Tooltip("The Object that contains the SkillTreePlayerData Script")] 
    public SkillTreePlayerData SkillTreePlayerData;
    [Tooltip("Weather or not the skilltree has to use Skill Points.\nIf off it won't cost Skill Points and Skill Points won't be used. So you can add youre own curentcy.\nDefault: true")] 
    public bool UseSkillPoints = true;
    [Tooltip("Weather or not the skilltree has to use a Minimum required level.\nIf off it won't use the Level variable.\nDefault: false")] 
    public bool UseMinReqLevel = false;

    [Header("Skill Colors")]
    public Color NormalColor = new Color(0.6f, 0.6f, 0.6f, 1.0f);
    public Color HighlightedColor = new Color(0.9f, 0.9f, 0.9f, 1.0f);
    public Color PressedColor = new Color(1.0f, 1.0f, 1.0f, 1.0f);
    public Color DisabledColor = new Color(0.4f, 0.4f, 0.4f, 1.0f);
    public Color UnlockedColor = new Color(0.32f, 0.91f, 0.32f, 1.0f);
}
