using System;
using Cairo;
using MonoHotDraw;

namespace ClassDesigner
{
	public class ClassFigure: BaseBoxFigure
	{
		public ClassFigure(): base()
		{
			FillColor = new Cairo.Color(0.8, 0.0, 0.0, 0.8);
			
			classname = new SimpleTextFigure("ClassName");
		}
		
		public override void BasicDraw(Cairo.Context context)
		{
			context.Rectangle(DisplayBox.X, DisplayBox.Y,
			                  DisplayBox.Width, DisplayBox.Height);
			
			context.Stroke();
			
			classname.BasicDraw(context);
		}
		
		public override RectangleD BasicDisplayBox {
			get {
				return base.BasicDisplayBox;
			}
			set {
				RectangleD r = value;
				r.Width = Math.Max(r.Width, classname.DisplayBox.Width);
				r.Height = Math.Max(r.Height, classname.DisplayBox.Height);
				base.BasicDisplayBox = r;
			}
		}

		
		protected override void OnFigureChanged (MonoHotDraw.FigureEventArgs e)
		{
			classname.MoveTo(DisplayBox.X, DisplayBox.Y);
		}
		
		private SimpleTextFigure classname;
	}
}
