using Donjon_100_Pas.Core.Datas.Events;

namespace Donjon_100_Pas.Core.Datas.Dungeons
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
