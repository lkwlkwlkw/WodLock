using System;
using System.Device.Gpio;
using System.Threading.Tasks;

public class GPIO
{
    public int LockOutputPin;
    public int OpenButtonPin;
    private readonly GpioController controller = new();
    private bool DoorLockPowered = false;
    public IO(int LockOutputPin, int OpenButtonPin) //konstruktor klasy
    {
        this.LockOutputPin = LockOutputPin;
        controller.OpenPin(this.LockOutputPin, PinMode.Output, true);
        this.OpenButtonPin = OpenButtonPin;
        controller.OpenPin(this.OpenButtonPin, PinMode.InputPullUp);
        controller.RegisterCallbackForPinValueChangedEvent(OpenButtonPin, PinEventTypes.Falling, DoorOpenPressed);
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
