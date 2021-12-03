using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Internals;
using ObjCRuntime;
using UIKit;

namespace Microsoft.Maui.Controls.Platform
{
	public static partial class FontExtensions
	{
		public static UIFont ToUIFont(this Font self)
			=> CompatServiceProvider.FontManager.GetFont(self);

		internal static UIFont ToUIFont(this IFontElement self)
			=> CompatServiceProvider.FontManager.GetFont(self.ToFont());

		internal static bool IsDefault(this IFontElement self)
			=> self.FontFamily == null && self.FontSize == Device.GetNamedSize(NamedSize.Default, typeof(Label), true) && self.FontAttributes == FontAttributes.None;
	}
}