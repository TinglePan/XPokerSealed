﻿using System;
using System.Collections.Generic;
using System.Linq;
using XCardGame.Scripts.Cards;
using XCardGame.Scripts.Common.Constants;

namespace XCardGame.Scripts;

public class HandStrength: IComparable<HandStrength>
{
    public Enums.HandRank Rank;
    public List<BaseCard> PrimaryCards;
    public List<BaseCard> PrimaryComparerCards;
    public List<BaseCard> Kickers;
    
    public HandStrength(Enums.HandRank rank, List<BaseCard> primaryCards, List<BaseCard> primaryComparerCards,
        List<BaseCard> kickers)
    {
        Rank = rank;
        PrimaryCards = primaryCards;
        PrimaryComparerCards = primaryComparerCards;
        Kickers = kickers;
    }


    public int CompareTo(HandStrength other)
    {
        if (Rank > other.Rank) return 1;
        if (Rank < other.Rank) return -1;
        for (var i = 0; i < PrimaryCards.Count; i++)
        {
            if (PrimaryCards[i].Rank > other.PrimaryCards[i].Rank) return 1;
            if (PrimaryCards[i].Rank < other.PrimaryCards[i].Rank) return -1;
        }
        if (Kickers != null)
        {
            for (var i = 0; i < Kickers.Count; i++)
            {
                if (Kickers[i].Rank > other.Kickers[i].Rank) return 1;
                if (Kickers[i].Rank < other.Kickers[i].Rank) return -1;
            }
        }
        return 0;
    }

    public Enums.CardRank LowestPrimaryCardRank => PrimaryCards.Count >= 1 ? PrimaryCards[0].Rank : Enums.CardRank.None;
}