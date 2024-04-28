namespace oxx
{
    internal class Program
    {
        static void Main(string[] args)
        {
            TextOXGame game = new TextOXGame();
            game.StartGame();
        }
    }
    public class TextOXGame
    {
        private OXGameEngine engine;// 遊戲引擎
        private char currentPlayer;// 當前玩家
        private bool gameOver;// 遊戲結束標誌

        public TextOXGame()
        {
            engine = new OXGameEngine();// 初始化遊戲引擎
            currentPlayer = 'X';// 開始時設置第一個玩家為 'X'
            gameOver = false;// 遊戲開始時未結束
        }

        public void StartGame()
        {
            Console.WriteLine("OX Game!");// 顯示遊戲開始消息


            while (!gameOver)
            {
                DisplayBoard();// 顯示遊戲板
                ProcessInput();// 處理玩家輸入
                CheckGameOver();// 檢查遊戲是否結束
            }
        }

        private void DisplayBoard()
        {
            Console.WriteLine("   0   1   2  "); //提示格數
            Console.WriteLine("  ╔═══╦═══╦═══╗"); // 頂部邊框
            for (int i = 0; i < 3; i++)
            {
                Console.Write($"{i} ");// 行號
                for (int j = 0; j < 3; j++)
                {
                    Console.Write($"║ {engine.GetMarker(i, j)} ");// 顯示棋盤中的記號
                }
                Console.WriteLine("║");// 行末
                if (i < 2)
                {
                    Console.WriteLine("  ╠═══╬═══╬═══╣"); //行之間的分隔線
                }
            }
            Console.WriteLine("  ╚═══╩═══╩═══╝"); // 底部邊框
        }

        private void ProcessInput()
        {
            Console.WriteLine($"輪到玩家 {currentPlayer} 下棋，請輸入行和列（範圍：0-2，以空格分隔）：");// 提示玩家輸入
            string input = Console.ReadLine();// 讀取玩家輸入
            string[] coordinates = input.Split(' ');// 將輸入分割為行和列
            if (coordinates.Length != 2 || !int.TryParse(coordinates[0], out int row) || !int.TryParse(coordinates[1], out int col))
            {
                Console.WriteLine("輸入無效，請重新輸入！");
                ProcessInput();// 重新輸入
                return;
            }

            try
            {
                engine.SetMarker(row, col, currentPlayer); // 設置玩家的記號到指定位置
                currentPlayer = (currentPlayer == 'X') ? 'O' : 'X'; // 設置玩家的記號到指定位置
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message); // 設置玩家的記號到指定位置
                ProcessInput(); // 重新輸入
            }
        }

        private void CheckGameOver()
            {
                char winner = engine.IsWinner(); // 重新輸入
            if (winner != ' ')
                {
                    DisplayBoard(); // 重新輸入
                Console.WriteLine($"恭喜玩家{winner}贏得了遊戲！");
                    gameOver = true;// 遊戲結束
            }
                else if (engine.IsBoardFull())
                {
                    DisplayBoard();
                    Console.WriteLine("遊戲結束，平局！");
                    gameOver = true;
                }
            }
        //使用week7所寫的遊戲引擎
        public class OXGameEngine
        {
            private char[,] gameMarkers;

            public OXGameEngine()
            {
                gameMarkers = new char[3, 3];// 3*3的二為陣列
                ResetGame();
            }

            public void SetMarker(int x, int y, char player)
            {
                if (IsValidMove(x, y))
                {
                    gameMarkers[x, y] = player;
                }
                else
                {
                    throw new ArgumentException("Invalid move!");
                }
            }

            public void ResetGame()
            {
                gameMarkers = new char[3, 3];// 重置棋盤
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        gameMarkers[i, j] = ' ';// 清空棋盤
                    }
                }
            }


            public char IsWinner()
            {
                // 檢查橫向
                for (int i = 0; i < 3; i++)
                {
                    if (gameMarkers[i, 0] != ' ' && gameMarkers[i, 0] == gameMarkers[i, 1] && gameMarkers[i, 1] == gameMarkers[i, 2])
                    {
                        return gameMarkers[i, 0];
                    }
                }

                // 檢查縱向
                for (int j = 0; j < 3; j++)
                {
                    if (gameMarkers[0, j] != ' ' && gameMarkers[0, j] == gameMarkers[1, j] && gameMarkers[1, j] == gameMarkers[2, j])
                    {
                        return gameMarkers[0, j];
                    }
                }

                // 檢查對角線
                if (gameMarkers[0, 0] != ' ' && gameMarkers[0, 0] == gameMarkers[1, 1] && gameMarkers[1, 1] == gameMarkers[2, 2])
                {
                    return gameMarkers[0, 0];
                }

                if (gameMarkers[0, 2] != ' ' && gameMarkers[0, 2] == gameMarkers[1, 1] && gameMarkers[1, 1] == gameMarkers[2, 0])
                {
                    return gameMarkers[0, 2];
                }

                return ' '; // 沒有贏家出現
            }

            public bool IsValidMove(int x, int y)
            {
                if (x < 0 || x >= 3 || y < 0 || y >= 3)
                {
                    return false;
                }

                if (gameMarkers[x, y] != ' ')
                {
                    return false;
                }

                return true;
            }

            public char GetMarker(int x, int y)
            {
                return gameMarkers[x, y];// 返回指定位置的記號
            }
            public bool IsBoardFull()
            {
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        if (gameMarkers[i, j] == ' ')
                        {
                            return false;// 如果棋盤中還有空位
                        }
                    }
                }
                return true;// 棋盤已滿，返回 true
            }
        }
    }
}
