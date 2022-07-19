public interface IAttackCondition
{
    public bool IsPrimed { get; }
    public void Activate();
    public void PrimeCondition();
    
    //todo: un-expose primed bool
}