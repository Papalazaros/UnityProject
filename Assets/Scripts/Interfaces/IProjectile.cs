public interface IProjectile
{
    float Damage { get; set; }
    float Weight { get; set; }
    float Expiration { get; set; }
    float Speed { get; set; }
    bool UseGravity { get; set; }
    ProjectileType ProjectileType { get; set; }
}
