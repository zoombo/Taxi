using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS_Taxi
{
    /// <summary>
    /// Эвакуатор. 
    /// </summary>
    [Serializable]
    public class Wrecker : CarIface
    {
        //public bool IsRunCatch { get; protected set; } = false; // Флаг говорящий о том что авто на вызове. Ловит сука.
        public bool IsBrokenRun { get; set; } = false; // Сломан но ездит.
        public bool IsBrokenNoRun { get; set; } = false; // Сломан и не ездит.

        public override void Run()
        {
            if (this.IsRun )
            {
                RunException carIsRunningException = new RunException(RunException.type.AlreadyRunning);
                throw carIsRunningException;
            }
            else if (!this.IsRun)
            {
                RunException noSetDriver = new RunException(RunException.type.NoDriver);
                throw noSetDriver;
            }
            else if (this.IsBrokenNoRun || this.IsBrokenRun)
            {
                RunException carIsBroken = new RunException(RunException.type.IsBroken);
                throw carIsBroken;
            }
            this.IsRun= true;
        }
    }
}
