using System;

namespace Helpers
{
    [Flags]
    public enum GameState
    {
        Default = 0,
        Playing = 1,
        Pause = 2,
        Lose = 4,
        Win = 8,
        Finish = 16
    }
}
