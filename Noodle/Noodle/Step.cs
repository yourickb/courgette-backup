using System;
using System.Collections.Generic;
using System.Text;

namespace Noodle
{
    public class Step
    {
        private Step _prev;

        public Step()
        {

        }

        public void SetPrevious(Step s)
        {
            _prev = s;
        }

        public void Log(string message)
        {
            // log.txt
        }

        public void Display(string message)
        {
            Console.WriteLine(message);
        }

        public virtual void Show()
        {
            throw new NotImplementedException("Let op: deze functie overschrijven");
        }

        public void Back()
        {
            Log("[Back]");

            if (_prev != null)
            {
                _prev.Show();
            }
        }
    }
}
