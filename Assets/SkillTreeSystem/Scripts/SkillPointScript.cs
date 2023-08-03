using UnityEngine;
using UnityEngine.UI;

public class SkillPointScript : MonoBehaviour
{
    [Header("Required")]
    [Tooltip("If there is no previous skill you must uncheck this box if there is check it but if you'd like it that it doesn't mater wheather there is or not you may leave it unchecked.\nDefault: true\nNOTE!: With multiple linked skills to the previouse skill will make it that CanBuyOtherLinkedSkills no longer works.")] 
    public bool IsPrevSkillReq = true; //done
    [Tooltip("Weather or not you already have the skill.\nDefault: false")] 
    public bool Unlocked = false; //done

    [Header("Customization")]
    [Tooltip("If checked and the current skill is bought and there are multiple Next Skills it will only allow you to buy one of the linked following skills.\nDefault: True")]
    public bool CanBuyOtherLinkedSkills = true; //done
    [Tooltip("The cost in skill points. Doesn't do anything if UseSkillPoints is off.\nSet to 0 for free.\nDefault: 1")] 
    public int Cost = 1; //done
    [Tooltip("The minimum required Level to buy this skill. Doesn't do anything if UseMinReqLevel is off.\nSet to 0 for no requirement.\nDefault: 0")] 
    public int MinLevelReq = 0; //done
    //[Tooltip("Unused, so doesn't do anything.\nDefault: 0")]
    //public int Tier = 0;

    [Header("Linked Skills")]
    [Tooltip("Put all the following linked skill points in here. Put none here to end the line of linked skills.")]
    public SkillPointScript[] NextSkillPoints; //done
    [Tooltip("Put the previouse linked skill point here. If there is none then there won't be a link and you won't require a previouse skill.")] 
    public SkillPointScript PrevSkillPoint; //done
    Button button;
    SkillTreeSystemSettings skillTreeSystemSettings;

    [Header("Recommended Not To Change")]
    [Tooltip("Check to if the next skill is buyeable.\nRecommended to not change.\nDefault: false")] 
    public bool CanBuyNextSkill = false; //done
    [Tooltip("Weather or not you can currently buy this skill.\nRecommended to not change.\nDefault: false")] 
    public bool Buyeable = false; //done

    void Start()
    {
        GameObject g = GameObject.FindGameObjectWithTag("SkillTreeSystemSettings");
        skillTreeSystemSettings = g.GetComponent<SkillTreeSystemSettings>();
        button = this.GetComponent<Button>();

        if (!skillTreeSystemSettings.UseMinReqLevel)
        {
            MinLevelReq = 0;
        }

        if (!skillTreeSystemSettings.UseSkillPoints)
        {
            skillTreeSystemSettings.SkillTreePlayerData.SkillPoints = Mathf.Infinity;
        }
    }

    public void SkillPressed()
    {
        if (Buyeable)
        {
            Unlocked = true;
            button.interactable = false;
            skillTreeSystemSettings.SkillTreePlayerData.SkillPoints -= Cost;
        }
    }

    void Effect()
    {
        if (Unlocked)
        {
            // Put whatever you want in here may be external public functions.
        }
    }

    void Update()
    {
        CheckBuyeable();
        CheckNextBuyeable();
        ChangeSkillVisuals();
    }

    void CheckNextBuyeable()
    {
        if (NextSkillPoints != null)
        {
            if (Unlocked == true)
            {
                if (!CanBuyOtherLinkedSkills)
                {
                    var i = 0;
                    bool oneSkillBought = false;
                    foreach (SkillPointScript skill in NextSkillPoints)
                    {
                        if (NextSkillPoints[i].Unlocked == true)
                        {
                            oneSkillBought = true;
                        }

                        i++;
                    }

                    if (oneSkillBought == false)
                    {
                        CanBuyNextSkill = true;
                    }
                    else
                    {
                        CanBuyNextSkill = false;
                    }
                }
                else
                {
                    CanBuyNextSkill = true;
                }
            }
            else
            {
                CanBuyNextSkill = false;
            }
        }
    }

    void CheckBuyeable()
    {
        if (skillTreeSystemSettings.SkillTreePlayerData.SkillPoints - Cost >= 0 && MinLevelReq <= skillTreeSystemSettings.SkillTreePlayerData.Level)
        {
            if (IsPrevSkillReq == false)
            {
                Buyeable = true;
            }
            else if (PrevSkillPoint.CanBuyNextSkill == true)
            {
                Buyeable = true;
            }
            else
            {
                Buyeable = false;
            }
        }
        else
        {
            Buyeable = false;
        }
    }

    void ChangeSkillVisuals()
    {
        var colors = button.colors;
        if (!Unlocked)
        {
            if (Buyeable)
            {
                button.interactable = true;

                colors.normalColor = skillTreeSystemSettings.NormalColor;
                colors.highlightedColor = skillTreeSystemSettings.HighlightedColor;
                colors.pressedColor = skillTreeSystemSettings.PressedColor;
                colors.disabledColor = skillTreeSystemSettings.DisabledColor;
                button.colors = colors;
            }
            else
            {
                button.interactable = false;
            }
        }
        else
        {
            button.interactable = false;
            colors.disabledColor = skillTreeSystemSettings.UnlockedColor;
            button.colors = colors;
        }
    }
}
