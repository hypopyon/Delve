using System;
using Delve.Structures;
using Delve.Tiles;
using Godot;
using DotNext;

namespace Delve; 

public partial class Main {
	public Result Explore(Tile source, Tile destination) {
		if (source.CheckAdjacency(destination).IsSuccessful == false || source.Empty || !destination.Empty)
			return new Result(new ArgumentException());

		var pull = Cards.PullSingle(rng, new CardPullOptions {
			Spades = true,
			LittleJoker = true,
			BigJoker = true
		});

		
		GD.Print(pull.GetName());

		Result<Structure>? structureResult = null;
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
								structureResult = Definitions.Structures.Forest.Create(Map, destination);
								break;
							case 5:
								structureResult = Definitions.Structures.Cavern.Create(Map, destination);
								break;
							case 6:
								structureResult = Definitions.Structures.CrystalCavern.Create(Map, destination);
								break;
							default:
								placeSingleCavern = true;
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
			var placementBuffer = new StructurePlacementBuffer(Map);
			placementBuffer.Add(destination.Position);
			placementBuffer.Add(destination.Position + Direction.Down.ToUnitVector());
			var tiles = placementBuffer.Finish().Value;
			structureResult = Definitions.Structures.Cavern.Create(Map, tiles);
		}

		if (structureResult is not null) {
			if (structureResult.Value.IsSuccessful == false)
				throw structureResult.Value.Error;
			source.Connect(destination);
		}

		return Result.Success;
	}
}