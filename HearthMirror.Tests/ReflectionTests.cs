﻿using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HearthMirror.Tests
{
	[TestClass]
	public class ReflectionTests
	{
		[TestMethod]
		public void GameState_InTournamentScreen()
		{
			var decks = Reflection.GetDecks();
			Assert.IsTrue(decks.Count > 0, "at least one deck was found");
			Assert.IsTrue(decks.TrueForAll(x => !string.IsNullOrEmpty(x.Name)), "all decks have a name");
			Assert.IsTrue(decks.TrueForAll(x => x.Id > 0), "all decks have an ID");

			var selectedDeck = Reflection.GetSelectedDeckInMenu();
			Assert.IsTrue(selectedDeck > 0, "selected deck has an ID");
		}

		[TestMethod]
		public void GameState_InDraftScreen_Complete()
		{
			var deck = Reflection.GetArenaDeck();
			Assert.IsTrue(deck.Deck.Id > 0, "deck has an ID");
			Assert.AreEqual(deck.Deck.Cards.Sum(x => x.Count), 30, "deck has 30 cards");
			Assert.IsTrue(!string.IsNullOrEmpty(deck.Deck.Hero), "deck has a hero");
		}

		[TestMethod]
		public void GameState_InDraftScreen_Drafting()
		{
			var choices = Reflection.GetArenaDraftChoices();
			Assert.AreEqual(choices.Count, 3, "three choices");
			Assert.IsTrue(choices.TrueForAll(x => !string.IsNullOrEmpty(x.Id)), "all choices have a card ID");
		}

		[TestMethod]
		public void GameState_InCollectionScreen()
		{
			var collection = Reflection.GetCollection();
			Assert.IsTrue(collection.Count > 0, "collection contains cards");
			Assert.IsTrue(collection.TrueForAll(x => !string.IsNullOrEmpty(x.Id)), "all cards in collection have an ID");
		}

		[TestMethod]
		public void GameState_InCollectionScreen_Editing()
		{
			var deck = Reflection.GetEditedDeck();
			Assert.IsTrue(deck.Id > 0, "deck has an ID");
			Assert.IsTrue(!string.IsNullOrEmpty(deck.Hero), "deck has a hero");
		}

		[TestMethod]
		public void GameState_InGame_WildRanked()
		{
			var gameType = Reflection.GetGameType();
			Assert.AreEqual(gameType, 7, "gametype is ranked");

			var matchInfo = Reflection.GetMatchInfo();
			Assert.IsTrue(matchInfo.LocalPlayer.Id > 0, "matchInfo has id for local player");
			Assert.IsTrue(matchInfo.OpposingPlayer.Id > 0, "matchInfo has id for opposing player");
			Assert.IsTrue(matchInfo.LocalPlayer.WildRank > 0, "matchInfo has rank for local player");
			Assert.IsTrue(matchInfo.OpposingPlayer.WildRank > 0, "matchInfo has rank for opposing player");

			var serverInfo = Reflection.GetServerInfo();
			Assert.IsTrue(serverInfo.ClientHandle > 0, "serverInfo has client id");
			Assert.IsTrue(serverInfo.GameHandle > 0, "serverInfo has game id");

			Assert.AreEqual(Reflection.GetFormat(), 1, "is wild mode");
		}

		[TestMethod]
		public void GameState_InGame_Spectating()
		{
			var spectating = Reflection.IsSpectating();
			Assert.IsTrue(spectating, "is spectating");
		}

		[TestMethod]
		public void UI_FriendsList_IsVisible()
		{
			Assert.IsTrue(Reflection.IsFriendsListVisible(), "friendslist is visible");
		}
	}
}
