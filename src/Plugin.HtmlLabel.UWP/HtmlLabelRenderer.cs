﻿using System.ComponentModel;
using Plugin.HtmlLabel;
using Plugin.HtmlLabel.UWP;
using Xamarin.Forms;
using Xamarin.Forms.Platform.UWP;

[assembly: ExportRenderer(typeof(HtmlLabel), typeof(HtmlLabelRenderer))]

namespace Plugin.HtmlLabel.UWP
{
    public class HtmlLabelRenderer : LabelRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Label> e)
        {
            base.OnElementChanged(e);
            if (Control == null) return;
            UpdateMaxLines();
            UpdateText();
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            if (e.PropertyName == Label.TextProperty.PropertyName)
                UpdateText();
            else if (e.PropertyName == HtmlLabel.MaxLinesProperty.PropertyName)
                UpdateMaxLines();
            else if (e.PropertyName == HtmlLabel.IsHtmlProperty.PropertyName)
                UpdateText();
        }

        private void UpdateMaxLines()
        {
            var maxLines = HtmlLabel.GetMaxLines(Element);
            if (maxLines == default(int)) return;
            Control.MaxLines = maxLines;
        }

        private void UpdateText()
        {
            if (Control == null || Element == null) return;

            var isHtml = HtmlLabel.GetIsHtml(Element);
            if (!isHtml) return;

            var plainText = HtmlToText.ConvertHtml(Element.Text);
            Control.Text = plainText;
        }
    }
}