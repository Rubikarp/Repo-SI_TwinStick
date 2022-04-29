
public enum TARGET_TYPE
{
    TARGET_TYPE_NONE,
    TARGET_TYPE_BEE,
    TARGET_TYPE_PLAYER,
    TARGET_TYPE_ENNEMY,
    TARGET_TYPE_BUILDING,
    TARGET_TYPE_HIVE,
}

public interface IHealth
{
    public TARGET_TYPE TargetType { get; }

    public int HealthPoint { get; }
    public void TakeDamage(int damage = 1) { }

}