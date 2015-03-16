using System;
using Xamarin.Forms;
using ACM.Views.CustomCells;
using ACM.iOS.Renderers;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer (typeof (RightImageCell), typeof (RightImageCellRenderer))]
namespace ACM.iOS.Renderers
{
	public class RightImageCellRenderer : ImageCellRenderer
	{
		public override UIKit.UITableViewCell GetCell (Cell item, UIKit.UITableViewCell reusableCell, UIKit.UITableView tv)
		{
			var cell = base.GetCell (item, reusableCell, tv);
			cell.Accessory = UIKit.UITableViewCellAccessory.DisclosureIndicator;
			return cell;
		} 
	}
}

