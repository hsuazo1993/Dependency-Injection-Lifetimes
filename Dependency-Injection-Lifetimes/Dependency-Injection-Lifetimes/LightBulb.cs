namespace Dependency_Injection_Lifetimes
{
    public class LightBulb
    {
        public Guid Id { get; } = Guid.NewGuid();
        public bool IsOn { get; private set; }

        public void TurnOn()
        {
            IsOn = true;
            Console.WriteLine($"Light bulb {Id} turned ON!");
        }

        public void TurnOff()
        {
            IsOn = false;
            Console.WriteLine($"Light bulb {Id} turned OFF!");
        }
    }
}
