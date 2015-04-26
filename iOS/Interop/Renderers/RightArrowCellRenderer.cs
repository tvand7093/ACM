using System;
using Xamarin.Forms.Platform.iOS;
using CSAssistant.iOS.Interop.Renderers;
using CSAssistant.Cells;

[assembly: Xamarin.Forms.ExportRenderer(typeof(RightArrowCell), typeof(RightArrowCellRenderer))]
namespace CSAssistant.iOS.Interop.Renderers
{
	public class RightArrowCellRenderer : TextCellRenderer
	{
		public override UIKit.UITableViewCell GetCell (Xamarin.Forms.Cell item, UIKit.UITableViewCell reusableCell, UIKit.UITableView tv)
		{
			var cell = base.GetCell (item, reusableCell, tv); 
			cell.Accessory = UIKit.UITableViewCellAccessory.DisclosureIndicator;
			return cell;
		}
		public RightArrowCellRenderer ()
		{
			
		}
	}
}

