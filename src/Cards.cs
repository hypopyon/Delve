using System;
using Godot;
using DotNext;

namespace Delve;

public enum CardColor {
    Black,
    Red
}

public enum CardSuit {
    Spades,
    Hearts,
    Clubs,
    Diamonds
}

public enum Card {
    SpadesAce, Spades2, Spades3, Spades4, Spades5, Spades6, Spades7, Spades8, Spades9, Spades10, SpadesJack,
    SpadesQueen, SpadesKing, HeartsAce, Hearts2, Hearts3, Hearts4, Hearts5, Hearts6, Hearts7, Hearts8, Hearts9,
    Hearts10, HeartsJack, HeartsQueen, HeartsKing, ClubsAce, Clubs2, Clubs3, Clubs4, Clubs5, Clubs6, Clubs7, Clubs8,
    Clubs9, Clubs10, ClubsJack, ClubsQueen, ClubsKing, DiamondsAce, Diamonds2, Diamonds3, Diamonds4, Diamonds5,
    Diamonds6, Diamonds7, Diamonds8, Diamonds9, Diamonds10, DiamondsJack, DiamondsQueen, DiamondsKing,
    LittleJoker, BigJoker
}

public static class CardExtensions {
    public static CardColor GetColor(this Card card) {
        return card switch {
            >= Card.SpadesAce and <= Card.SpadesKing => CardColor.Black,
            >= Card.HeartsAce and <= Card.HeartsKing => CardColor.Red,
            >= Card.ClubsAce and <= Card.ClubsKing => CardColor.Black,
            >= Card.DiamondsAce and <= Card.DiamondsKing => CardColor.Red,
            Card.LittleJoker => CardColor.Black,
            Card.BigJoker => CardColor.Red,
            _ => throw new Exception()
        };
    }
    public static Optional<CardSuit> GetSuit(this Card card) {
        return card switch {
            >= Card.SpadesAce and <= Card.SpadesKing => CardSuit.Spades,
            >= Card.HeartsAce and <= Card.HeartsKing => CardSuit.Hearts,
            >= Card.ClubsAce and <= Card.ClubsKing => CardSuit.Clubs,
            >= Card.DiamondsAce and <= Card.DiamondsKing => CardSuit.Diamonds,
            Card.LittleJoker or Card.BigJoker => Optional<CardSuit>.None,
            _ => throw new Exception()
        };
    }
    public static Optional<int> GetValue(this Card card) {
        return card >= Card.LittleJoker ? Optional.None<int>() : Optional.Some((int)card % 13 + 1);
    }

    public static string GetName(this Card card) {
        switch (card)
        {
            case Card.LittleJoker:
                return "Little Joker";
            case Card.BigJoker:
                return "Big Joker";
            case >= Card.SpadesAce and <= Card.DiamondsKing:
                var suitString = card.GetSuit().Value switch {
                    CardSuit.Spades => "Spades",
                    CardSuit.Hearts => "Hearts",
                    CardSuit.Clubs => "Clubs",
                    CardSuit.Diamonds => "Diamonds",
                    _ => throw new Exception(),
                };
                var value = card.GetValue().Value;
                var valueString = value switch {
                    1 => "Ace",
                    <= 10 => value.ToString(),
                    11 => "Jack",
                    12 => "Queen",
                    13 => "King",
                    _ => throw new ArgumentOutOfRangeException()
                };
                return valueString + " of " + suitString;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}

public struct CardPullOptions {
    public bool Spades, LittleJoker, BigJoker;
}

public static class Cards {
    public static Card PullSingle(RandomNumberGenerator rng, CardPullOptions options) {
        var min = options.Spades ? Card.SpadesAce : Card.HeartsAce;
        (bool swapJoker, Card max) = options.LittleJoker switch {
            true when options.BigJoker => (false, Card.BigJoker),
            true when options.BigJoker == false => (false, Card.LittleJoker),
            false when options.BigJoker => (true, Card.LittleJoker),
            _ => (false, Card.DiamondsKing)
        };
        var result = (Card)rng.RandiRange((int)min, (int)max);
        if (result == Card.LittleJoker && swapJoker)
            result = Card.BigJoker;
        return result;
    }

    public static Card[] PullMultiple(RandomNumberGenerator rng, CardPullOptions options) {
        throw new NotImplementedException();
    }
}