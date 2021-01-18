using System;
using ResolveSudokus.Estratexias;
using ResolveSudokus.Traballadores;

namespace ResolveSudokus
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                SudokuMapeador sudokuMapeador = new SudokuMapeador();
                SudokuXestorEstadoTableiro sudokuXestorEstadoTableiro = new SudokuXestorEstadoTableiro();
                MotorDeSolucionsSudoku solucionsSudoku = new MotorDeSolucionsSudoku(sudokuXestorEstadoTableiro, sudokuMapeador);
                LectorArquivosSudoku sudokuLectorArquivos = new LectorArquivosSudoku();
                MostraTableiroSudoku mostraTableiroSudoku = new MostraTableiroSudoku();

                Console.WriteLine("Por favor, indica o nome do arquivo que conten o puzzle Sudoku");
                var nomeArquivo = Console.ReadLine();

                var tableiroSudoku = sudokuLectorArquivos.LerArquivo(nomeArquivo);
                mostraTableiroSudoku.Mostra("Estado inicial", tableiroSudoku);

                bool estaOSudokuResolto = solucionsSudoku.Resolver(tableiroSudoku);
                mostraTableiroSudoku.Mostra("Estado final", tableiroSudoku);

                Console.WriteLine(estaOSudokuResolto
                ? "Resolviches o Sudoku satisfactoriamente"
                : "Desafortunadamente os algoritmos usados non foron suficientes para resolver o Sudoku!");

            }
            catch (System.Exception ex)
            {
                //No mundo real querriamos facer un log deste tipo de mensaxes na base de datos
                Console.WriteLine("{0} : {1}", "Non se puido resolver o Sudoku porque houbo un erro", ex.Message);
            }
        }
    }
}
