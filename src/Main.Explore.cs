using System;
using Delve.Structures;
using Delve.Tiles;
using Godot;
using DotNext;

namespace Delve; 

public partial class Main {
	public Result Explore(Tile source, Tile destination) {
		if (!source.CheckAdjacency(destination).IsSuccessful || source.Empty || !destination.Empty)
			return new Result(new ArgumentException());

		var pull = Cards.PullSingle(rng, new CardPullOptions {
			Spades = true,
			LittleJoker = true,
			BigJoker = true
		});

		GD.Print(pull.GetName());

		Result<StructureInstance>? structureResult = null;
		var placeSingleCavern = false;
		switch (pull) {
			case Card.LittleJoker:
				placeSingleCavern = true;
				break;
			case Card.BigJoker:
				placeSingleCavern = true;
				break;
			case >= Card.SpadesAce and <= Card.DiamondsKing:
				var suit = pull.GetSuit().Value;
				var value = pull.GetValue().Value;
				switch (suit) {
					case CardSuit.Spades:
						placeSingleCavern = true;
						break;
					case CardSuit.Hearts:
						placeSingleCavern = true;
						State.Resources += pull.GetValue().Value + destination.Y + 1;
						break;
					case CardSuit.Clubs:
						switch (value) {
							case 1:
								structureResult = Meta.Structures.Forest.Create(Map, destination);
								break;
							case 5:
								structureResult = Meta.Structures.Cavern.Create(Map, destination);
								break;
							case 6:
								structureResult = Meta.Structures.CrystalCavern.Create(Map, destination);
								break;
						}
						break;
					case CardSuit.Diamonds:
						placeSingleCavern = true;
						State.TradeGoods += pull.GetValue().Value + destination.Y + 1;
						break;
					default:
						throw new ArgumentOutOfRangeException();
				}
				break;
		}
		if (placeSingleCavern) {
			if (destination.Y == Map.BottommostTile)
				Map.ExpandDownwards();
			structureResult = Meta.Structures.Cavern.Create(Map, destination).Value;
		}

		if (structureResult is not null) {
			if (structureResult.Value.IsSuccessful == false)
				throw structureResult.Value.Error;
			source.Connect(destination);
		}

		return Result.Success;
	}
}