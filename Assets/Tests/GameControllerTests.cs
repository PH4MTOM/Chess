using UnityEngine;
using NUnit.Framework;
using System.Collections.Generic;
using System.Reflection;

public class GameControllerTests
{
    private class DummyPiece : ChessPiece
    {
        public List<(int, int)> Moves = new List<(int, int)>();
        public override List<(int, int)> GetPossibleMoves(Dictionary<(int, int), ChessPiece> pieceCoordsMap)
        {
            return new List<(int, int)>(Moves);
        }
    }

    private Dictionary<(int, int), ChessPiece?> CreateEmptyBoard()
    {
        var map = new Dictionary<(int, int), ChessPiece?>();
        for (int x = 0; x < 8; x++)
        {
            for (int y = 0; y < 8; y++)
            {
                map.Add((x, y), null);
            }
        }
        return map;
    }

    [Test]
    public void IsChecked_DetectsDirectRookAttackOnKing()
    {
        var gameControllerGameObject = new GameObject("GameController");
        var gameController = gameControllerGameObject.AddComponent<GameController>();

        var board = CreateEmptyBoard();

        var kingStartPosition = (4, 0);
        var whiteKingGameObject = new GameObject("WhiteKing");
        var whiteKing = whiteKingGameObject.AddComponent<King>();
        whiteKing.Init("WhiteKing", ChessPiece.Color.White, kingStartPosition);
        board[kingStartPosition] = whiteKing;

        var blackRookStartPosition = (4, 7);
        var blackRookGameObject = new GameObject("BlackRook");
        var blackRook = blackRookGameObject.AddComponent<Rook>();
        blackRook.Init("BlackRook", ChessPiece.Color.Black, blackRookStartPosition);
        board[blackRookStartPosition] = blackRook;

        // Ensure path is clear between rook and king
        for (int y = 1; y <= 6; y++) board[(4, y)] = null;

        // Set isWhiteTurn = true so IsChecked checks black piece moves against white king
        var isWhiteTurn = typeof(GameController).GetField("isWhiteTurn", BindingFlags.Instance | BindingFlags.NonPublic);
        // Check if variable was found
        Assert.IsNotNull(isWhiteTurn);
        isWhiteTurn.SetValue(gameController, true);

        var isCheckedMethod = typeof(GameController).GetMethod("IsChecked", BindingFlags.Instance | BindingFlags.NonPublic);
        // Check if function IsChecked was found
        Assert.IsNotNull(isCheckedMethod);

        var result = (bool)isCheckedMethod.Invoke(gameController, new object[] { board, kingStartPosition });

        // Result = True, if King is in check by the Black Rook
        Assert.IsTrue(result);

        Object.DestroyImmediate(gameControllerGameObject);
        Object.DestroyImmediate(whiteKingGameObject);
        Object.DestroyImmediate(blackRookGameObject);
    }

    [Test]
    public void IsChecked_ReturnsFalse_WhenBlocked()
    {
        var gameControllerGameObject = new GameObject("GameController");
        var gameController = gameControllerGameObject.AddComponent<GameController>();

        var board = CreateEmptyBoard();

        var whiteKingStartPosition = (4, 0);
        var whiteKingGameObject = new GameObject("WhiteKing");
        var whiteKing = whiteKingGameObject.AddComponent<King>();
        whiteKing.Init("WhiteKing", ChessPiece.Color.White, whiteKingStartPosition);
        board[whiteKingStartPosition] = whiteKing;

        var blackRookStartPosition = (4, 7);
        var blackRookGameObject = new GameObject("BlackRook");
        var blackRook = blackRookGameObject.AddComponent<Rook>();
        blackRook.Init("BlackRook", ChessPiece.Color.Black, blackRookStartPosition);
        board[blackRookStartPosition] = blackRook;

        // Blocker piece between WhiteKing and BlackRook
        var blockerStartPosition = (4, 3);
        var blockerGameObject = new GameObject("Blocker");
        var blocker = blockerGameObject.AddComponent<Pawn>();
        blocker.Init("BlackPawn", ChessPiece.Color.Black, blockerStartPosition);
        board[blockerStartPosition] = blocker;

        // Set isWhiteTurn = true so IsChecked checks black piece moves against white king
        var isWhiteTurn = typeof(GameController).GetField("isWhiteTurn", BindingFlags.Instance | BindingFlags.NonPublic);
        // Check if variable isWhiteTurn was found
        Assert.IsNotNull(isWhiteTurn);

        isWhiteTurn.SetValue(gameController, true);

        var isCheckedMethod = typeof(GameController).GetMethod("IsChecked", BindingFlags.Instance | BindingFlags.NonPublic);
        Assert.IsNotNull(isCheckedMethod);

        var result = (bool)isCheckedMethod.Invoke(gameController, new object[] { board, whiteKingStartPosition });

        Assert.IsFalse(result);

        Object.DestroyImmediate(gameControllerGameObject);
        Object.DestroyImmediate(whiteKingGameObject);
        Object.DestroyImmediate(blackRookGameObject);
        Object.DestroyImmediate(blockerGameObject);
    }

    [Test]
    public void IsCheckmate_ReturnsTrue_WhenBlackKingCannotMove()
    {
        var gameControllerGameObject = new GameObject("GameController");
        var gameController = gameControllerGameObject.AddComponent<GameController>();

        var board = CreateEmptyBoard();

        // White Rook A
        var rookAStartPositision = (0, 0);
        var whiteRookAGameObject = new GameObject("WhiteRookA");
        var whiteRookA = whiteRookAGameObject.AddComponent<Rook>();
        whiteRookA.Init("WhiteRookA", ChessPiece.Color.White, rookAStartPositision);
        board[rookAStartPositision] = whiteRookA;

        // White Rook B
        var rookBStartPositision = (0, 1);
        var whiteRookBGameObject = new GameObject("WhiteRookB");
        var whiteRookB = whiteRookBGameObject.AddComponent<Rook>();
        whiteRookB.Init("WhiteRookB", ChessPiece.Color.White, rookBStartPositision);
        board[rookBStartPositision] = whiteRookB;

        // Black King
        var kingStartPositision = (4, 0);
        var blackKingGameObject = new GameObject("BlackKing");
        var blackKing = blackKingGameObject.AddComponent<King>();
        blackKing.Init("BlackKing", ChessPiece.Color.Black, kingStartPositision);
        board[kingStartPositision] = blackKing;

        // Set isWhiteTurn = false so IsChecked.
        var isWhiteTurn = typeof(GameController).GetField("isWhiteTurn", BindingFlags.Instance | BindingFlags.NonPublic);
        // Check if variable isWhiteTurn was found
        Assert.IsNotNull(isWhiteTurn);

        isWhiteTurn.SetValue(gameController, false);

        var isCheckMateMethod = typeof(GameController).GetMethod("IsCheckmate", BindingFlags.Instance | BindingFlags.NonPublic);
        Assert.IsNotNull(isCheckMateMethod);

        var result = (bool)isCheckMateMethod.Invoke(gameController, new object[] { board });

        Assert.IsTrue(result);

        Object.DestroyImmediate(gameControllerGameObject);
        Object.DestroyImmediate(whiteRookAGameObject);
        Object.DestroyImmediate(whiteRookBGameObject);
        Object.DestroyImmediate(blackKingGameObject);
    }

    [Test]
    public void IsCheckmate_ReturnsFalse_WhenKingStillHasMoves()
    {
        var gameControllerGameObject = new GameObject("GameController");
        var gameController = gameControllerGameObject.AddComponent<GameController>();

        var board = CreateEmptyBoard();

        // White Rook A
        var rookAStartPositision = (0, 1);
        var whiteRookAGameObject = new GameObject("WhiteRookA");
        var whiteRookA = whiteRookAGameObject.AddComponent<Rook>();
        whiteRookA.Init("WhiteRookA", ChessPiece.Color.White, rookAStartPositision);
        board[rookAStartPositision] = whiteRookA;

        // Black King
        var kingStartPositision = (4, 0);
        var blackKingGameObject = new GameObject("BlackKing");
        var blackKing = blackKingGameObject.AddComponent<King>();
        blackKing.Init("BlackKing", ChessPiece.Color.Black, kingStartPositision);
        board[kingStartPositision] = blackKing;

        // Set isWhiteTurn = false so IsChecked.
        var isWhiteTurn = typeof(GameController).GetField("isWhiteTurn", BindingFlags.Instance | BindingFlags.NonPublic);
        // Check if variable isWhiteTurn was found
        Assert.IsNotNull(isWhiteTurn);

        isWhiteTurn.SetValue(gameController, false);

        var isCheckMateMethod = typeof(GameController).GetMethod("IsCheckmate", BindingFlags.Instance | BindingFlags.NonPublic);
        Assert.IsNotNull(isCheckMateMethod);

        var result = (bool)isCheckMateMethod.Invoke(gameController, new object[] { board });

        Assert.IsFalse(result);

        Object.DestroyImmediate(gameControllerGameObject);
        Object.DestroyImmediate(whiteRookAGameObject);
        Object.DestroyImmediate(blackKingGameObject);
    }
}
