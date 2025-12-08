using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class PawnTests
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
    public void Pawn_Center_EmptyBoard_ReturnExpectedMoves()
    {
        var board = CreateEmptyBoard();
        var startPosition = (3, 3);

        var pawnGameObject = new GameObject("Pawn");
        var pawn = pawnGameObject.AddComponent<Pawn>();
        pawn.Init("WhitePawn", ChessPiece.Color.White, startPosition);
        board[startPosition] = pawn;

        // Check that the Pawn has been added to the board at TilePosition (3,3)
        Assert.NotNull(board[startPosition]);

        // Havent moved, so it still has its start move.
        var moves = pawn.GetPossibleMoves(board);
        Assert.AreEqual(2, moves.Count);
        Assert.Contains((3, 4), moves);
        Assert.Contains((3, 5), moves);

        // Move the pawn, so it loses it start move
        pawn.Move(Vector2.zero, (3, 4));
        Assert.IsTrue(pawn.CurrentTilePosition == (3, 4));

        board[(3, 3)] = null;
        board[(3, 4)] = pawn;

        moves = pawn.GetPossibleMoves(board);
        Assert.AreEqual(1, moves.Count);
        Assert.Contains((3, 5), moves);

        // Clean up
        Object.DestroyImmediate(pawnGameObject);
    }

    [Test]
    public void Pawn_BlockedByOwnPiece()
    {
        var board = CreateEmptyBoard();
        var startPosition = (3, 3);

        var pawnGameObject = new GameObject("Pawn");
        var pawn = pawnGameObject.AddComponent<Pawn>();
        pawn.Init("WhitePawn", ChessPiece.Color.White, startPosition);
        board[startPosition] = pawn;

        // Check that the Pawn has been added to the board at TilePosition (3,3)
        Assert.NotNull(board[startPosition]);

        var rookGameObject = new GameObject("Rook");
        var rook = rookGameObject.AddComponent<Rook>();
        rook.Init("WhiteRook", ChessPiece.Color.White, (3, 5));

        board[(3, 5)] = rook;

        // Generate Pawn Moves
        var moves = pawn.GetPossibleMoves(board);
        Assert.AreEqual(1, moves.Count);
        Assert.Contains((3, 4), moves);

        // Move Pawn one forward
        pawn.Move(Vector2.zero, (3, 4));
        Assert.IsTrue(pawn.CurrentTilePosition == (3, 4));

        board[startPosition] = null;
        board[pawn.CurrentTilePosition] = pawn;

        moves = pawn.GetPossibleMoves(board);
        Assert.AreEqual(0, moves.Count);

        // Clean up
        Object.DestroyImmediate(pawnGameObject);
        Object.DestroyImmediate(rookGameObject);
    }

    [Test]
    public void Pawn_CanCapture_EnemyPiece()
    {
        var board = CreateEmptyBoard();
        var startPosition = (3, 3);

        var pawnGameObject = new GameObject("Pawn");
        var pawn = pawnGameObject.AddComponent<Pawn>();
        pawn.Init("WhitePawn", ChessPiece.Color.White, startPosition);
        board[startPosition] = pawn;

        // Place enemy pieces on both capture diagonals
        var enemyRightGameObject = new GameObject("EnemyRight");
        var enemyRight = enemyRightGameObject.AddComponent<Rook>();
        enemyRight.Init("BlackRookRight", ChessPiece.Color.Black, (4, 4));
        board[(4, 4)] = enemyRight;

        var enemyLeftGameObject = new GameObject("EnemyLeft");
        var enemyLeft = enemyLeftGameObject.AddComponent<Rook>();
        enemyLeft.Init("BlackRookLeft", ChessPiece.Color.Black, (2, 4));
        board[(2, 4)] = enemyLeft;

        // Generate moves and ensure captures are included
        var moves = pawn.GetPossibleMoves(board);
        Assert.IsTrue(moves.Contains((4, 4)));
        Assert.IsTrue(moves.Contains((2, 4)));

        // Remove one enemy and ensure that diagonal is no longer available
        board[(4, 4)] = null;
        Object.DestroyImmediate(enemyRightGameObject);

        moves = pawn.GetPossibleMoves(board);
        Assert.IsFalse(moves.Contains((4, 4)));
        Assert.IsTrue(moves.Contains((2, 4)));

        // Clean up
        Object.DestroyImmediate(pawnGameObject);
        Object.DestroyImmediate(enemyLeftGameObject);
    }
}
