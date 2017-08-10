using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace DoubleBufferExample
{
	public class DoubleBufferControl : Control
	{
        const Graphics NO_BUFFER_GRAPHICS = null;
        const Bitmap NO_BACK_BUFFER = null;
        const BufferedGraphics NO_MANAGED_BACK_BUFFER = null;

        Bitmap BackBuffer;
        Graphics BufferGraphics;
        BufferedGraphicsContext GraphicManager;
        BufferedGraphics ManagedBackBuffer;
		
		DoubleBufferMethod _PaintMethod = DoubleBufferMethod.NoDoubleBuffer;
		GraphicTestMethods _GraphicTest = GraphicTestMethods.DrawTest;

        public DoubleBufferControl()
        {
            InitializeComponent();

            SetStyle(
                ControlStyles.AllPaintingInWmPaint | 
                ControlStyles.UserPaint, true);

            Application.ApplicationExit += new EventHandler(MemoryCleanup);
        }

        private void InitializeComponent()
		{
            this.SuspendLayout();
            // 
            // DoubleBufferControl
            // 
            this.BackColor = System.Drawing.Color.Black;
            this.Resize += new System.EventHandler(this.DoubleBufferControl_Resize);
            this.ResumeLayout(false);

		}

		public DoubleBufferMethod PaintMethod
		{
			get { return _PaintMethod; }
			set
			{
				_PaintMethod = value;
				RemovePaintMethods();

                switch (value)
                {
                    case DoubleBufferMethod.BuiltInDoubleBuffer:
                        this.SetStyle(ControlStyles.UserPaint, true);
                        this.DoubleBuffered = true;
                        break;
                    case DoubleBufferMethod.BuiltInOptimizedDoubleBuffer:
                        this.SetStyle(
                            ControlStyles.OptimizedDoubleBuffer | 
                            ControlStyles.AllPaintingInWmPaint, true);
                        break;
                    case DoubleBufferMethod.ManualDoubleBuffer11:
                        this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
                        BackBuffer = new Bitmap(ClientRectangle.Width, ClientRectangle.Height);
                        BufferGraphics = Graphics.FromImage(BackBuffer);
                        break;

                    case DoubleBufferMethod.ManualDoubleBuffer20:
                        this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
                        GraphicManager = BufferedGraphicsManager.Current;
                        GraphicManager.MaximumBuffer = new Size(this.Width + 1, this.Height + 1);
                        ManagedBackBuffer = GraphicManager.Allocate(this.CreateGraphics(), ClientRectangle);
                        break;
                }
			}
		}

		public GraphicTestMethods GraphicTest
		{
			get { return _GraphicTest; }
			set	
			{
				this.CreateGraphics().Clear(Color.Wheat);
				_GraphicTest = value;	
			}
		}

		public enum DoubleBufferMethod
		{
			NoDoubleBuffer,
			BuiltInDoubleBuffer,
			BuiltInOptimizedDoubleBuffer,
			ManualDoubleBuffer11,
			ManualDoubleBuffer20
		};

		public enum GraphicTestMethods
		{
			DrawTest,
			FillTest
		};

		private void MemoryCleanup(object sender, EventArgs e)
		{
            if (BufferGraphics != NO_BUFFER_GRAPHICS)
                BufferGraphics.Dispose();

			if (BackBuffer != NO_BACK_BUFFER)
				BackBuffer.Dispose();

            if (ManagedBackBuffer != NO_MANAGED_BACK_BUFFER)
                ManagedBackBuffer.Dispose();
		}


		protected override void OnPaint(PaintEventArgs e)
		{
            if (DesignMode) { base.OnPaint(e); return; }

			switch (_PaintMethod)
			{
				case DoubleBufferMethod.NoDoubleBuffer:
					base.OnPaint(e);
					LunchGraphicTest(e.Graphics);
					break;

				case DoubleBufferMethod.BuiltInDoubleBuffer:
					LunchGraphicTest(e.Graphics);
					break;

				case DoubleBufferMethod.BuiltInOptimizedDoubleBuffer:
					LunchGraphicTest(e.Graphics); 
					break;

				case DoubleBufferMethod.ManualDoubleBuffer11:
					PaintDoubleBuffer11(e.Graphics); break;

				case DoubleBufferMethod.ManualDoubleBuffer20:
					PaintDoubleBuffer20(e.Graphics); break;
			}
		}

        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
/*            if (_PaintMethod != DoubleBufferMethod.ManualDoubleBuffer11 &&
                _PaintMethod != DoubleBufferMethod.ManualDoubleBuffer20)

                base.OnPaintBackground(pevent);*/
        }

		private void RemovePaintMethods()
		{
			this.DoubleBuffered = false;

			this.SetStyle(ControlStyles.OptimizedDoubleBuffer, false);

            if (BufferGraphics != NO_BUFFER_GRAPHICS)
            {
                BufferGraphics.Dispose();
                BufferGraphics = NO_BUFFER_GRAPHICS;
            }

            if (BackBuffer != NO_BACK_BUFFER)
            {
                BackBuffer.Dispose();
                BackBuffer = NO_BACK_BUFFER;
            }

            if (ManagedBackBuffer != NO_MANAGED_BACK_BUFFER)
                ManagedBackBuffer.Dispose();
		}

        private void DoubleBufferControl_Resize(object sender, EventArgs e)
        {
            switch (_PaintMethod)
            {
                case DoubleBufferMethod.ManualDoubleBuffer11:
                    BackBuffer = new Bitmap(ClientRectangle.Width, ClientRectangle.Height);
                    BufferGraphics = Graphics.FromImage(BackBuffer);
                    break;

                case DoubleBufferMethod.ManualDoubleBuffer20:
                    GraphicManager.MaximumBuffer = new Size(this.Width + 1, this.Height + 1);

                    if (ManagedBackBuffer != NO_MANAGED_BACK_BUFFER)
                        ManagedBackBuffer.Dispose();

                    ManagedBackBuffer = GraphicManager.Allocate(this.CreateGraphics(), ClientRectangle);
                    break;
            }

            this.Refresh();
        }

        private void PaintDoubleBuffer11(Graphics ControlGraphics)
        {
            LunchGraphicTest(BufferGraphics);

            // this draws the image from the buffer into the form area 
            // (note: DrawImageUnscaled is the fastest way)
            ControlGraphics.DrawImageUnscaled(BackBuffer, 0, 0); 
        }

		private void PaintDoubleBuffer20(Graphics ControlGraphics)
		{
			try
			{
				LunchGraphicTest(ManagedBackBuffer.Graphics);
                
                // paint the picture in from the back buffer into the form draw area
                ManagedBackBuffer.Render(ControlGraphics);
			}
			catch (Exception Exp) { Console.WriteLine(Exp.Message); }
		}

		private void LunchGraphicTest(Graphics TempGraphics)
		{
			int i;
			Random Rnd = new Random();
			Pen BlackPen = new Pen(new SolidBrush(Color.Black));
            Pen ColorPen = null;
			Rectangle TempRectangle;
            LinearGradientBrush ColorBrush = null;

			TempGraphics.Clear(Color.Wheat);

			switch (GraphicTest)
			{
				case GraphicTestMethods.DrawTest:
					for (i = 0; i < 100; i++)
					{
						TempRectangle = new Rectangle(
							Rnd.Next(0, Width),
							Rnd.Next(0, Height),
							Width - i,
							Height - i);

						ColorPen = new Pen(Color.FromArgb(127, Rnd.Next(0, 256), Rnd.Next(256), Rnd.Next(256)));
                        TempGraphics.DrawRectangle(ColorPen, TempRectangle);
					}
                    
                    ColorPen.Dispose();
					break;

				case GraphicTestMethods.FillTest:
					for (i = 0; i < 100; i++)
					{
						TempRectangle = new Rectangle(
							Rnd.Next(0, Width),
							Rnd.Next(0, Height),
							Width - i,
							Height - i);

                        ColorBrush = new LinearGradientBrush(
                                TempRectangle,
                                Color.FromArgb(127, Rnd.Next(0, 256), Rnd.Next(256), Rnd.Next(256)),
                                Color.FromArgb(127, Rnd.Next(0, 256), Rnd.Next(256), Rnd.Next(256)),
                                (LinearGradientMode)Rnd.Next(3));

                        TempGraphics.FillEllipse(ColorBrush, TempRectangle);

					}

                    ColorBrush.Dispose();
					break;
			}
		}
    }
}
