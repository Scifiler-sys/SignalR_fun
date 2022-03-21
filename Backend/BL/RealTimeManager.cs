namespace BL
{
    /// <summary>
    /// Simple class that will emulate a real-time environment by sending data every 2 seconds
    /// </summary>
    public class RealTimeManager
    {
        private Timer _timer;
        private AutoResetEvent _autoRes;
        private Action _action;
        public static Boolean alreadySending = false;

        public DateTime TimerTime { get; }

        public RealTimeManager(Action p_action)
        {
            //Setting up actions
            _action = p_action;
            _autoRes = new AutoResetEvent(false);

            //Will execute our action delegate that we pass in the constructor every 2 seconds with a 1 second delay
            _timer = new Timer(Execute, _autoRes, 1000,2000);
            alreadySending = true;
            TimerTime = DateTime.Now;
        }

        public void Execute(object stateInfo)
        {
            _action();
            

            //Will remove timer after 60 seconds
            if((DateTime.Now - TimerTime).Seconds > 60)
            {
                _timer.Dispose();
                alreadySending = false;
            }
        }
    }
}