using System.Threading;

namespace ProiectMIP2
{
    // A delegate egy típus, amely egy metódusra tud mutatni.
    // Ez olyan metódust fogad, amelynek nincs paramétere és nem ad vissza semmit.
    public delegate void DrawDelegate();

    public class MyThread
    {
        private Thread thread;
        private volatile bool running = false;

        private Form1 form;         // 1. változat: referencia a formra
        private DrawDelegate draw;  // 2. változat: delegate

        // 1. változat: a szál megkapja magát a formot
        public MyThread(Form1 form)
        {
            this.form = form;
        }

        // 2. változat: a szál csak egy metódust kap, a formról nem tud
        public MyThread(DrawDelegate draw)
        {
            this.draw = draw;
        }

        public void Start()
        {
            if (thread != null && thread.IsAlive)
                return;

            running = true;
            thread = new Thread(Run);
            thread.Start();
        }

        public void Stop()
        {
            running = false;
            if (thread != null && thread.IsAlive)
                thread.Join();
        }

        private void Run()
        {
            while (running)
            {
                if (form != null)
                    form.DrawText();   // hívás referencián keresztül
                else
                    draw();            // hívás delegate-en keresztül

                Thread.Sleep(100);
            }
        }
    }
}