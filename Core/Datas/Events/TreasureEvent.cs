using Donjon_100_Pas.Core.Datas.Items;

namespace Donjon_100_Pas.Core.Datas.Events
{
    public class TreasureEvent(Item? loot, int gold = 0, TrapEvent? trap = null, string description = "") : Event(EventType.Treasure, description)
    {
        public TrapEvent? Trap { get; set; } = trap;
        public Item? Loot { get; set; } = loot;
        public int Gold { get; set; } = gold;
    }
}
