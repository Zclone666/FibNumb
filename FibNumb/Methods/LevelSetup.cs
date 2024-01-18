using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FibNumb.Consts;

namespace FibNumb.Methods
{
    public static class LevelSetup
    {
        public static void OnSliderValueChanged(uint value)
        {
            Consts.Main.level = value;
            Consts.Main.NmbOfSquares = value + 4;
            Main.FibNumbs = new uint[(int)Math.Pow(Main.NmbOfSquares, 2)];
            uint[] PreFib = new uint[(int)Math.Pow(Main.NmbOfSquares, 4)];
            for (int i = 0; i < PreFib.Length; i++)
            {
                if (i == 0) PreFib[i] = 0;
                else if (i == 1) PreFib[i] = 1;
                else
                {
                    PreFib[i] = PreFib[i - 2] + PreFib[i - 1];
                }
            }
            int rndSt = new Random().Next(10);

            Main.Map = new bool[(int)Math.Pow(Main.NmbOfSquares, 2)];

            Random rnd = new Random();
            for (int i = 0; i < Main.Map.Length; i++)
            {
                Main.Map[i] = (rnd.Next(10+(int)Main.level) == 1) ? true : false;
            }

            for (int i = 0; i < Main.FibNumbs.Length; i++)
            {
                Main.FibNumbs[i] = PreFib[rndSt + i];
            }
            Main.ScoreN = 0;
            Main.LivesN = 3;
            Main.NumbToF = Main.FibNumbs[4];// rndSt];
        }
    }
}
