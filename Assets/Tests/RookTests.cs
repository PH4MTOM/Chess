using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class RookTests
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
    public void Rook_Center_EmptyBoard_ReturnExpectedMoves()
    {
        var board = CreateEmptyBoard();
        var startPosition = (3, 3);

        var rookGameObject = new GameObject("Rook");
        var rook = rookGameObject.AddComponent<Rook>();
        rook.Init("WhiteRook", ChessPiece.Color.White, startPosition);
        board[startPosition] = rook;

        var moves = rook.GetPossibleMoves(board);

        // From (3,3) rook should have 14 moves (3 left + 4 right + 3 down + 4 up)
        Assert.AreEqual(14, moves.Count);
        Assert.Contains((0, 3), moves);
        Assert.Contains((7, 3), moves);
        Assert.Contains((3, 0), moves);
        Assert.Contains((3, 7), moves);

        Object.DestroyImmediate(rookGameObject);
    }

    [Test]
    public void Rook_BlockedByOwnPiece()
    {
        var board = CreateEmptyBoard();
        var startPosition = (3, 3);

        var rookGameObject = new GameObject("Rook");
        var rook = rookGameObject.AddComponent<Rook>();
        rook.Init("WhiteRook", ChessPiece.Color.White, startPosition);
        board[startPosition] = rook;

        // Place a friendly blocker to the right and above
        var blockerRightGameObject = new GameObject("BlockerRight");
        var blockerRight = blockerRightGameObject.AddComponent<Rook>();
        blockerRight.Init("WhiteBlockerRight", ChessPiece.Color.White, (5, 3));
        board[(5, 3)] = blockerRight;

        var blockerUpGameObject = new GameObject("BlockerUp");
        var blockerUp = blockerUpGameObject.AddComponent<Rook>();
        blockerUp.Init("WhiteBlockerUp", ChessPiece.Color.White, (3, 6));
        board[(3, 6)] = blockerUp;

        var moves = rook.GetPossibleMoves(board);

        // Own piece square should NOT be included and squares beyond it should be blocked
        Assert.IsFalse(moves.Contains((5, 3)));
        Assert.IsFalse(moves.Contains((6, 3)));
        Assert.IsFalse(moves.Contains((3, 6)));
        Assert.IsFalse(moves.Contains((3, 7)));

        Object.DestroyImmediate(rookGameObject);
        Object.DestroyImmediate(blockerRightGameObject);
        Object.DestroyImmediate(blockerUpGameObject);
    }

    [Test]
    public void Rook_CanCapture_EnemyPiece()
    {
        var board = CreateEmptyBoard();
        var startPosition = (3, 3);

        var rookGameObject = new GameObject("Rook");
        var rook = rookGameObject.AddComponent<Rook>();
        rook.Init("WhiteRook", ChessPiece.Color.White, startPosition);
        board[startPosition] = rook;

        // Place enemy pieces on the right and above
        var enemyRightGameObject = new GameObject("EnemyRight");
        var enemyRight = enemyRightGameObject.AddComponent<Rook>();
        enemyRight.Init("BlackEnemyRight", ChessPiece.Color.Black, (5, 3));
        board[(5, 3)] = enemyRight;

        var enemyUpGameObject = new GameObject("EnemyUp");
        var enemyUp = enemyUpGameObject.AddComponent<Rook>();
        enemyUp.Init("BlackEnemyUp", ChessPiece.Color.Black, (3, 6));
        board[(3, 6)] = enemyUp;

        var moves = rook.GetPossibleMoves(board);

        // Enemy squares should be included (capture) but squares beyond should not
        Assert.IsTrue(moves.Contains((5, 3)));
        Assert.IsFalse(moves.Contains((6, 3)));
        Assert.IsTrue(moves.Contains((3, 6)));
        Assert.IsFalse(moves.Contains((3, 7)));

        Object.DestroyImmediate(rookGameObject);
        Object.DestroyImmediate(enemyRightGameObject);
        Object.DestroyImmediate(enemyUpGameObject);
    }
}