using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Quoridor
{
    public enum Turn
    {
        FirstPlayer = 0,
        SecondPlayer = 1
    }

    public enum GameStage
    {
        Playing = 0,
        End = 1,
    }

    public class Quoridor
    {
        public readonly Player FirstPlayer;
        public readonly Player SecondPlayer;

        public Turn CurrentPlayerTurn { get; private set; }
        public GameStage CurrentStage { get; private set; }

        public readonly Field Field;
        public readonly List<DeprecatedPath> DeprecatedPaths;

        public event Action<Player> OnWinning;

        public Quoridor(int rows, int columns, Player firstPlayer, Player secondPlayer)
        {
            Field = new Field(new Vector2(rows, columns));
            DeprecatedPaths = new List<DeprecatedPath>();

            FirstPlayer = firstPlayer;
            SecondPlayer = secondPlayer;
            CurrentPlayerTurn = Turn.FirstPlayer;
            CurrentStage = GameStage.Playing;

            SetupField();
            PlacePlayersOnStartPosition();
        }

        public bool TryMoveCurrentPlayer(Direction direction)
        {
            Player player;
            Vector2 potentialPosition;

            switch (direction)
            {
                case Direction.Up:
                    player = GetCurrentPlayer();
                    potentialPosition = player.Position + Vector2.Left;
                    if (!Field.IsOutOfRange(potentialPosition) && !IsPlayerStandingOnCell(potentialPosition))
                    {
                        if (!DoesDeprecatedPathExist(new DeprecatedPath(player.Position, potentialPosition)))
                        {
                            Field.Cells[player.Position.x, player.Position.y].PlayerOver = null;
                            player.Move(Vector2.Left);
                            Field.Cells[player.Position.x, player.Position.y].PlayerOver = player;

                            CheckWinning();
                            return true;
                        }
                    }

                    return false;

                case Direction.Down:
                    player = GetCurrentPlayer();
                    potentialPosition = player.Position + Vector2.Right;
                    if (!Field.IsOutOfRange(potentialPosition) && !IsPlayerStandingOnCell(potentialPosition))
                    {
                        if (!DoesDeprecatedPathExist(new DeprecatedPath(player.Position, potentialPosition)))
                        {
                            Field.Cells[player.Position.x, player.Position.y].PlayerOver = null;
                            player.Move(Vector2.Right);
                            Field.Cells[player.Position.x, player.Position.y].PlayerOver = player;

                            CheckWinning();
                            return true;
                        }
                    }

                    return false;

                case Direction.Left:
                    player = GetCurrentPlayer();
                    potentialPosition = player.Position + Vector2.Down;
                    if (!Field.IsOutOfRange(potentialPosition) && !IsPlayerStandingOnCell(potentialPosition))
                    {
                        if (!DoesDeprecatedPathExist(new DeprecatedPath(player.Position, potentialPosition)))
                        {
                            Field.Cells[player.Position.x, player.Position.y].PlayerOver = null;
                            player.Move(Vector2.Down);
                            Field.Cells[player.Position.x, player.Position.y].PlayerOver = player;

                            CheckWinning();
                            return true;
                        }
                    }

                    return false;

                case Direction.Right:
                    player = GetCurrentPlayer();
                    potentialPosition = player.Position + Vector2.Up;
                    if (!Field.IsOutOfRange(potentialPosition) && !IsPlayerStandingOnCell(potentialPosition))
                    {
                        if (!DoesDeprecatedPathExist(new DeprecatedPath(player.Position, potentialPosition)))
                        {
                            Field.Cells[player.Position.x, player.Position.y].PlayerOver = null;
                            player.Move(Vector2.Up);
                            Field.Cells[player.Position.x, player.Position.y].PlayerOver = player;

                            CheckWinning();
                            return true;
                        }
                    }

                    return false;
            }

            return false;
        }

        private void CheckWinning()
        {
            if (FirstPlayer.Position.x == Field.Size.y - 1)
            {
                OnWinning?.Invoke(FirstPlayer);
                CurrentStage = GameStage.End;
            }
            else if (SecondPlayer.Position.x == 0)
            {
                OnWinning?.Invoke(SecondPlayer);
                CurrentStage = GameStage.End;
            }
        }


        public Player GetCurrentPlayer()
        {
            if (CurrentPlayerTurn == Turn.FirstPlayer)
            {
                return FirstPlayer;
            }

            return SecondPlayer;
        }

        public void SwitchPlayer()
        {
            if (CurrentPlayerTurn == Turn.FirstPlayer)
            {
                CurrentPlayerTurn = Turn.SecondPlayer;
                return;
            }

            CurrentPlayerTurn = Turn.FirstPlayer;
        }

        private void SetupField()
        {
            for (int x = 0; x < Field.Cells.GetLength(0); x++)
            {
                for (int y = 0; y < Field.Cells.GetLength(1); y++)
                {
                    var newCell = new Cell(Field, new Vector2(x, y));
                    Field.Cells[x, y] = newCell;
                }
            }
        }

        private void PlacePlayersOnStartPosition()
        {
            int centerIndex = GetCenterIndex();
            Vector2 firstPlayerPosition = new Vector2(0, centerIndex);
            Vector2 secondPlayerPosition = new Vector2(Field.Size.x - 1, centerIndex);

            FirstPlayer.MoveTo(firstPlayerPosition);
            Field.Cells[firstPlayerPosition.x, secondPlayerPosition.y].PlayerOver = FirstPlayer;

            SecondPlayer.MoveTo(secondPlayerPosition);
            Field.Cells[secondPlayerPosition.x, secondPlayerPosition.y].PlayerOver = SecondPlayer;

            int GetCenterIndex()
            {
                if (Field.Size.y % 2 == 0)
                {
                    return Field.Size.y / 2 - 1;
                }

                return Field.Size.y / 2;
            }
        }

        public bool TryAddDeprecatedPath(DeprecatedPath newDeprecatedPath)
        {
            if (!DoesDeprecatedPathExist(newDeprecatedPath))
            {
                DeprecatedPaths.Add(newDeprecatedPath);

                if (IsAnyPlayerBlocked())
                {
                    DeprecatedPaths.Remove(newDeprecatedPath);
                    return false;
                }

                return true;
            }

            return false;
        }

        public bool IsAnyPlayerBlocked()
        {
            Vector2 fieldSize = Field.Size;

            List<Vector2> positionsThatHaveToBeReachableForFirstPlayer = new List<Vector2>();
            for (int y = 0; y < fieldSize.y; y++)
            {
                positionsThatHaveToBeReachableForFirstPlayer.Add(Field.Cells[fieldSize.x - 1, y].Position);
            }

            Vector2 firstPlayerPosition = FirstPlayer.Position;

            // Cell currentCell = Field.Cells[firstPlayerPosition.x, firstPlayerPosition.y];
            Cell currentCell = new Cell(Field, firstPlayerPosition);
            
            List<Vector2> positionsThatPlayerCanReach = new List<Vector2>();

            positionsThatPlayerCanReach.Add(currentCell.Position);

            for (int i = 0; i <= positionsThatPlayerCanReach.ToList().Count; i++)
            {
                currentCell.Position = positionsThatPlayerCanReach[i];

                var upperPoint = currentCell.UpperCell;
                if (upperPoint != null)
                {
                    var deprecatedPath = new DeprecatedPath(currentCell.Position, upperPoint.Position);
                    if (!DoesDeprecatedPathExist(deprecatedPath))
                    {
                        if (!positionsThatPlayerCanReach.Contains(upperPoint.Position))
                        {
                            positionsThatPlayerCanReach.Add(upperPoint.Position);

                            if (positionsThatHaveToBeReachableForFirstPlayer.Contains(upperPoint.Position))
                            {
                                return false;
                            }
                        }
                    }
                }

                var lowerPoint = currentCell.LowerCell;
                if (lowerPoint != null)
                {
                    var deprecatedPath = new DeprecatedPath(currentCell.Position, lowerPoint.Position);
                    if (!DoesDeprecatedPathExist(deprecatedPath))
                    {
                        if (!positionsThatPlayerCanReach.Contains(lowerPoint.Position))
                        {
                            positionsThatPlayerCanReach.Add(lowerPoint.Position);

                            if (positionsThatHaveToBeReachableForFirstPlayer.Contains(lowerPoint.Position))
                            {
                                return false;
                            }
                        }
                    }
                }

                var leftPoint = currentCell.LeftCell;
                if (leftPoint != null)
                {
                    var deprecatedPath = new DeprecatedPath(currentCell.Position, leftPoint.Position);
                    if (!DoesDeprecatedPathExist(deprecatedPath))
                    {
                        if (!positionsThatPlayerCanReach.Contains(leftPoint.Position))
                        {
                            positionsThatPlayerCanReach.Add(leftPoint.Position);

                            if (positionsThatHaveToBeReachableForFirstPlayer.Contains(leftPoint.Position))
                            {
                                return false;
                            }
                        }
                    }
                }

                var rightPoint = currentCell.RightCell;
                if (rightPoint != null)
                {
                    var deprecatedPath = new DeprecatedPath(currentCell.Position, rightPoint.Position);
                    if (!DoesDeprecatedPathExist(deprecatedPath))
                    {
                        if (!positionsThatPlayerCanReach.Contains(rightPoint.Position))
                        {
                            positionsThatPlayerCanReach.Add(rightPoint.Position);

                            if (positionsThatHaveToBeReachableForFirstPlayer.Contains(rightPoint.Position))
                            {
                                return false;
                            }
                        }
                    }
                }

                if (currentCell.Position == positionsThatPlayerCanReach[positionsThatPlayerCanReach.Count - 1])
                {
                    return true;
                }
            }
            
            return true;
        }

        public bool DoesDeprecatedPathExist(DeprecatedPath deprecatedPath)
        {
            foreach (var path in DeprecatedPaths)
            {
                if (path.FirstPoint == deprecatedPath.FirstPoint &&
                    path.SecondPoint == deprecatedPath.SecondPoint
                    ||
                    path.FirstPoint == deprecatedPath.SecondPoint &&
                    path.SecondPoint == deprecatedPath.FirstPoint)
                {
                    return true;
                }
            }

            return false;
        }

        private bool IsPlayerStandingOnCell(Vector2 cellPosition)
        {
            Player player = Field.Cells[cellPosition.x, cellPosition.y].PlayerOver;
            if (player != null)
            {
                return true;
            }

            return false;
        }
    }
}