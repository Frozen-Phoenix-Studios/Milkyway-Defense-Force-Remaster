public class Laser : Attack
{
    private void OnEnable()
    {
        Initialize();
    }

    private void Update()
    {
        Movement();
    }
 
}