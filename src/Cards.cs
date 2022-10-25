using System;
using Godot;
using DotNext;

namespace Delve;

public enum CardColor {
    Black,
    Red
}

public enum CardSuit {
    None,
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
    public static CardSuit GetSuit(this Card card) {
        return card switch {
            >= Card.SpadesAce and <= Card.SpadesKing => CardSuit.Spades,
            >= Card.HeartsAce and <= Card.HeartsKing => CardSuit.Hearts,
            >= Card.ClubsAce and <= Card.ClubsKing => CardSuit.Clubs,
            >= Card.DiamondsAce and <= Card.DiamondsKing => CardSuit.Diamonds,
            Card.LittleJoker or Card.BigJoker => CardSuit.None,
            _ => throw new Exception()
        };
    }
    public static Optional<int> GetValue(this Card card) {
        return card >= Card.LittleJoker ? Optional.None<int>() : Optional.Some((int)card % 13 + 1);
    }
}

public struct CardPullOptions {
    public bool Spades, LittleJoker, BigJoker;
}

public static class Cards {
    public static Card Pull(RandomNumberGenerator rng, CardPullOptions options) {
        var min = options.Spades ? Card.SpadesAce : Card.HeartsAce;
        (bool swapJoker, Card max) = options.LittleJoker switch {
            true when options.BigJoker => (false, Card.BigJoker),
            true when !options.BigJoker => (false, Card.LittleJoker),
            false when options.BigJoker => (true, Card.LittleJoker),
            _ => (false, Card.DiamondsKing)
        };
        var result = (Card)rng.RandiRange((int)min, (int)max);
        if (result == Card.LittleJoker && swapJoker)
            result = Card.BigJoker;
        return result;
    }
}