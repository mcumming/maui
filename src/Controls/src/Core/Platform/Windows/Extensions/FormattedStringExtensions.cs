#nullable enable
using System.Collections.Generic;
using Microsoft.Maui.Controls.Internals;
using Microsoft.Maui.Graphics;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Documents;
using Microsoft.UI.Xaml.Media;

namespace Microsoft.Maui.Controls.Platform
{
	public static class FormattedStringExtensions
	{
		public static void UpdateInlines(this TextBlock textBlock, Label label)
		{
			var formatted = label.FormattedText;
			if (formatted == null)
				return;

			textBlock.Inlines.Clear();
			// Have to implement a measure here, otherwise inline.ContentStart and ContentEnd will be null, when used in RecalculatePositions
			textBlock.Measure(new global::Windows.Foundation.Size(double.MaxValue, double.MaxValue));

			var heights = new List<double>();
			for (var i = 0; i < formatted.Spans.Count; i++)
			{
				var span = formatted.Spans[i];
				var run = span.ToRun(label);
				heights.Add(textBlock.FindDefaultLineHeight(run));
				textBlock.Inlines.Add(run);
			}
		}

		public static Run ToRun(this Span span, Label label)
		{
			var text = TextTransformUtilites.GetTransformedText(span.Text, span.TextTransform);
			
			var run = new Run { Text = text ?? string.Empty };

			var fgcolor = span.TextColor ?? label?.TextColor;
			if (fgcolor is not null)
				run.Foreground = fgcolor.ToNative();

			// NOTE: Background is not supported in Run

			var font = span.ToFont();
			if (font.IsDefault && label is not null)
				font = label.ToFont();

			if (!font.IsDefault)
			{
				run.ApplyFont(font);
			}

			if (span.IsSet(Span.TextDecorationsProperty))
				run.TextDecorations = (global::Windows.UI.Text.TextDecorations)span.TextDecorations;

			run.CharacterSpacing = span.CharacterSpacing.ToEm();

			return run;
		}
	}
}