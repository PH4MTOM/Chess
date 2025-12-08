using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class KingTests
{
    private Dictionary<(int, int), ChessPiece> CreateEmptyBoard()
    {
        var map = new Dictionary<(int, int), ChessPiece>();
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
    public void King_Center_EmptyBoard_ReturnsExpectedMoves()
    {
        var board = CreateEmptyBoard();
        var startPosition = (3, 3);

        var kingGameObject = new GameObject("King");
        var king = kingGameObject.AddComponent<King>();
        king.Init("WhiteKing", ChessPiece.Color.White, startPosition);
        board[startPosition] = king;

        var moves = king.GetPossibleMoves(board);

        // From (3,3) king should have 8 moves
        Assert.AreEqual(8, moves.Count);
        Assert.Contains((3, 4), moves);
        Assert.Contains((4, 4), moves);
        Assert.Contains((4, 3), moves);
        Assert.Contains((2, 2), moves);

        Object.DestroyImmediate(kingGameObject);
    }

    [Test]
    public void King_BlockedByOwnPiece()
    {
        var board = CreateEmptyBoard();
        var startPosition = (3, 3);

        var kingGameObjekt = new GameObject("King");
        var king = kingGameObjekt.AddComponent<King>();
        king.Init("WhiteKing", ChessPiece.Color.White, startPosition);
        board[startPosition] = king;

        // Place a friendly piece adjacent to the king
        var blockerGameObjektt = new GameObject("Blocker");
        var blocker = blockerGameObjektt.AddComponent<Pawn>();
        blocker.Init("WhitePawn", ChessPiece.Color.White, (3, 4));
        board[(3, 4)] = blocker;

        var moves = king.GetPossibleMoves(board);

        // Own piece square should NOT be included
        Assert.IsFalse(moves.Contains((3, 4)));
        // Other adjacent squares still available
        Assert.IsTrue(moves.Contains((4, 4)));
        Assert.IsTrue(moves.Contains((2, 2)));

        Object.DestroyImmediate(kingGameObjekt);
        Object.DestroyImmediate(blockerGameObjektt);
    }

    [Test]
    public void King_CanCapture_EnemyPiece()
    {
        var board = CreateEmptyBoard();
        var startPosition = (3, 3);

        var kingGameObjekt = new GameObject("King");
        var king = kingGameObjekt.AddComponent<King>();
        king.Init("WhiteKing", ChessPiece.Color.White, startPosition);
        board[startPosition] = king;

        // Place an enemy piece adjacent to the king
        var enemyGameObjekt = new GameObject("Pawn");
        var enemy = enemyGameObjekt.AddComponent<Pawn>();
        enemy.Init("BlackPawn", ChessPiece.Color.Black, (4, 3));
        board[(4, 3)] = enemy;

        var moves = king.GetPossibleMoves(board);

        // Enemy square should be included (capture)
        Assert.IsTrue(moves.Contains((4, 3)));
        // Other adjacent squares still valid
        Assert.IsTrue(moves.Contains((3, 4)));

        Object.DestroyImmediate(kingGameObjekt);
        Object.DestroyImmediate(enemyGameObjekt);
    }
}
