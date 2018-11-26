using FlowCheker.Interface;
using FlowCheker.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace FlowCheker.Controller
{
    public class ResultWriterController<T> where T : IResultWriter
    {
        private T resultWriter;
        private Queue<MeasurementResult> resultQueue;
        private Timer timer;
        private bool isTimerRunning;

        public ResultWriterController(T resultWriter, double timeout)
        {
            this.resultWriter = resultWriter;
            resultQueue = new Queue<MeasurementResult>();
            timer = new Timer(timeout);
        }

        public void Start()
        {
            if (isTimerRunning)
                return;

            isTimerRunning = true;
            timer.Elapsed += Timer_Elapsed;
            timer.Start();
        }

        public void Stop()
        {
            timer.Stop();
            isTimerRunning = false;
        }

        public void AddToQueue(MeasurementResult result)
        {
            if (result != null)
                resultQueue.Enqueue(result);
        }

        private void WriteQueue()
        {
            while(resultQueue.Count > 0)
            {
                MeasurementResult result = resultQueue.Dequeue();
                resultWriter.Write(result);
            }
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            WriteQueue();
        }
    }
}
