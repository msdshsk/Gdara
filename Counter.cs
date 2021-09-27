using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gdara
{
    class Counter
    {
        private int count;
        private int firstNumber;
        public Counter(int firstNumber)
        {
            count = firstNumber;
            this.firstNumber = firstNumber;
        }

        public int up()
        {
            count++;
            return count;
        }

        public int down()
        {
            count--;
            return count;
        }

        public int get()
        {
            return count;
        }

        public void set(int i)
        {
            count = i;
        }

        public void reset()
        {
            count = firstNumber;
        }
    }
}
