﻿using System;
using System.Windows.Controls;

namespace TicTacToe
{
    [Serializable]
    class TicTacGame
    {
        private string ia;
        private int turn;
        private int draw;
        private int pointPl1;
        private int pointPl2;
        //by default you have undo
        private bool undoState = true;
        private int[,] winCombination = new int[,] { { 0, 1, 2 }, { 3, 4, 5 }, { 6, 7, 8 }, { 0, 3, 6 }, { 1, 4, 7 }, { 2, 5, 8 }, { 0, 4, 8 }, { 2, 4, 6 } };
        private string[] gameState;
        private string[] oldState;


        public TicTacGame(string ia, int pointPl1, int pointPl2, int draw)
        {
            this.ia = ia;
            //By default when you call me turn is 0
            this.turn = 0;
            this.draw = draw;
            this.pointPl1 = pointPl1;
            this.pointPl2 = pointPl2;
            //Default Game Board is Empty (For saving to file)
            gameState = new string[] {"","","","","","","","","" };
            oldState = new string[] { "", "", "", "", "", "", "", "", "" };
        }

        public TicTacGame(string ia, int pointPl1, int pointPl2, int draw, int turn, string[] savedBoard, string[] savedOldBoard, bool undo)
        {
            this.ia = ia;
            this.pointPl1 = pointPl1;
            this.pointPl2 = pointPl2;
            this.draw = draw;
            Turn = turn;
            UndoState = undo;
            this.gameState = savedBoard;
            this.oldState = savedOldBoard;
        }

        public void trackOldState(string[] gameBoard)
        {
            for (int i = 0; i < oldState.Length; i++)
                this.oldState[i] = gameBoard[i];
        }

        public string[] getOldState()
        {
            string[] copyState = new string[9];
            Array.Copy(oldState, copyState, oldState.Length);
            return copyState;
        }

        public Button[] loadOnButtonState(Button[] buttonArray,string[] gameBoard)
        {
            for (int i = 0; i < gameBoard.Length; i++)
            {
                buttonArray[i].Content = gameBoard[i];
            }

            return buttonArray;
        }


        /**
         * Save the state after every round
         * and return the stae either, but 
         * a deep copy only
         */
        public void saveButtonState(Button[] buttonArray)
        {
            for (int i = 0; i < buttonArray.Length; i++)
                gameState[i] = (buttonArray[i].Content as string);

        }

        /**
         * Return a deep copy of the state of the buttons
         */ 
        public string[] getButtonState()
        {
            string[] copyState = new string[9];
            Array.Copy(gameState, copyState, gameState.Length);        
            return copyState;
        }


        public bool checkWinner()
        {
            //2D array that will hold all the win combination
            int[,] winCombination = new int[,] { { 0, 1, 2 }, { 3, 4, 5 }, { 6, 7, 8 }, { 0, 3, 6 }, { 1, 4, 7 }, { 2, 5, 8 }, { 0, 4, 8 }, { 2, 4, 6 } };
            bool result = false;

            //Loop looking for a Winner and if I found one !result will leave the loop
            for (int i = 0; i < 8 && !result; i++)
            {
                int a = winCombination[i, 0];
                int b = winCombination[i, 1];
                int c = winCombination[i, 2];

                string but1 = gameState[a];
                string but2 = gameState[b];
                string but3 = gameState[c];

                if (but1 == "" || but2 == "" || but3 == "") // if one if blank
                    continue;    // if they are empty that mean nothing happen this turn

                if (but1 == but2 && but2 == but3)
                {
                    result = true;
                }
            }
            return result;
        }


        /**
         * This property will set a value from this class only 
         * and will get the current turn 
         */
        public int Turn
        {
            get
            {
                return this.turn;
            }
            private set{
                this.turn = value;
            }
        }

        /**
         *This method will call the property Turn  
         * and set a the value for the next turn.
         * The goal is to avoid instantiation of
         * "turn" outside of the class.
         */
        public void nextTurn()
        {
            Turn = Turn +=1;
        }

        /**
         * If you clic on undo you have to
         * delete the turn that you made
         */ 
        public void deleteTurn()
        {
            Turn = Turn -= 1;
        }


        /**
         * Return the IA that the user choosed
         */
        public string IA
        {
            get
            {
                return this.ia.ToString();
            }
        }

        /**
         * return the points, but you cannot set them 
         * only by using the addPoint method
         */ 
        public int PointPl1
        {
            get
            {
                return this.pointPl1;
            }
            private set
            {
                this.pointPl1 = value;
            }
        }

        /**
         * return the points, but you cannot set them 
         * only by using the addPoint method
         */
        public int PointPl2
        {
            get
            {
                return this.pointPl2;
            }
            private set
            {
                this.pointPl2 = value;
            }
        }

        /**
         * will use the private setter to add points
         */
        public void addPointPl1()
        {
               
            PointPl1 = PointPl1 += 1;
        }


        /**
         * will use the private setter to add points
         */
        public void addPointPl2()
        {
            PointPl2 = PointPl2 += 1;
        }

        public int Draw
        {
            get
            {
                return this.draw;
            }
            private set
            {
                this.draw = value;
            }
        }

        public void addDraw()
        {
            Draw = Draw += 1;
        }


        /**
         * return a deep copy of all wins combination because it 
         * is used in all my IA classes
         */
        public int[,] getWinCombination
        {
            get
            {
                return new int[,] { { 0, 1, 2 }, { 3, 4, 5 }, { 6, 7, 8 }, { 0, 3, 6 }, { 1, 4, 7 }, { 2, 5, 8 }, { 0, 4, 8 }, { 2, 4, 6 } };
            }
        }

        public bool UndoState
        {
            get { return undoState; }
            set { undoState = value; }
        }
    }
}
