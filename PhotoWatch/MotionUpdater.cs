using System;
using Tizen.Sensor;

namespace PhotoWatch
{
    public class MotionUpdater
    {
        Gyroscope _gyroscope;
        ClockViewModel _model;

        int xResetCounter = 0;
        int yResetCounter = 0;


        public MotionUpdater(ClockViewModel model)
        {
            if (!Gyroscope.IsSupported)
            {
                return;
            }
            _model = model;
            _gyroscope = new Gyroscope();
            _gyroscope.DataUpdated += DataUpdated;
        }

        public void Start()
        {
            _gyroscope.Start();
        }
        public void Stop()
        {
            _model.BGTranslationX = 0;
            _model.BGTranslationY = 0;
            _gyroscope.Stop();
        }

        void DataUpdated(object sender, GyroscopeDataUpdatedEventArgs e)
        {
            if (Math.Abs(e.Y) > 1 && Math.Abs(e.Y) < 300)
            {
                var newvalue = _model.BGTranslationX + e.Y / 25.0;
                _model.BGTranslationX = Math.Clamp(newvalue, -25, 25);

                if (e.Y / 10.0 > 1)
                    xResetCounter = 0;
            }
            else
            {
                xResetCounter++;
                if (xResetCounter > 15)
                {
                    var newvalue = _model.BGTranslationX - _model.BGTranslationX / 10.0;
                    _model.BGTranslationX = Math.Abs(newvalue) < 3 ? 0 : newvalue;
                }
            }
            if (Math.Abs(e.X) > 1 && Math.Abs(e.X) < 300)
            {
                var newvalue = _model.BGTranslationY + e.X / 25.0;
                _model.BGTranslationY = Math.Clamp(newvalue, -25, 25);

                if (e.X / 10.0 > 1)
                    yResetCounter = 0;
            }
            else
            {
                yResetCounter++;
                if (yResetCounter > 15)
                {
                    var newvalue = _model.BGTranslationY - _model.BGTranslationY / 10.0;
                    _model.BGTranslationY = Math.Abs(newvalue) < 3 ? 0 : newvalue;
                }
            }
        }
    }
}
