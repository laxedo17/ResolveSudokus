using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ResolveSudokus.Traballadores
{
    public class LectorArquivosSudoku
    {
        public int[,] LerArquivo(string nomeArquivo)
        {
            int[,] taboleiroSudoku = new int[9, 9]; //taboleiro de 9 filas x 9 columnas tipico do Sudoku

            try
            {
                var lineasTaboleiroSudoku = File.ReadAllLines(nomeArquivo);

                int fila = 0;//empezamos na 1ª fila, que sera a fila 0
                foreach (var lineaTaboleiroSudoku in lineasTaboleiroSudoku)
                {
                    // |9| | |2|3|7|6|8| | . Exemplo de linea Sudoku. Con Split obteremos a seguinte liña: "", "9", " ", " ", "2", "3", "7", "6", "8", " ",""
                    //Split transforma o delimitador | nisto "" un string sin espacios nin nada, que colle todo ata o final da liña a dereita. E ao final a dereita intenta facer o mesmo pero non hai nada mais a dereita
                    //Con Split colle 9 espacios, os nove numeros, teñan numero ou anque esten valeiros
                    string[] elementosDaLinea = lineaTaboleiroSudoku.Split('|').Skip(1).Take(9).ToArray();

                    int columna = 0;//columna 0, primeira na que comenzamos- Elemento do array [0][0] (fila 0, columna 0) , de ahi pasamos a elementos [0][1], [0][2] etc
                    foreach (var elementoDaLinea in elementosDaLinea) //de ahi imos elemento por elemento das columnas ata que non queden mais columnas
                    {
                        taboleiroSudoku[fila, columna] = elementoDaLinea.Equals(" ") ? 0 : Convert.ToInt16(elementoDaLinea); //onde haxa un espacio baleiro poñemos un 0, senon convertimos a un enteiro
                        columna++; //pasamos a seguinte columna, de principio a fin [0][1] [0][2] [0][3] [0][4] [0][5] [0][6 ][0][7] [0][8] [0][9] ,e terminadas as columnas, seguinte linea
                    }
                    fila++;//terminados todos os elementos de cada columna, pasamos a seguinte linea e volvemos a recorrer todas as columnas nesa linea (no bucle anterior), ata terminar as filas tamen
                }
            }
            catch (Exception ex)
            {

                throw new Exception("Houbo algun erro tratando de ler o arquivo. " + ex.Message);
            }

            return taboleiroSudoku;
        }
    }
}
