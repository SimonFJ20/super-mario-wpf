using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SuperMarioWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly Game game;
        private readonly Thread thread;

        public MainWindow()
        {
            InitializeComponent();
            game = new Game();
            game.Load(canvas);
            thread = new Thread(GameLoop);
            thread.Start();

        }

        private void OnKeyPress(object sender, KeyEventArgs a)
        {
            game.keyboard.Set(a.Key, KeyStates.Down);
        }

        private void OnKeyRelease(object sender, KeyEventArgs a)
        {
            game.keyboard.Set(a.Key, KeyStates.None);
        }

        private void GameLoop()
        {
            var before = DateTime.Now;
            while (true)
            {
                var deltaTS = (DateTime.Now - before).TotalSeconds;
                before = DateTime.Now;
                if (game.Tick(deltaTS) == false)
                    break;
                game.Draw(this.Dispatcher);
                Thread.Sleep(16);
            }
            thread.Join();
        }
    }
}
