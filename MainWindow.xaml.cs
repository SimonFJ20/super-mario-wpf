using System;
using System.Threading;
using System.Windows;
using System.Windows.Input;

namespace SuperMarioWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly Game game;
        private readonly Thread thread;
        private bool running;

        public MainWindow()
        {
            InitializeComponent();
            running = true;
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

        private void OnClose(object? sender, EventArgs a)
        {
            running = false;
            thread.Join();
            Application.Current.Shutdown();
        }

        private void GameLoop()
        {
            var before = DateTime.Now;
            while (running)
            {
                var deltaSeconds = (DateTime.Now - before).TotalSeconds;
                before = DateTime.Now;
                if (game.Tick(deltaSeconds) == false)
                    break;
                Dispatcher.Invoke(() => game.Draw());
                Thread.Sleep(16);
            }
            thread.Join();
        }
    }
}
