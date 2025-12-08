using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class QueenTests
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
    public void Queen_Center_EmptyBoard_ReturnsExpectedMoves()
    {
        var board = CreateEmptyBoard();

        var queenGameObject = new GameObject("Queen");
        var queen = queenGameObject.AddComponent<Queen>();
        queen.Init("WhiteQueen", ChessPiece.Color.White, (3, 3));
        board[(3, 3)] = queen;

        // Check that the queen has been added to the board at TilePosition (3,3)
        Assert.NotNull(board[(3, 3)]);

        var moves = queen.GetPossibleMoves(board);

        // From (3,3) on empty board queen should have 27 moves
        Assert.AreEqual(27, moves.Count);
        // some expected squares
        Assert.Contains((7, 7), moves);
        Assert.Contains((0, 3), moves);
        Assert.Contains((3, 7), moves);
        Assert.Contains((6, 0), moves);

        // Clean up
        Object.DestroyImmediate(queenGameObject);
    }

    [Test]
    public void Queen_BlockedByOwnPiece()
    {
        var board = CreateEmptyBoard();

        var queenGameObject = new GameObject("Queen");
        var queen = queenGameObject.AddComponent<Queen>();
        queen.Init("Queen", ChessPiece.Color.White, (3, 3));
        board[(3, 3)] = queen;

        var pawnGameObject = new GameObject("Pawn");
        var pawn = pawnGameObject.AddComponent<Pawn>();
        pawn.Init("WhitePawn", ChessPiece.Color.White, (5, 3));
        board[(5, 3)] = pawn;

        var moves = queen.GetPossibleMoves(board);

        // Pawn should block this move for the Queen
        Assert.IsFalse(moves.Contains((5, 3)));
        // Square beyond the Pawn should not be reachable for the Queen
        Assert.IsFalse(moves.Contains((6, 3)));

        // Clean Up
        Object.DestroyImmediate(queenGameObject);
        Object.DestroyImmediate(pawnGameObject);
    }

    [Test]
    public void Queen_CanCapture_EnemyPiece()
    {
        var board = CreateEmptyBoard();

        var queenGameObject = new GameObject("Queen");
        var queen = queenGameObject.AddComponent<Queen>();
        queen.Init("Queen", ChessPiece.Color.White, (3, 3));
        board[(3, 3)] = queen;

        var pawnGameObject = new GameObject("Pawn");
        var pawn = pawnGameObject.AddComponent<Pawn>();
        pawn.Init("BlackPawn", ChessPiece.Color.Black, (5, 3));
        board[(5, 3)] = pawn;

        var moves = queen.GetPossibleMoves(board);

        // Enemy square should be included (capture)
        Assert.IsTrue(moves.Contains((5, 3)));
        // But squares beyond should NOT be reachable
        Assert.IsFalse(moves.Contains((6, 3)));

        // Clean Up
        Object.DestroyImmediate(queenGameObject);
        Object.DestroyImmediate(pawnGameObject);
    }
}
