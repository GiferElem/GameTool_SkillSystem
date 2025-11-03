using System.Collections;
using UnityEngine;

namespace SkillSystem {
[CreateAssetMenu(fileName = "Skill", menuName = "New Skill/Skill Effect/ProjectileEffect")]
public class ProjectileEffect : SkillEffect {
    [Header("投掷物设置")]
    public GameObject projectile_prefab;
    public float speed = 10f;

    [Header("发射方式")]
    public LaunchType launch_type = LaunchType.Straight;

    [Header("抛物线参数")]
    public float height = 5f;
    //应用效果
    public override void Apply(Unit user, Unit target, Vector3 target_position, SkillData skill_data) {
        if (projectile_prefab == null) return;

        //创建投掷物
        GameObject projectile = GameObject.Instantiate(projectile_prefab, user.transform.position + Vector3.up, Quaternion.identity);
        //初始化投掷物
        ProjectileController projectile_ctrl = projectile.GetComponent<ProjectileController>();
        if (projectile_ctrl == null) {
            projectile_ctrl = projectile.AddComponent<ProjectileController>();
        }
        projectile_ctrl.Initialize(user, skill_data, this, target_position, speed);
    }
    //异步应用效果
    public override IEnumerator ApplyAsync(Unit user, Unit target, Vector3 target_position, SkillData skill_data) {
        if (projectile_prefab == null) yield break;

        GameObject projectile = GameObject.Instantiate(projectile_prefab, user.transform.position + Vector3.up, Quaternion.identity);
        ProjectileController projectile_ctrl = projectile.GetComponent<ProjectileController>();
        if (projectile_ctrl == null) {
            projectile_ctrl = projectile.AddComponent<ProjectileController>();
        }
        bool is_completed = false;
        projectile_ctrl.Initialize(user, skill_data, this, target_position, speed, () => {
            is_completed = true;
        });
        yield return new WaitUntil(() => is_completed);
    }
}}