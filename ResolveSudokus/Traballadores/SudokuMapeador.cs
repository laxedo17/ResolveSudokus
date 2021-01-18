using ResolveSudokus.Datos;

using System;
using System.Collections.Generic;
using System.Text;

namespace ResolveSudokus.Traballadores
{
    public class SudokuMapeador
    {
        /// <summary>
        /// Atopa en que bloque 3x3 do taboleiro Sudoku esta unha determinada fila e columna que se xuntan, para que saibamos en que lugar estamos actuando
        /// </summary>
        /// <param name="filaDada"></param>
        /// <param name="columnaDada"></param>
        /// <returns></returns>
        public SudokuMapa Atopar(int filaDada, int columnaDada)
        {
            SudokuMapa mapaSudoku = new SudokuMapa();

            if ((filaDada >= 0 && filaDada <= 2) && (columnaDada >= 0 && columnaDada <= 2)) //asi sabemos que estamos na seccion inicial do Sudoku, a des que arriba á esquerda [0][0][0][1][0][2][1][0][1][1][1][2][2][0][2][1][2][2]
            {
                mapaSudoku.FilaInicio = 0;
                mapaSudoku.ColumnaInicio = 0;
            }
            else if((filaDada >= 0 && filaDada <= 2) && (columnaDada >= 3 && columnaDada <= 5)) //parte superior central do Sudoku
            {
                mapaSudoku.FilaInicio = 0; //segue na mismas filas iniciais, kedan columnas
                mapaSudoku.ColumnaInicio = 3;
            }
            else if ((filaDada >= 0 && filaDada <= 2) && (columnaDada >= 6 && columnaDada <= 8)) //parte superior dereita do Sudoku
            {
                mapaSudoku.FilaInicio = 0;
                mapaSudoku.ColumnaInicio = 6;
            }
            else if ((filaDada >= 3 && filaDada <= 5) && (columnaDada >= 0 && columnaDada <= 2)) //parte central esquerda do Sudoku
            {
                mapaSudoku.FilaInicio = 3;
                mapaSudoku.ColumnaInicio = 0;
            }
            else if ((filaDada >= 3 && filaDada <= 5) && (columnaDada >= 3 && columnaDada <= 5)) //parte central, centro total do Sudoku
            {
                mapaSudoku.FilaInicio = 3;
                mapaSudoku.ColumnaInicio = 3;
            }
            else if ((filaDada >= 3 && filaDada <= 5) && (columnaDada >= 6 && columnaDada <= 8)) //parte central dereita do Sudoku
            {
                mapaSudoku.FilaInicio = 3;
                mapaSudoku.ColumnaInicio = 6;
            }
            else if ((filaDada >= 6 && filaDada <= 8) && (columnaDada >= 0 && columnaDada <= 2)) //parte inferior esquerda do Sudoku
            {
                mapaSudoku.FilaInicio = 6;
                mapaSudoku.ColumnaInicio = 0;
            }
            else if ((filaDada >= 6 && filaDada <= 8) && (columnaDada >= 3 && columnaDada <= 5)) //parte inferior central do Sudoku
            {
                mapaSudoku.FilaInicio = 6;
                mapaSudoku.ColumnaInicio = 3;
            }
            else if ((filaDada >= 6 && filaDada <= 8) && (columnaDada >= 6 && columnaDada <= 8)) //parte inferior dereita do sudoku
            {
                mapaSudoku.FilaInicio = 6;
                mapaSudoku.ColumnaInicio = 6;
            }

            return mapaSudoku;
        }
    }
}
