using System;
using Xamarin.Forms.Platform.iOS;
using Xamarin.Forms;
using ACM.Views.CustomCells;
using ACM.iOS.Renderers;
using ACM.Views.Themes;
using UIKit;
using System.Linq;

[assembly: ExportRenderer (typeof (RightTextCell), typeof (RightTextCellRenderer))]
namespace ACM.iOS.Renderers
{
	public class RightTextCellRenderer : TextCellRenderer
	{
		public override UIKit.UITableViewCell GetCell (Xamarin.Forms.Cell item, UIKit.UITableViewCell reusableCell, UIKit.UITableView tv)
		{			
			var cell = base.GetCell (item, reusableCell, tv);
			cell.Accessory = UIKit.UITableViewCellAccessory.DisclosureIndicator;
			return cell;
		}
	}
}

