using SharpDX.DirectInput;
using System;
using System.IO;
using System.Threading.Tasks;

namespace MDT.Core
{
    public class Guard:BaseManager<Guard>
    {
        public event Func<KeyboardUpdate, Task> Command;
        public Guard()
        {
            _ = Task.Run(async () => { await Start(); });
        }
        public async Task Start()
        {
            // Initialize DirectInput
            DirectInput directInput = new DirectInput();

            // Instantiate the joystick
            Keyboard keyboard = new Keyboard(directInput);

            // Acquire the joystick
            keyboard.Properties.BufferSize = 128;
            keyboard.Acquire();

            // Poll events from joystick
            while (true)
            {
                keyboard.Poll();
                KeyboardUpdate[] datas = keyboard.GetBufferedData();
                foreach (KeyboardUpdate state in datas)
                {
                    Command?.Invoke(state);
                }
                await Task.Delay(1);
            }
        }
    }
}
