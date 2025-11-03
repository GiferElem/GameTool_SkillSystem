using UnityEngine;

namespace SkillSystem {
[CreateAssetMenu(fileName = "Skill", menuName = "New Skill/Skill Effect/AttributeEffect")]
public class AttributeEffect : SkillEffect {
    [Header("属性设置")]
    public string attribute_name = "health";
    public float base_change = 10f;
    public override void Apply(Unit user, Unit target, Vector3 target_position, SkillData skill_data) {
        if (!CanAffectTarget(user, target)) return;
        float damage = base_change + skill_data.skill_damage;
        target.ModifyAttribute(attribute_name, -1 * (damage));

        Debug.Log("释放者 " + user.unit_name + " 对" + "目标" + target.unit_name + "造成" + damage + "伤害");
    }
}}