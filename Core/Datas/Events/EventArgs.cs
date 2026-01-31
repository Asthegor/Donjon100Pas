using Donjon_100_Pas.Core;

using System;

namespace Donjon_100_Pas.Core.Datas.Events
{
    public class DungeonEndedEventArgs(EventResult result) : EventArgs
    {
        public EventResult Result { get; } = result;
    }
    public class TutorialEventArgs(EventResult result) : EventArgs
    {
        public EventResult Result { get; } = result;
    }
}
