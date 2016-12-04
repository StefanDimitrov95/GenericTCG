using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Classes.EnumClasses
{
    public enum BattleState
    {
        Start,
        PlayerTurn,
        EnemyTurn,
        CalculateTurn,
        Win,
        Lose,
        Draw,
        End
    }
}
