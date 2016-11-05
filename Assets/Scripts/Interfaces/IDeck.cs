﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Interfaces
{
    public interface IDeck
    {
        List<Card> Deck { get; set; }
        void UpdateDeckLabel();
    }
}
