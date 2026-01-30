using System;
using UnityEngine;

namespace PCMaker.Services
{
    public interface IPCBObjectPoolService
    {
        void RegisterBoard(Type typeReference, Board boardPrefab);
        Board PlaceBoardAt(Type type, Transform parent, string pointName);
        void RemoveBoard(Board board);
        Board GetBoard(Type type);
        Type GetBoardTargetType(Board board);

        void Dispose();
    }
}