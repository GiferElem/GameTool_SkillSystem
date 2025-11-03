using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace SkillSystem{
public class Unit : MonoBehaviour {
    [Header("基础属性")]
    public string unit_name;
    public int team_ID = 0;

    public float health = 100f;
    public float max_health = 100f;
    public float power = 100f;
    public float max_power = 100f;

    private const string HEALTH = "health";
    private const string MAX_HEALTH = "max_health";
    private const string POWER = "power";
    private const string MAX_POWER = "max_power";

    //属性访问
    public virtual float GetAttribute(string attribute_name) {
        switch (attribute_name.ToLower()) {
            case HEALTH:
                return health;
            case MAX_HEALTH:
                return max_health;
            case POWER:
                return power;
            case MAX_POWER:
                return max_power;
            default://未定义属性为0
                return 0;
        }
    }
    //属性赋值
    public virtual void SetAttribute(string attribute_name, float value) {
        switch (attribute_name.ToLower()) {
            case HEALTH:
                health = Mathf.Clamp(value, 0, max_health);
                break;
            case MAX_HEALTH:
                max_health = Mathf.Max(0, value);
                health = Mathf.Min(health, max_health);
                break;
            case POWER:
                power = Mathf.Clamp(value, 0, max_power);
                break;
            case MAX_POWER:
                max_power = Mathf.Max(0, value);
                power = Mathf.Min(power, max_power);
                break;
        }
    }
    //属性修改
    public virtual void ModifyAttribute(string attribute_name, float change) {
        SetAttribute(attribute_name, GetAttribute(attribute_name) + change);
    }
}}
