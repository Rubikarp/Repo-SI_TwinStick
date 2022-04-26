public interface IHealth
{
    public int HealthPoint { get; }
    public void TakeDamage(int damage = 1) { }
}