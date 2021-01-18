using System;
using System.Collections.Generic;
using System.Text;

namespace ResolveSudokus.Traballadores
{
    public class MostraTableiroSudoku
    {
        public void Mostra(string titulo, int[,] tableiroSudoku)
        {
            if (!titulo.Equals(string.Empty)) //se titulo non esta baleiro
            {
                Console.WriteLine("{0} {1}", titulo, Environment.NewLine); //crea nova linea co titulo e un salto de linea extra -Environment.NewLine-
            }

            for (int fila = 0; fila < tableiroSudoku.GetLength(0); fila++) //recolle a lonxitude das filas da 1ª dimension do array bidimensional
            {
                Console.Write("|");//simbolo para o inicio de cada fila/columna na propia liña
                for (int columna = 0; columna < tableiroSudoku.GetLength(1); columna++) //GetLength(1) permitenos saber a lonxitude das columnas
                {
                    Console.Write("{0}{1}", tableiroSudoku[fila, columna], "|");//a columna e fila especifica na que estamos imprimese e xusto despois poñemos un | entre cada elemento impreso
                }
                Console.WriteLine();//terminada cada columna ata o final, saltamos unha liña, co cal estamos na seguinte fila, e asi sucesivamente
            }
            Console.WriteLine();//xeneramos mais espacio se necesitamos un pouco de espacio
        }
    }
}
