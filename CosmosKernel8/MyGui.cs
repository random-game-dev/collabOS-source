using Cosmos.System;
using Cosmos.System.Graphics;
using Cosmos.System.Graphics.Fonts;
using System.Drawing;

namespace CollabGui
{
    // THIS TOOOK TOO LONG AGGGGGGH
    public static class CollabGUIClass
    {
        public static Canvas CollabGUI;
        private static Color backgroundColor = Color.Black;

        private static int prevMouseX = -1;
        private static int prevMouseY = -1;

        private static bool mouseInitialized = false;

        public static void InitGUI()
        {
            CollabGUI = FullScreenCanvas.GetFullScreenCanvas();
            CollabGUI.Clear(backgroundColor);
            CollabGUI.Display();

            mouseInitialized = false;
        }

        public static void EnableMouse()
        {
            if (CollabGUI == null)
                return;

            // Set mouse screen boundaries
            MouseManager.ScreenWidth = (uint)CollabGUI.Mode.Width;
            MouseManager.ScreenHeight = (uint)CollabGUI.Mode.Height;

            // Center annoying mouse
            MouseManager.X = (uint)(CollabGUI.Mode.Width / 2);
            MouseManager.Y = (uint)(CollabGUI.Mode.Height / 2);

            mouseInitialized = true;
        }

        public static void UpdateMouse()
        {
            if (!mouseInitialized || CollabGUI == null)
                return;

            int mouseX = (int)MouseManager.X;
            int mouseY = (int)MouseManager.Y;

            // Only update if mouse moved
            if (mouseX != prevMouseX || mouseY != prevMouseY)
            {
                // Erase previous cursor
                if (prevMouseX >= 0 && prevMouseY >= 0)
                    DrawCursor(prevMouseX, prevMouseY, backgroundColor);

                // Draw new cursor UGGGHHHHHHH
                DrawCursor(mouseX, mouseY, Color.White);

                prevMouseX = mouseX;
                prevMouseY = mouseY;

                CollabGUI.Display();
            }
        }

        private static void DrawCursor(int x, int y, Color color)
        {
            // Simple cross-style cursor
            CollabGUI.DrawPoint(color, x, y);
            CollabGUI.DrawPoint(color, x - 1, y);
            CollabGUI.DrawPoint(color, x + 1, y);
            CollabGUI.DrawPoint(color, x, y - 1);
            CollabGUI.DrawPoint(color, x, y + 1);
        }
    }
}
