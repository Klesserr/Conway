using System;
using System.ComponentModel;
using System.Diagnostics.Metrics;
using System.IO.IsolatedStorage;

namespace Conway // Note: actual namespace depends on the project name.
{
	internal class Program
	{
		static void Main(string[] args)
		{
			int[,] cells =
			{
			 {1,1,1,0,0,0,1,0},
			{1,0,0,0,0,0,0,1},
			{0,1,0,0,0,1,1,1}
			};
			int generation = 16;
			int[,] expected = GetGeneration(cells, generation);
			for (int i = 0; i < expected.GetLength(0); i++)
			{
				for (int j = 0; j < expected.GetLength(1); j++)
				{
					Console.Write(expected[i, j] + " ");
				}
				Console.WriteLine();
			}
		}
		public static int[,] GetGeneration(int[,] cells, int generation)
		{
			// Inicializar la matriz de celdas iniciales.
			int[,] board = cells;

			// Si no hay generaciones, retornar las celdas iniciales.
			if (generation == 0)
			{
				return cells;
			}

			for (int countGeneration = 0; countGeneration < generation; countGeneration++)
			{
				int[,] expansionBoard = new int[board.GetLength(0) + 2, board.GetLength(1) + 2];

				// Copiar el contenido del board en el centro de la expansionBoard
				for (int i = 0; i < board.GetLength(0); i++)
				{
					for (int j = 0; j < board.GetLength(1); j++)
					{
						expansionBoard[i + 1, j + 1] = board[i, j];
					}
				}

				// Crear un nuevo board para la siguiente generación
				board = new int[expansionBoard.GetLength(0), expansionBoard.GetLength(1)];

				// Aplicar las reglas de Conway
				for (int i = 0; i < expansionBoard.GetLength(0); i++)
				{
					for (int j = 0; j < expansionBoard.GetLength(1); j++)
					{
						int neighbors = CountNeighbors(expansionBoard, i, j);

						if (expansionBoard[i, j] == 1) // Célula viva
						{
							board[i, j] = (neighbors < 2 || neighbors > 3) ? 0 : 1; // Muere o sobrevive
							/*if (neighbors < 2 || neighbors > 3) board[i,j] = 0;
							else board[i, j] = 1;*/
						}
						else // Célula muerta
						{
							board[i, j] = (neighbors == 3) ? 1 : 0; // Se vuelve viva o se mantiene muerta
						}
					}
				}
				// Reducir el tamaño del array eliminando filas y columnas vacías
				board = ReduceBoard(board);
			}

			return board;
		}
		private static int CountNeighbors(int[,] board, int row, int col) // row es valor de i, col es valor de j
		{
			int count = 0;
			int rows = board.GetLength(0);
			int cols = board.GetLength(1);

			for (int i = -1; i <= 1; i++) //con el -1 miramos las superiores Y el valor de 1 las inferiores.
			{
				for (int j = -1; j <= 1; j++)
				{
					if (i == 0 && j == 0) continue; // Ignora la célula misma
					int newRow = row + i;
					int newCol = col + j;
					if (newRow >= 0 && newRow < rows && newCol >= 0 && newCol < cols)
					{
						count += board[newRow, newCol]; //Cuenta el numero total de vecinos vivos y es por ello el +=.
					}
				}
			}
			return count;
		}

		private static int[,] ReduceBoard(int[,] board)
		{
			int rows = board.GetLength(0);
			int cols = board.GetLength(1);

			// Verificar si se pueden eliminar filas o columnas
			int top = 0, bottom = rows - 1, left = 0, right = cols - 1;

			// Encontrar límites que contienen celdas vivas
			while (top < rows && IsRowEmpty(board, top)) top++;
			//Verifica si la fila actual TOP esta vacía mediante RowEmpty.Se incrementa top para verificar la siguiente fila hacia abjo.El proceso sigue hasta que se encuentra una fila que contiene almenos 1 celula viva o el finl del tablero
			while (bottom >= 0 && IsRowEmpty(board, bottom)) bottom--;
			while (left < cols && IsColumnEmpty(board, left)) left++;
			while (right >= 0 && IsColumnEmpty(board, right)) right--;

			// Determinar nuevas dimensiones
			int newRows = Math.Max(0, bottom - top + 1); //Calcula la cantidad de filas que quedan entre los limites top y bot incluyendo ambos.
			int newCols = Math.Max(0, right - left + 1);

						if (i != 0 && j != 0 && i != expansionBoard.GetLength(0) - 1 && j != expansionBoard.GetLength(1) - 1) // Valores para medio del tablero
						{
							board[i, j] = ValueMidBoardDifferentCornersAndDifferentSides(expansionBoard, i, j);
						}
					}
				}
				expansionBoard = board;
				countGeneration++;
			}
			bool reduced;
			do
			{
				reduced = false;
				int countColumnFirst = 0;
				int countColumnLast = 0;
				int countRowUp = 0;
				int countRowDown = 0;

				// Verificar si la primera o última fila está llena de ceros
				for (int i = 0; i < board.GetLength(0); i++)
				{
					//Comprobamos la primera fila y la última.
					if (board[i, 0] == 0 || board[i, board.GetLength(1) - 1] == 0)
					{
						lessRowBoard = true;
						break;
					}
				}

				for (int j = 0; j < board.GetLength(1); j++)
				{
					//Comprobamos la primera columna y la ultima
					if (board[0, j] == 0 || board[board.GetLength(0) - 1, j] == 0)
					{
						lessColBoard = true;
						break;
					}
				}

				if (lessRowBoard) //Si es true, significa que contiene valores 0 y es por ello que quitamos 2 a las filas.
				{
					var temporalBoard = new int[board.GetLength(0) - 2, board.GetLength(1)];
					//Pasamos el contenido del nuevo array creado de forma temporal al array de board y al finalizar igualamos su contenido.

					for (int i = 0; i < temporalBoard.GetLength(0) - 1; i++)
					{
						for (int j = 0; j < temporalBoard.GetLength(1); j++)
						{
							temporalBoard[i + 1, j] = board[i, j];
						}
					}
					board = temporalBoard;
				}

				if (lessColBoard) //Hacemos exactamente lo mismo con las columnas
				{
					var temporalBoard = new int[board.GetLength(0), board.GetLength(1) - 2];

					for (int i = 0; i < temporalBoard.GetLength(0); i++)
					{
						for (int j = 0; j < temporalBoard.GetLength(1) - 1; j++)
						{
							temporalBoard[i, j + 1] = board[i, j];
						}
					}
					board = temporalBoard;
				}

			}
			bool reduced;
			do
			{
				reduced = false;
				int countColumnFirst = 0;
				int countColumnLast = 0;
				int countRowUp = 0;
				int countRowDown = 0;

				// Verificar si la primera o última fila está llena de ceros
				for (int i = 0; i < board.GetLength(0); i++)
				{
					if (board[i, 0] == 0) countRowUp++;
					if (board[i, board.GetLength(1) - 1] == 0) countRowDown++;
				}

				// Verificar si la primera o última columna está llena de ceros
				for (int j = 0; j < board.GetLength(1); j++)
				{
					if (board[0, j] == 0) countColumnFirst++;
					if (board[board.GetLength(0) - 1, j] == 0) countColumnLast++;
				}

				// Eliminar filas si están llenas de ceros
				if (countRowUp == board.GetLength(0))
				{
					// Reduce el tamaño del array temporalmente
					int[,] temporalArray = new int[board.GetLength(0) - 1, board.GetLength(1)];
					for (int i = 1; i < board.GetLength(0); i++)
					{
						for (int j = 0; j < board.GetLength(1); j++)
						{
							temporalArray[i - 1, j] = board[i, j];
						}
					}
					board = temporalArray;
					reduced = true;
				}
				if (countRowDown == board.GetLength(0))
				{
					int[,] temporalArray = new int[board.GetLength(0) - 1, board.GetLength(1)];
					for (int i = 0; i < board.GetLength(0) - 1; i++)
					{
						for (int j = 0; j < board.GetLength(1); j++)
						{
							temporalArray[i, j] = board[i, j];
						}
					}
					board = temporalArray;
					reduced = true;
				}

				// Eliminar columnas si están llenas de ceros
				if (countColumnFirst == board.GetLength(1))
				{
					int[,] temporalArray = new int[board.GetLength(0), board.GetLength(1) - 1];
					for (int i = 0; i < board.GetLength(0); i++)
					{
						for (int j = 1; j < board.GetLength(1); j++)
						{
							temporalArray[i, j - 1] = board[i, j];
						}
					}
					board = temporalArray;
					reduced = true;
				}
				if (countColumnLast == board.GetLength(1))
				{
					int[,] temporalArray = new int[board.GetLength(0), board.GetLength(1) - 1];
					for (int i = 0; i < board.GetLength(0); i++)
					{
						for (int j = 0; j < board.GetLength(1) - 1; j++)
						{
							temporalArray[i, j] = board[i, j];
						}
					}
					board = temporalArray;
					reduced = true;
				}

			} while (reduced);

			return board;
		}

		public static int ValueCornerTopLeftCells(int[,] expansionBoard, int valueI, int valueJ)
		{


			int rightValue = expansionBoard[valueI, valueJ + 1];
			int crossValue = expansionBoard[valueI + 1, valueJ + 1];
			int downValue = expansionBoard[valueI + 1, valueJ];
			int actualValue = expansionBoard[valueI, valueJ];
			int countHp = 0;

			if (rightValue == 1) countHp++;
			if (crossValue == 1) countHp++;
			if (downValue == 1) countHp++;

			if (actualValue == 1 && countHp < 2 || countHp > 3) actualValue = 0;
			else if (actualValue == 1 && countHp == 2 || countHp == 3) actualValue = 1;

			if (actualValue == 0 && countHp == 3) actualValue = 1;
			return actualValue;


		}
		public static int ValueCornerDownLeftCells(int[,] expansionBoard, int valueI, int valueJ)
		{

			int upValue = expansionBoard[valueI - 1, valueJ];
			int crossValue = expansionBoard[valueI - 1, valueJ + 1];
			int rightValue = expansionBoard[valueI, valueJ + 1];
			int countHp = 0;
			int actualValue = expansionBoard[valueI, valueJ];

			if (rightValue == 1) countHp++;
			if (crossValue == 1) countHp++;
			if (upValue == 1) countHp++;

			if (actualValue == 1 && countHp < 2 || countHp > 3) actualValue = 0;
			else if (actualValue == 1 && countHp == 2 || countHp == 3) actualValue = 1;

			if (actualValue == 0 && countHp == 3) actualValue = 1;
			return actualValue;

		}
		public static int ValueCornerTopRightCells(int[,] expansionBoard, int valueI, int valueJ)
		{
			int leftValue = expansionBoard[valueI, valueJ - 1];
			int crossValue = expansionBoard[valueI + 1, valueJ - 1];
			int downValue = expansionBoard[valueI + 1, valueJ];
			int actualValue = expansionBoard[valueI, valueJ];
			int countHp = 0;

			if (leftValue == 1) countHp++;
			if (crossValue == 1) countHp++;
			if (downValue == 1) countHp++;

			if (actualValue == 1 && countHp < 2 || countHp > 3) actualValue = 0;
			else if (actualValue == 1 && countHp == 2 || countHp == 3) actualValue = 1;

			if (actualValue == 0 && countHp == 3) actualValue = 1;
			return actualValue;
		}
		public static int ValueCornerDownRightCells(int[,] expansionBoard, int valueI, int valueJ)
		{

			int upValue = expansionBoard[valueI - 1, valueJ];
			int crossValue = expansionBoard[valueI - 1, valueJ - 1];
			int leftValue = expansionBoard[valueI, valueJ - 1];
			int actualValue = expansionBoard[valueI, valueJ];
			int countHp = 0;

			if (leftValue == 1) countHp++;
			if (crossValue == 1) countHp++;
			if (upValue == 1) countHp++;

			if (actualValue == 1 && countHp < 2 || countHp > 3) actualValue = 0;
			else if (actualValue == 1 && countHp == 2 || countHp == 3) actualValue = 1;

			if (actualValue == 0 && countHp == 3) actualValue = 1;
			return actualValue;

		}

		public static int ValueTopDifferentCorners(int[,] expansionBoard, int valueI, int valueJ)
		{

			int leftValue = expansionBoard[valueI, valueJ - 1];
			int crossLeftValue = expansionBoard[valueI + 1, valueJ - 1];
			int donwValue = expansionBoard[valueI + 1, valueJ];
			int crossRightValue = expansionBoard[valueI + 1, valueJ + 1];
			int rightValue = expansionBoard[valueI, valueJ + 1];
			int actualValue = expansionBoard[valueI, valueJ];
			int countHp = 0;

			if (leftValue == 1) countHp++;
			if (crossLeftValue == 1) countHp++;
			if (donwValue == 1) countHp++;
			if (crossRightValue == 1) countHp++;
			if (rightValue == 1) countHp++;

			if (actualValue == 1 && countHp < 2 || countHp > 3) actualValue = 0;
			else if (actualValue == 1 && countHp == 2 || countHp == 3) actualValue = 1;

			if (actualValue == 0 && countHp == 3) actualValue = 1;
			return actualValue;
		}

		public static int ValueBottomDifferentCorners(int[,] expansionBoard, int valueI, int valueJ)
		{

			int leftValue = expansionBoard[valueI, valueJ - 1];
			int crossLeftValue = expansionBoard[valueI - 1, valueJ - 1];
			int upValue = expansionBoard[valueI - 1, valueJ];
			int crossRightValue = expansionBoard[valueI - 1, valueJ + 1];
			int rightValue = expansionBoard[valueI, valueJ + 1];

			int actualValue = expansionBoard[valueI, valueJ];
			int countHp = 0;

			if (leftValue == 1) countHp++;
			if (crossLeftValue == 1) countHp++;
			if (upValue == 1) countHp++;
			if (crossRightValue == 1) countHp++;
			if (rightValue == 1) countHp++;

			if (actualValue == 1 && countHp < 2 || countHp > 3) actualValue = 0;
			else if (actualValue == 1 && countHp == 2 || countHp == 3) actualValue = 1;

			if (actualValue == 0 && countHp == 3) actualValue = 1;
			return actualValue;
		}

		public static int ValueLeftDifferentCorners(int[,] expansionBoard, int valueI, int valueJ)
		{

			int upValue = expansionBoard[valueI - 1, valueJ];
			int crossUpValue = expansionBoard[valueI - 1, valueJ + 1];
			int rightValue = expansionBoard[valueI, valueJ + 1];
			int crossDownValue = expansionBoard[valueI + 1, valueJ + 1];
			int downValue = expansionBoard[valueI + 1, valueJ];
			int actualValue = expansionBoard[valueI, valueJ];
			int countHp = 0;

			if (upValue == 1) countHp++;
			if (crossUpValue == 1) countHp++;
			if (rightValue == 1) countHp++;
			if (crossDownValue == 1) countHp++;
			if (downValue == 1) countHp++;

			if (actualValue == 1 && countHp < 2 || countHp > 3) actualValue = 0;
			else if (actualValue == 1 && countHp == 2 || countHp == 3) actualValue = 1;

			if (actualValue == 0 && countHp == 3) actualValue = 1;

			return actualValue;

		}

		public static int ValueRightDifferentCorners(int[,] expansionBoard, int valueI, int valueJ)
		{
			int upValue = expansionBoard[valueI - 1, valueJ];
			int crossUpLeft = expansionBoard[valueI - 1, valueJ - 1];
			int leftValue = expansionBoard[valueI, valueJ - 1];
			int crossDownLeft = expansionBoard[valueI + 1, valueJ - 1];
			int downValue = expansionBoard[valueI + 1, valueJ];
			int actualValue = expansionBoard[valueI, valueJ];
			int countHp = 0;

			if (upValue == 1) countHp++;
			if (crossUpLeft == 1) countHp++;
			if (leftValue == 1) countHp++;
			if (crossDownLeft == 1) countHp++;
			if (downValue == 1) countHp++;


			if (actualValue == 1 && countHp < 2 || countHp > 3) actualValue = 0;
			else if (actualValue == 1 && countHp == 2 || countHp == 3) actualValue = 1;

			if (actualValue == 0 && countHp == 3) actualValue = 1;

			return actualValue;

		}

		public static int ValueMidBoardDifferentCornersAndDifferentSides(int[,] expansionBoard, int valueI, int valueJ)
		{

			int crossTopLeft = expansionBoard[valueI - 1, valueJ - 1];
			int topSide = expansionBoard[valueI - 1, valueJ];
			int crossTopRight = expansionBoard[valueI - 1, valueJ + 1];

			int leftSide = expansionBoard[valueI, valueJ - 1];
			int rightSide = expansionBoard[valueI, valueJ + 1];

			int crossDownLeft = expansionBoard[valueI + 1, valueJ - 1];
			int downSide = expansionBoard[valueI + 1, valueJ];
			int crossDownRight = expansionBoard[valueI + 1, valueJ + 1];

			int actualValue = expansionBoard[valueI, valueJ];
			int countHp = 0;

			if (crossTopLeft == 1) countHp++;
			if (topSide == 1) countHp++;
			if (crossTopRight == 1) countHp++;
			if (leftSide == 1) countHp++;
			if (rightSide == 1) countHp++;
			if (crossDownLeft == 1) countHp++;
			if (downSide == 1) countHp++;
			if (crossDownRight == 1) countHp++;

			if (actualValue == 1 && countHp < 2 || countHp > 3) actualValue = 0;
			else if (actualValue == 1 && countHp == 2 || countHp == 3) actualValue = 1;

			if (actualValue == 0 && countHp == 3) actualValue = 1;

			return actualValue;
		}
	}
}
