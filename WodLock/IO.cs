using System.Device.Gpio;
using System.Threading.Tasks;


namespace WodLock
{
    public class GPIO
    {
        public int LockOutputPin;
        public int DoorOpenedSensorPin;
        private readonly GpioController controller = new();
        private bool DoorLockPowered = false;
        public GPIO(int LockOutputPin, int OpenButtonPin, int BuzzerPin) //konstruktor klasy
        {
            this.LockOutputPin = LockOutputPin;
            controller.OpenPin(this.LockOutputPin, PinMode.Output, true);
            this.DoorOpenedSensorPin = OpenButtonPin;
            controller.OpenPin(this.DoorOpenedSensorPin, PinMode.InputPullUp);
            //   controller.RegisterCallbackForPinValueChangedEvent(OpenButtonPin, PinEventTypes.Falling, DoorOpenPressed);
        }

        public async void OpenDoor()
        {
            DoorLockPowered = true;
            controller.Write(LockOutputPin, PinValue.Low);
            await Task.Delay(4000);
            controller.Write(LockOutputPin, PinValue.High);
            DoorLockPowered = false;
        }
    }
}
