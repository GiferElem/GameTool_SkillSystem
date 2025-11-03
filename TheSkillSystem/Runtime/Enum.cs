namespace Skill {//关系类型
public enum RelationshipType {
    Friendly,//友军
    Hostile,//敌人
    Neutral//所有人
};

//技能效果类型
public enum EffectType {
    Direct,//直接作用在目标
    Projectile,//投掷物效果
    Area,//范围效果
}

//ProjectileEffect：发射方式
public enum LaunchType {
    Straight,//直线
    Parabola//抛物线
}}