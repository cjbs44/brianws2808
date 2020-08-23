﻿using System.Device.Spi;
using System.Drawing;
using Iot.Device.Graphics;
using Iot.Device.Ws28xx;

namespace brianws2808
{
    class Program
    {
        // Configure the count of pixels
        private const int Count = 8;

        static void Main()
        {
            var settings = new SpiConnectionSettings(0, 0)
            {
                ClockFrequency = 2_400_000,
                Mode = SpiMode.Mode0,
                DataBitLength = 8
            };

            // Create a Neo Pixel x8 stick on spi 0.0
            var spi = SpiDevice.Create(settings);

            var neo = new Ws2808(spi, Count);

            // Display basic colors for 5 sec
            BitmapImage img = neo.Image;
            img.Clear();
            img.SetPixel(0, 0, Color.White);
            img.SetPixel(1, 0, Color.Red);
            img.SetPixel(2, 0, Color.Green);
            img.SetPixel(3, 0, Color.Blue);
            img.SetPixel(4, 0, Color.Yellow);
            img.SetPixel(5, 0, Color.Cyan);
            img.SetPixel(6, 0, Color.Magenta);
            img.SetPixel(7, 0, Color.FromArgb(unchecked((int)0xffff8000)));
            neo.Update();
            System.Threading.Thread.Sleep(5000);

            // Fade in first pixel
            byte b = 0;
            img.Clear();
            while (true)
            {
                img.SetPixel(0, 0, Color.FromArgb(0xff, b, b, b));
                neo.Update();
                System.Threading.Thread.Sleep(10);
                b++;
            }

        }
    }
}
