
public interface IAttachable
{
    bool IsActive { get; }
    void Attach();
    void Detach();

}
