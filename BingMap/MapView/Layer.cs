using System.Collections.Generic;
using System.Drawing;

namespace BingMap.MapView
{
    public class Layer
    {
		public int m_LeftUpPixelX;

		public int m_LeftUpPixelY;

		public int m_MaxWidth;

		public int m_MaxHeight;

		public int m_DrawWidth;

		public int m_DrawHeight;

		protected MapView MapView;
		public string RescoresPath { get { return m_RescoresPath; } set { m_RescoresPath = value; } }
		public string[] Rescores { get { return m_RescoresList.ToArray(); } set => m_RescoresList = new List<string>(value); }


		protected string m_RescoresPath = string.Empty;
		protected List<string> m_RescoresList = new List<string>();

		public Layer(int maxW, int maxH, MapView view)
		{
			this.m_MaxWidth = maxW;
			this.m_MaxHeight = maxH;
			this.m_DrawWidth = 0;
			this.m_DrawHeight = 0;
			this.m_LeftUpPixelX = 0;
			this.m_LeftUpPixelY = 0;
			this.MapView = view;
		}

		public virtual void Close()
		{
		}

		public virtual void LoadFile()
		{
		}

		public virtual void Move(int offsetX, int offsetY)
		{
			this.m_LeftUpPixelX -= offsetX;
			this.m_LeftUpPixelY -= offsetY;
			this.LoadFile();
		}

		public virtual void MoveNoLoad(int offsetX, int offsetY)
		{
			this.m_LeftUpPixelX -= offsetX;
			this.m_LeftUpPixelY -= offsetY;
		}

		public virtual void Paint(Graphics graphics)
		{
		}

		public void Resize(int width, int height)
		{
			this.m_MaxWidth = width;
			this.m_MaxHeight = height;
		}

		public void SetHeight(int height)
		{
			this.m_DrawHeight = height;
		}

		public virtual void setLeftUpPixel(int x, int y)
		{
			this.m_LeftUpPixelX = x;
			this.m_LeftUpPixelY = y;
		}

		public void SetWidth(int width)
		{
			this.m_DrawWidth = width;
		}

		public virtual void UpdateLayer()
		{
            this.MapView.Invalidate();
        }

		public virtual void UpdateLayer(Rectangle rect)
		{
			this.MapView.Invalidate(rect);
		}

		public virtual void ViewAllMapAndCentor()
		{
			this.m_DrawWidth = 0;
			this.m_DrawHeight = 0;
			this.m_LeftUpPixelX = 0;
			this.m_LeftUpPixelY = 0;
		}

		public virtual void ZoomIn(int x, int y)
		{
		}

		public virtual void ZoomOut(int x, int y)
		{

		}
	}
}
