using ResolveSudokus.Traballadores;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ResolveSudokus.Estratexias
{
    public class MotorDeSolucionsSudoku
    {
        private readonly SudokuXestorEstadoTableiro _sudokuXestorEstadoTableiro;
        private readonly SudokuMapeador _sudokuMapeador;

        /// <summary>
        /// Constructor usa Dependency Injection. Inxectamos as clases SudokuXestorEstadoTableiro e SudokuMapeador
        /// </summary>
        public MotorDeSolucionsSudoku(SudokuXestorEstadoTableiro sudokuXestorEstadoTableiro, SudokuMapeador sudokuMapeador)
        {
            _sudokuXestorEstadoTableiro = sudokuXestorEstadoTableiro;
            _sudokuMapeador = sudokuMapeador;
        }

        /// <summary>
        /// Resolve os sudokus, inicializando as estratexias para o taboleiro recibido
        /// </summary>
        /// <param name="tableiroSudoku"></param>
        /// <returns></returns>
        public bool Resolver(int[,] tableiroSudoku)
        {
            //inicializa as estratexias
            List<IEstratexiaSudoku> estratexias = new List<IEstratexiaSudoku>()
            {
                new SimpleEstratexiaMarcas(_sudokuMapeador),
                new EstratexiaParesDesnudos(_sudokuMapeador)
            };

            var estadoActual = _sudokuXestorEstadoTableiro.XerarEstado(tableiroSudoku);
            var seguinteEstado = _sudokuXestorEstadoTableiro.XerarEstado(estratexias.First().Resolver(tableiroSudoku));//collemos o 1º elemento e polo "contrato" coa interfaz debemos ter un metodo Resolver. Se hai 60 estratexias, ira 1 por 1 ata resolver
            //Unha vez resolto, devolvemos un taboleiro con unha estratexia Sudoku, enton xeramos un estado con un novo taboleiro, e obtemos un novo estado que sera o seguinteEstado

            //Se con un solo movemento anterior resolvemos o taboleiro, xa non entra no loop porque esta resolto, e a 1ª condicion do loop e que non este resolto, a seguinte e que se o estadoActual e diferente o seguinte estado, significa que fixo algo co taboleiro ainda asi
            while (!_sudokuXestorEstadoTableiro.EstaResolto(tableiroSudoku) && estadoActual != seguinteEstado)
            {
                estadoActual = seguinteEstado;//cando se cumple as condicions do loop, o estadoActual vai ser igual ao seguinte estado, e asi sucesivamente ata Resolver
                foreach (var estratexia in estratexias) //recorremos todas as estratexias necesarias unha por unha da lista de estratexias
                {
                    seguinteEstado = _sudokuXestorEstadoTableiro.XerarEstado(estratexia.Resolver(tableiroSudoku));//agora o seguinte estado vai ser igual a nova estratexia que resolve o taboleiro Sudoku
                }
            }

            //o loop funciona ata que está resolto, ou dase conta de que non se pode resolver
            return _sudokuXestorEstadoTableiro.EstaResolto(tableiroSudoku);
        }

    }
}
