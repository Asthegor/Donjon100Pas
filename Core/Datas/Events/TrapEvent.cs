namespace Dungeon100Steps.Core.Datas.Events
{
    public class TrapEvent(int percentage, string description = "") : Event(EventType.Trap, description)
    {
        public int Percentage { get; private set; } = percentage;
    }
}
