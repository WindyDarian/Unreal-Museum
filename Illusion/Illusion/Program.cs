using System;
using System.Windows.Forms;

namespace Illusion
{
#if WINDOWS || XBOX

    static class Program
    {
        public static bool gamebool = false;

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(true);
            Application.Run(new UI());
            if (gamebool)
            {
                IllusionMain game = new IllusionMain();
                game.Run();
            }
        }
    }
#endif
}