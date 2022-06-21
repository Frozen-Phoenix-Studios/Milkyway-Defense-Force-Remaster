public interface ISuppliable
{
    SupplyType SupplyType { get; }
    void Resupply(float amount);
}