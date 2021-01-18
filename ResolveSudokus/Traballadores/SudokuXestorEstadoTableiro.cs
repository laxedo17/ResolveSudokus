using System;
using System.Collections.Generic;
using System.Text;

namespace ResolveSudokus.Traballadores
{
    public class SudokuXestorEstadoTableiro
    {
        /// <summary>
        /// Como se ve o taboleiro, despois de facer unha estratexia. 
        /// </summary>
        /// <param name="tableiroSudoku"></param>
        /// <returns></returns>
        public string XerarEstado(int[,] tableiroSudoku)
        {
            StringBuilder clave = new StringBuilder();

            for (int fila = 0; fila < tableiroSudoku.GetLength(0); fila++) //recolle a lonxitude das filas da 1ª dimension do array bidimensional
            {
                for (int columna = 0; columna < tableiroSudoku.GetLength(1); columna++) //GetLength(1) permitenos saber a lonxitude das columnas
                {
                    clave.Append(tableiroSudoku[fila, columna]);
                }
            }

            return clave.ToString();
        }

        /// <summary>
        /// Veremos como queda o taboleiro e saber se esta resolto ou non.
        /// </summary>
        /// <param name="tableiroSudoku"></param>
        /// <returns></returns>
        public bool EstaResolto(int[,] tableiroSudoku)
        {
            for (int fila = 0; fila < tableiroSudoku.GetLength(0); fila++) //recolle a lonxitude das filas da 1ª dimension do array bidimensional
            {
                for (int columna = 0; columna < tableiroSudoku.GetLength(1); columna++) //GetLength(1) permitenos saber a lonxitude das columnas
                {
                    if (tableiroSudoku[fila, columna] == 0 || tableiroSudoku[fila, columna].ToString().Length > 1) //se un elemento e un 0, como a clase LectorArquivo fai, k os transforma a 0
                    { //enton e falso. Tb devolve falso se hai varios numeros onde deberia haber un
                        return false;
                    }
                }
            }
            return true;
        }
    }
}
