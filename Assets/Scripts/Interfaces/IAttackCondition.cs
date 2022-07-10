public interface IAttackCondition
{
    public bool IsPrimed { get; }
    public bool CheckIsMet();
    public bool PrimeCondition();
}