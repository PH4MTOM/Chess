using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class KnightTests
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
    public void Knight_Center_EmptyBoard_ReturnExpectedMoves()
    {
        var board = CreateEmptyBoard();
        var startPosition = (3, 3);

        var knightGameObject = new GameObject("Knight");
        var knight = knightGameObject.AddComponent<Knight>();
        knight.Init("TestKnight", ChessPiece.Color.White, startPosition);
        board[startPosition] = knight;

        var moves = knight.GetPossibleMoves(board);

        // Knight from (3,3) has up to 8 moves
        Assert.AreEqual(8, moves.Count);
        Assert.Contains((4, 5), moves);
        Assert.Contains((5, 4), moves);
        Assert.Contains((2, 5), moves);
        Assert.Contains((1, 4), moves);
        Assert.Contains((4, 1), moves);
        Assert.Contains((5, 2), moves);
        Assert.Contains((2, 1), moves);
        Assert.Contains((1, 2), moves);

        Object.DestroyImmediate(knightGameObject);
    }

    [Test]
    public void Knight_BlockedByOwnPiece()
    {
        var board = CreateEmptyBoard();
        var startPosition = (3, 3);

        var knightGameObject = new GameObject("Knight");
        var knight = knightGameObject.AddComponent<Knight>();
        knight.Init("WhiteKnight", ChessPiece.Color.White, startPosition);
        board[startPosition] = knight;

        // Place a friendly piece on one of the knight target squares
        var blockerGameObject = new GameObject("Blocker");
        var blocker = blockerGameObject.AddComponent<Rook>();
        blocker.Init("WhiteBlocker", ChessPiece.Color.White, (5, 4));
        board[(5, 4)] = blocker;

        var moves = knight.GetPossibleMoves(board);

        // Square occupied by own piece should NOT be included
        Assert.IsFalse(moves.Contains((5, 4)));
        // Other moves remain available
        Assert.IsTrue(moves.Contains((4, 5)));
        Assert.IsTrue(moves.Contains((2, 5)));

        Object.DestroyImmediate(knightGameObject);
        Object.DestroyImmediate(blockerGameObject);
    }

    [Test]
    public void Knight_CanCapture_EnemyPiece()
    {
        var board = CreateEmptyBoard();
        var startPosition = (3, 3);

        var knightGameObject = new GameObject("Knight");
        var knight = knightGameObject.AddComponent<Knight>();
        knight.Init("WhiteKnight", ChessPiece.Color.White, startPosition);
        board[startPosition] = knight;

        // Place an enemy piece on a target square
        var enemyGameObject = new GameObject("Enemy");
        var enemy = enemyGameObject.AddComponent<Rook>();
        enemy.Init("BlackEnemy", ChessPiece.Color.Black, (5, 4));
        board[(5, 4)] = enemy;

        var moves = knight.GetPossibleMoves(board);

        // Enemy square should be included (capture)
        Assert.IsTrue(moves.Contains((5, 4)));
        // Other moves still valid
        Assert.IsTrue(moves.Contains((4, 5)));

        Object.DestroyImmediate(knightGameObject);
        Object.DestroyImmediate(enemyGameObject);
    }
}
