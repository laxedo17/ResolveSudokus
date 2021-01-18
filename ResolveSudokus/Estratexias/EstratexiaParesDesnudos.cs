using System;
using System.Collections.Generic;
using System.Text;
using ResolveSudokus.Traballadores;

namespace ResolveSudokus.Estratexias
{
    public class EstratexiaParesDesnudos : IEstratexiaSudoku
    {
        private readonly SudokuMapeador _sudokuMapeador;

        /// <summary>
        /// Facemos Dependency Injection no constructor de novo.
        /// </summary>
        /// <param name="sudokuMapeador"></param>
        public EstratexiaParesDesnudos(SudokuMapeador sudokuMapeador)
        {
            _sudokuMapeador = sudokuMapeador;
        }
        public int[,] Resolver(int[,] tableiroSudoku)
        {

            for (int fila = 0; fila < tableiroSudoku.GetLength(0); fila++) //recolle a lonxitude das filas da 1ª dimension do array bidimensional
            {
                for (int columna = 0; columna < tableiroSudoku.GetLength(1); columna++) //GetLength(1) permitenos saber a lonxitude das columnas
                {
                    EliminarParesDesnudosEnOutrasFilas(tableiroSudoku, fila, columna);
                    EliminarParesDesnudosEnOutrasColumnas(tableiroSudoku, fila, columna);
                    EliminarParesDesnudosEnOutrosBloques(tableiroSudoku, fila, columna);
                }
            }

            return tableiroSudoku;
        }

        private void EliminarParesDesnudos(int[,] tableiroSudoku, int valoresAEliminar, int eliminarDeFila, int eliminarDeColumna)
        {
            var valoresAEliminarArray = valoresAEliminar.ToString().ToCharArray();

            foreach (var valorAEliminar in valoresAEliminarArray)
            {
                tableiroSudoku[eliminarDeFila, eliminarDeColumna] = Convert.ToInt32(tableiroSudoku[eliminarDeFila, eliminarDeColumna].ToString().Replace(valorAEliminar.ToString(), string.Empty));//se temos 123, quedara 12, e finalmente 1
            }
        }

        private void EliminarParesDesnudosEnOutrasFilas(int[,] tableiroSudoku, int filaDada, int columnaDada)
        {
            if (!TenParDesnudoNaFila(tableiroSudoku, filaDada, columnaDada))
            {
                return;
            }

            for (int columna = 0; columna < tableiroSudoku.GetLength(1); columna++)
            {
                if (tableiroSudoku[filaDada, columna] != tableiroSudoku[filaDada, columnaDada] && tableiroSudoku[filaDada, columna].ToString().Length > 1)
                {
                    EliminarParesDesnudos(tableiroSudoku, tableiroSudoku[filaDada, columnaDada], filaDada, columna);
                }
            }
        }

        private void EliminarParesDesnudosEnOutrasColumnas(int[,] tableiroSudoku, int filaDada, int columnaDada)
        {
            if (!TenParDesnudoNaColumna(tableiroSudoku, filaDada, columnaDada))
            {
                return;
            }

            for (int fila = 0; fila < tableiroSudoku.GetLength(0); fila++)
            {
                if (tableiroSudoku[fila, columnaDada] != tableiroSudoku[filaDada, columnaDada] && tableiroSudoku[fila, columnaDada].ToString().Length > 1)
                {
                    EliminarParesDesnudos(tableiroSudoku, tableiroSudoku[filaDada, columnaDada], fila, columnaDada);
                }
            }
        }


        private void EliminarParesDesnudosEnOutrosBloques(int[,] tableiroSudoku, int filaDada, int columnaDada)
        {
            if (!TenParDesnudoNoBloque(tableiroSudoku, filaDada, columnaDada))
            {
                return;
            }

            var mapaSudoku = _sudokuMapeador.Atopar(filaDada, columnaDada);

            for (int fila = mapaSudoku.FilaInicio; fila <= mapaSudoku.FilaInicio + 2; fila++) //recolle a lonxitude das filas da 1ª dimension do array bidimensional
            {
                for (int columna = mapaSudoku.ColumnaInicio; columna <= mapaSudoku.ColumnaInicio + 2; columna++) //GetLength(1) permitenos saber a lonxitude das columnas
                {
                    if (tableiroSudoku[fila, columna].ToString().Length > 1 && tableiroSudoku[fila, columna] != tableiroSudoku[filaDada, columnaDada])
                    {
                        EliminarParesDesnudos(tableiroSudoku, tableiroSudoku[filaDada, columnaDada], fila, columna);
                    }
                }
            }
        }

        private bool TenParDesnudoNaFila(int[,] tableiroSudoku, int filaDada, int columnaDada)
        {
            for (int columna = 0; columna < tableiroSudoku.GetLength(1); columna++)
            {
                if (columnaDada != columna && EsParDesnudo(tableiroSudoku[filaDada, columna], tableiroSudoku[filaDada, columnaDada]))
                {
                    return true;
                }
            }
            return false;
        }

        private bool TenParDesnudoNaColumna(int[,] tableiroSudoku, int filaDada, int columnaDada)
        {
            for (int fila = 0; fila < tableiroSudoku.GetLength(0); fila++)
            {
                if (filaDada != fila && EsParDesnudo(tableiroSudoku[fila, columnaDada], tableiroSudoku[filaDada, columnaDada]))
                {
                    return true;
                }
            }
            return false;
        }

        private bool TenParDesnudoNoBloque(int[,] tableiroSudoku, int filaDada, int columnaDada)
        {
            for (int fila = 0; fila < tableiroSudoku.GetLength(0); fila++)
            {
                for (int columna = 0; columna < tableiroSudoku.GetLength(1); columna++)
                {
                    var mismoElemento = filaDada == fila && columnaDada == columna;
                    var elementoNoMismoBloque = _sudokuMapeador.Atopar(filaDada, columnaDada).FilaInicio == _sudokuMapeador.Atopar(fila, columna).FilaInicio &&
                    _sudokuMapeador.Atopar(filaDada, columnaDada).ColumnaInicio == _sudokuMapeador.Atopar(fila, columna).ColumnaInicio;

                    if (!mismoElemento && elementoNoMismoBloque && EsParDesnudo(tableiroSudoku[filaDada, columnaDada], tableiroSudoku[fila, columna]))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private bool EsParDesnudo(int primeiroPar, int segundoPar)
        {
            return primeiroPar.ToString().Length == 2 && primeiroPar == segundoPar;
        }
    }
}
