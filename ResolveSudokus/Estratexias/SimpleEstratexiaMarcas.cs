using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ResolveSudokus.Datos;
using ResolveSudokus.Traballadores;

namespace ResolveSudokus.Estratexias
{
    public class SimpleEstratexiaMarcas : IEstratexiaSudoku
    {

        private readonly SudokuMapeador _sudokuMapa;

        /// <summary>
        /// Usamos Dependency Injection para agregar esa dependencia.
        /// </summary>
        /// <param name="sudokuMapa"></param>
        public SimpleEstratexiaMarcas(SudokuMapeador sudokuMapa)
        {
            _sudokuMapa = sudokuMapa;
        }
        public int[,] Resolver(int[,] tableiroSudoku)
        {
            for (int fila = 0; fila < tableiroSudoku.GetLength(0); fila++) //recolle lonxitude filas da 1ª dimension do array bidimensional. GetLength(0) recorre filas
            {
                for (int columna = 0; columna < tableiroSudoku.GetLength(1); columna++) //GetLength(1) permitenos saber a lonxitude das columnas
                {
                    if (tableiroSudoku[fila, columna] == 0 || tableiroSudoku[fila, columna].ToString().Length > 1)
                    {
                        var posibilidadesEnFilaColumna = GetPosibilidadesEnFilaColumna(tableiroSudoku, fila, columna);
                        var posibilidadesEnBloque = GetPosiblidadesEnBloque(tableiroSudoku, fila, columna);
                        tableiroSudoku[fila, columna] = GetPosibilidadesEnInterseccion(posibilidadesEnFilaColumna, posibilidadesEnBloque);
                    }
                }
            }
            return tableiroSudoku;
        }

        /// <summary>
        /// Obten unha lista de posibilidades de numeros nunha fila e columna, para saber se un numero se pode usar nunha celda determinada. Os numeros xa existentes nas celdas transformaos nun 0 para descartalos, e os numeros de 1 a 9 que deberian estar nas celdas baleiras para encher o Sudoku e non foron transformados a 0, xuntanse nun numero unico
        /// </summary>
        /// <param name="tableiroSudoku"></param>
        /// <param name="fila"></param>
        /// <param name="columna"></param>
        /// <returns>Devolve unha cadena cos numeros que non existen na columna e convirteos de string a un numero unico -por ex. 1267, 5321 etc etc-</returns>
        private int GetPosibilidadesEnFilaColumna(int[,] tableiroSudoku, int filaDada, int columnaDada)
        {
            int[] posibilidades = { 1, 2, 3, 4, 5, 6, 7, 8, 9 };//os numeros posibles en cada casilla dun taboleiro Sudoku

            for (int columna = 0; columna < 9; columna++)
            {
                if (EsValidaSimple(tableiroSudoku[filaDada, columna]))//comproba k o numero e valido, e para a fila dada recorre todas as columnas de numeros desa fila
                {
                    posibilidades[tableiroSudoku[filaDada, columna] - 1] = 0;//o -1 e porque as posibilidades van de 1 a 9 pero un array de elementos empeza en 0. Enton a posibilidade 3 -numero 3- no array representa o 4º valor dun array, e para obter un 2 temos que restar 1 a 3 -valor nas posibilidades, pero non e o 2º valor no array-, e se por exemplo hai un 2 nunha casilla sabemos que e o 2º valor das posibilidades
                    //habendo un valor, por ex. 2, o que fai isto e que o transforma en 0 e sabe que xa non e unha posibilidade, ese numero esta collido
                }
            }

            for (int fila = 0; fila < 9; fila++)
            {
                if (EsValidaSimple(tableiroSudoku[fila, columnaDada]))//como antes, neste caso para cada columna comproba os numeros das filas que pasan por esa columna. Se hai un 9 nunha columna, o resultado sera que no array o 8º valor do array se transformara en 0, indicando que o 9 esta collido, transformandoo en 0 para que despois non o poidamos convertir
                {
                    posibilidades[tableiroSudoku[fila, columnaDada] - 1] = 0;
                }
            }

            return Convert.ToInt32(String.Join(string.Empty, posibilidades.Select(p => p).Where(p => p != 0)));//String.Join pon xuntos todos os numeros obtidos, os que non estaban postos. E dicir, se nunha fila ou nunha columna, habia un 2 e un 9 como se mencionou, o transformalos en 0 obtemos unha lista de posibilidades que daria un string asi "1345678" (faltan o 2 e o 9, que xa non son posibilidades porque xa estaban postos). Ese string obtido con Convert pasamolo a un Numero

        }   

        /// <summary>
        /// Obten as posibilidades de que un numero se atope nun minibloque de 9 espacios (3x3) e situalo correctamente
        /// </summary>
        /// <param name="tableiroSudoku"></param>
        /// <param name="fila"></param>
        /// <param name="columna"></param>
        /// <returns></returns>
        private int GetPosiblidadesEnBloque(int[,] tableiroSudoku, int filaDada, int columnaDada)
        {
            int[] posibilidades = { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

            var mapaSudoku = _sudokuMapa.Atopar(filaDada, columnaDada);

            for (int fila = mapaSudoku.FilaInicio; fila <= mapaSudoku.FilaInicio + 2; fila++)//busca valores en cada fila entre o punto 0 (inicio fila) e o punto 2(3ª valor dun bloque nunha fila, por iso o +2). Recorrendo as filas 0, 1, e 2 dun bloque
            {
                for (int columna = mapaSudoku.ColumnaInicio; columna <= mapaSudoku.ColumnaInicio + 2; columna++)//recorrre as columnas 0, 1 e 2 dun bloque . <= para que inclua a ultima linea, senon con > solo, poñemos +3 para solucionar este posible bug
                {
                    if (EsValidaSimple(tableiroSudoku[fila, columna]))
                    {
                        posibilidades[tableiroSudoku[fila, columna] - 1] = 0;//igual que antes, eliminamos da lista de valores posibles os xa existentes nese bloque 3x3, transformandoos a 0, e os que non esten pasaran a devolverse nun unico numero
                    }
                }
            }

            return Convert.ToInt32(String.Join(string.Empty, posibilidades.Select(p => p).Where(p => p != 0)));
        }

        /// <summary>
        /// Traballa na interseccion dos anteriores metodos, para que os numeros nas filas e columnas e os numeros nos bloques non se repitan, e asi resolver o Sudoku se e posible que sexa resolto
        /// </summary>
        /// <param name="posibilidadesEnFilaColumna"></param>
        /// <param name="posibilidadesEnBloque"></param>
        /// <returns></returns>
        private int GetPosibilidadesEnInterseccion(int posibilidadesEnFilaColumna, int posibilidadesEnBloque)
        {
            var posibilidadesFilaColumnaArrayChars = posibilidadesEnFilaColumna.ToString().ToCharArray();
            var posibilidadesBloqueArrayChars = posibilidadesEnBloque.ToString().ToCharArray();
            var subconxuntoDePosibilidades = posibilidadesFilaColumnaArrayChars.Intersect(posibilidadesBloqueArrayChars);//usamos LINQ para facer intersect

            return Convert.ToInt32(string.Join(string.Empty, subconxuntoDePosibilidades));
        }
        //Exemplo do que fai Intersect

        /*
        |9| | |2|3|7|6|8| |
        | |2| |8|4| | |7|3|
        |8| |7|1| |5| |2|9|
        | | |4|5|9|8|3| | |
        | | | | | |1| | |6|
        | | | | | | | |4|7|
        |4| |1|3| |6|2|9|5|
        | |5| |9|1| |7|3|8|
        |3| |8| |5| | | | |

        Valores posibles
        filaEColumna(1ª fila, 2ª columna)= 123456789
        Grupode3x3: 123456789

        Valores que hai que descartar
        filaEColumna(1ª fila, 2ª columna): 2593768
        Grupode3x3(Bloque superior esquerda): 9287 no grupo de 3x3 estan ocupados e temos que descartalos 

        Valores finales que si podemos usar para cubrir os espacios
        filaEColumna (1ª fila, 2ª columna): 14 -o que ten menos posibilidades, e o que vai a limitar mais os valores posibles-
        Grupo3x3(bloque superior esquerda): 13456

        INTERSECCION vai ser
        14
        Hai 5 ocos libres no bloque de arriba a esquerda, e na columna 2 podemos poñer o 1 e o 4, no lugar adecuado
        */

        /// <summary>
        /// Cando iteramos polas filas e columnas comproba que o valor non e 0 nin os numeros son superiores a 9 (menos de 2 cifras) nin hai numeros negativos, o signo - e o numero ocupan dous espacios.
        /// </summary>
        /// <param name="dixitoDaCelda"></param>
        /// <returns></returns>
        private bool EsValidaSimple(int dixitoDaCelda)
        {
            return dixitoDaCelda != 0 && dixitoDaCelda.ToString().Length < 2; //tamen valeria return dixitoDaCelda != 0 && dixitoDaCelda.ToString().Length == 1
        }

    }
}
