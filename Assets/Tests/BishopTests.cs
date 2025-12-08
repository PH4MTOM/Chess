using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class BishopTests
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
    public void Bishop_Center_EmptyBoard_ReturnExpectedMoves()
    {
        var board = CreateEmptyBoard();
        var startPosition = (3, 3);

        var bishopGameObject = new GameObject("Bishop");
        var bishop = bishopGameObject.AddComponent<Bishop>();
        bishop.Init("WhiteBishop", ChessPiece.Color.White, startPosition);
        board[startPosition] = bishop;

        var moves = bishop.GetPossibleMoves(board);

        // From (3,3) bishop should have 13 diagonal moves
        Assert.AreEqual(13, moves.Count);
        Assert.Contains((7, 7), moves);
        Assert.Contains((0, 0), moves);
        Assert.Contains((6, 0), moves);
        Assert.Contains((0, 6), moves);

        Object.DestroyImmediate(bishopGameObject);
    }

    [Test]
    public void Bishop_BlockedByOwnPiece()
    {
        var board = CreateEmptyBoard();
        var startPosition = (3, 3);

        var bishopGameObject = new GameObject("Bishop");
        var bishop = bishopGameObject.AddComponent<Bishop>();
        bishop.Init("WhiteBishop", ChessPiece.Color.White, startPosition);
        board[startPosition] = bishop;

        // Place friendly blockers on top-right and top-left diagonals
        var blockerTopRightGameObject = new GameObject("BlockerTopRight");
        var blockerTopRight = blockerTopRightGameObject.AddComponent<Bishop>();
        blockerTopRight.Init("WhiteBlockerTopRight", ChessPiece.Color.White, (5, 5));
        board[(5, 5)] = blockerTopRight;

        var blockerTopLeftGameObject = new GameObject("BlockerTopLeft");
        var blockerTopLeft = blockerTopLeftGameObject.AddComponent<Bishop>();
        blockerTopLeft.Init("WhiteBlockerTopLeft", ChessPiece.Color.White, (1, 5));
        board[(1, 5)] = blockerTopLeft;

        var moves = bishop.GetPossibleMoves(board);

        // Own piece squares should NOT be included and squares beyond should be blocked
        Assert.IsFalse(moves.Contains((5, 5)));
        Assert.IsFalse(moves.Contains((6, 6)));
        Assert.IsFalse(moves.Contains((1, 5)));
        Assert.IsFalse(moves.Contains((0, 6)));

        Object.DestroyImmediate(bishopGameObject);
        Object.DestroyImmediate(blockerTopRightGameObject);
        Object.DestroyImmediate(blockerTopLeftGameObject);
    }

    [Test]
    public void Bishop_CanCapture_EnemyPiece()
    {
        var board = CreateEmptyBoard();
        var startPosition = (3, 3);

        var bishopGameObject = new GameObject("Bishop");
        var bishop = bishopGameObject.AddComponent<Bishop>();
        bishop.Init("WhiteBishop", ChessPiece.Color.White, startPosition);
        board[startPosition] = bishop;

        // Place enemy pieces on top-right and bottom-left diagonals
        var enemyTopRightGameObject = new GameObject("EnemyTopRight");
        var enemyTopRight = enemyTopRightGameObject.AddComponent<Bishop>();
        enemyTopRight.Init("BlackEnemyTopRight", ChessPiece.Color.Black, (5, 5));
        board[(5, 5)] = enemyTopRight;

        var enemyBackLeftGameObject = new GameObject("EnemyBackLeft");
        var enemyBackLeft = enemyBackLeftGameObject.AddComponent<Bishop>();
        enemyBackLeft.Init("BlackEnemyBackLeft", ChessPiece.Color.Black, (1, 1));
        board[(1, 1)] = enemyBackLeft;

        var moves = bishop.GetPossibleMoves(board);

        // Enemy squares should be included (capture) but squares beyond should not
        Assert.IsTrue(moves.Contains((5, 5)));
        Assert.IsFalse(moves.Contains((6, 6)));
        Assert.IsTrue(moves.Contains((1, 1)));
        Assert.IsFalse(moves.Contains((0, 0)));

        Object.DestroyImmediate(bishopGameObject);
        Object.DestroyImmediate(enemyTopRightGameObject);
        Object.DestroyImmediate(enemyBackLeftGameObject);
    }
}