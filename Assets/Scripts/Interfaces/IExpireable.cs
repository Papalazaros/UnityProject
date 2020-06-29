public interface IExpireable
{
    float ExpirationTime { get; set; }
    void OnExpire();
}