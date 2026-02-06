using Dungeon100Steps.Core.Datas.Events;

namespace Dungeon100Steps.Core.Datas.Dungeons
{
    public class Dungeon(Event[] events)
    {
        public int CurrentEventIndex { get; set; } = -1;
        public Event[] Events = events;

        public Event? NextEvent()
        {
            CurrentEventIndex++;
            if (CurrentEventIndex < Events.Length)
                return Events[CurrentEventIndex];
            return null;
        }
    }
}
