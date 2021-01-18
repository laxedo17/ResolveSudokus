using Microsoft.VisualStudio.TestTools.UnitTesting;
using ResolveSudokus.Estratexias;
using ResolveSudokus.Traballadores;
using System;
using System.Collections.Generic;
using System.Text;

namespace ResolveSudokus.Test.Unit.Estratexias
{
    [TestClass]
    public class TestEstratexiaParesDesnudos
    {
        private readonly IEstratexiaSudoku _estratexiaParesDesnudos = new EstratexiaParesDesnudos(new SudokuMapeador());

        [TestMethod]
        public void DeberiaEliminarNumerosEnFilasBasadasEnParesDesnudos()
        {
            int[,] tableiroSudoku =
            {
                { 1, 2, 34, 5 , 34, 6, 7, 348, 9},
                { 0, 0, 0, 0, 0, 0, 0, 0, 0},
                { 0, 0, 0, 0, 0, 0, 0, 0, 0},
                { 0, 0, 0, 0, 0, 0, 0, 0, 0},
                { 0, 0, 0, 0, 0, 0, 0, 0, 0},
                { 0, 0, 0, 0, 0, 0, 0, 0, 0},
                { 0, 0, 0, 0, 0, 0, 0, 0, 0},
                { 0, 0, 0, 0, 0, 0, 0, 0, 0},
                { 0, 0, 0, 0, 0, 0, 0, 0, 0},
            };

            var taboleiroSudokuResolto = _estratexiaParesDesnudos.Resolver(tableiroSudoku);

            Assert.IsTrue(taboleiroSudokuResolto[0, 7] == 8);
        }

        [TestMethod]
        public void DeberiaEliminarNumerosEnColumnasBasadasEnParesDesnudos()
        {
            int[,] tableiroSudoku =
            {
                { 1, 0, 0, 0 , 0, 0, 0, 0, 0},
                { 24, 0, 0, 0, 0, 0, 0, 0, 0},
                { 3, 0, 0, 0, 0, 0, 0, 0, 0},
                { 5, 0, 0, 0, 0, 0, 0, 0, 0},
                { 6, 0, 0, 0, 0, 0, 0, 0, 0},
                { 24, 0, 0, 0, 0, 0, 0, 0, 0},
                { 7, 0, 0, 0, 0, 0, 0, 0, 0},
                { 8, 0, 0, 0, 0, 0, 0, 0, 0},
                { 249, 0, 0, 0, 0, 0, 0, 0, 0}
            };

            var taboleiroSudokuResolto = _estratexiaParesDesnudos.Resolver(tableiroSudoku);

            Assert.IsTrue(taboleiroSudokuResolto[8, 0] == 9);
        }

        [TestMethod]
        public void DeberiaEliminarNumerosEnBloque1BasadosEnParesDesnudos()
        {
            int[,] tableiroSudoku =
            {
                { 1, 2, 3, 0 , 0, 0, 0, 0, 0},
                { 45, 6, 7, 0, 0, 0, 0, 0, 0},
                { 8, 45, 459, 0, 0, 0, 0, 0, 0},
                { 0, 0, 0, 0, 0, 0, 0, 0, 0},
                { 0, 0, 0, 0, 0, 0, 0, 0, 0},
                { 0, 0, 0, 0, 0, 0, 0, 0, 0},
                { 0, 0, 0, 0, 0, 0, 0, 0, 0},
                { 0, 0, 0, 0, 0, 0, 0, 0, 0},
                { 0, 0, 0, 0, 0, 0, 0, 0, 0},
            };

            var taboleiroSudokuResolto = _estratexiaParesDesnudos.Resolver(tableiroSudoku);

            Assert.IsTrue(taboleiroSudokuResolto[2, 2] == 9);
        }

        [TestMethod]
        public void DeberiaEliminarNumerosEnBloque5BasadosEnParesDesnudos()
        {
            int[,] tableiroSudoku =
            {
                { 0, 0, 0, 0, 0, 0, 0, 0, 0},
                { 0, 0, 0, 0, 0, 0, 0, 0, 0},
                { 0, 0, 0, 0, 0, 0, 0, 0, 0},
                { 0, 0, 0, 7, 8, 9, 0, 0, 0},
                { 0, 0, 0, 12, 3, 4, 0, 0, 0},
                { 0, 0, 0, 6, 12, 125, 0, 0, 0},
                { 0, 0, 0, 0, 0, 0, 0, 0, 0},
                { 0, 0, 0, 0, 0, 0, 0, 0, 0},
                { 0, 0, 0, 0, 0, 0, 0, 0, 0},
            };

            var taboleiroSudokuResolto = _estratexiaParesDesnudos.Resolver(tableiroSudoku);

            Assert.IsTrue(taboleiroSudokuResolto[5, 5] == 5);
        }

        [TestMethod]
        public void DeberiaEliminarNumerosEnBloque9BasadosEnParesDesnudos()
        {
            int[,] tableiroSudoku =
            {
                { 0, 0, 0, 0, 0, 0, 0, 0, 0},
                { 0, 0, 0, 0, 0, 0, 0, 0, 0},
                { 0, 0, 0, 0, 0, 0, 0, 0, 0},
                { 0, 0, 0, 0, 0 ,0, 0, 0, 0},
                { 0, 0, 0, 0, 0, 0, 0, 0, 0},
                { 0, 0, 0, 0, 0, 0, 0, 0, 0},
                { 0, 0, 0, 0, 0, 0, 1, 2, 3},
                { 0, 0, 0, 0, 0, 0, 4, 56, 56},
                { 0, 0, 0, 0, 0, 0, 567, 8, 9},
            };

            var taboleiroSudokuResolto = _estratexiaParesDesnudos.Resolver(tableiroSudoku);

            Assert.IsTrue(taboleiroSudokuResolto[8, 6] == 7);
        }
    }
}
