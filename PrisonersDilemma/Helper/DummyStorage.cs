using PrisonersDilemma.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrisonersDilemma.Helper
{
    internal class DummyStorage
    {
        public List<ResultMessage> Data { get; private set; }


        private static DummyStorage instance;
        private DummyStorage() 
        { 
            Data = new List<ResultMessage>();
        }
        public static DummyStorage Instance
        {
            get
            {
                if (instance == null)
                    instance = new DummyStorage();
                return instance;
            }
        }

        private object locker = new object();
        public void AddData(ResultMessage msg)
        {
            lock (locker)
            {
                //Console.WriteLine(Data.Count());
                Data.Add(msg);
            }
        }
    }
}
