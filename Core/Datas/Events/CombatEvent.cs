using DinaCSharp.Resources;

using Donjon_100_Pas.Core.Datas.Enemies;
using Donjon_100_Pas.Core.Datas.Items;
namespace Donjon_100_Pas.Core.Datas.Events
{
    public class CombatEvent(Enemy enemy, int rewardGold, int rewardXP, Item? loot) : Event(EventType.Combat)
    {
        public Enemy Enemy { get; private set; } = enemy;
        public int RewardGold { get; private set; } = rewardGold;
        public int RewardXP { get; private set; } = rewardXP;
        public Item? Loot { get; private set; } = loot;
    }
}
