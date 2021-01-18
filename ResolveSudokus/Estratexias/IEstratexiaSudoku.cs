using System;
using System.Collections.Generic;
using System.Text;

namespace ResolveSudokus.Estratexias
{
    /// <summary>
    /// Estratexias necesarias aplicadas de maneira abstracta para resolver o Sudoku, que o usuario final non ten porque coñecer
    /// </summary>
    public interface IEstratexiaSudoku
    {
        int[,] Resolver(int[,] tableiroSudoku);
    }
}
