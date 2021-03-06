﻿using System;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Tizen;

[assembly: ExportRenderer(typeof(PhotoWatch.CircleContentView), typeof(PhotoWatch.CircleContentViewRenderer))]
namespace PhotoWatch
{
    class CircleContentViewRenderer : LayoutRenderer
    {
        ElmSharp.EvasImage _circleImage;

        protected override void OnElementChanged(ElementChangedEventArgs<Layout> e)
        {
            base.OnElementChanged(e);
            if (_circleImage == null)
            {
                _circleImage = new ElmSharp.EvasImage(Forms.NativeParent);
                _circleImage.IsFilled = true;
                _circleImage.File = ResourcePath.GetPath("circle.png");
                _circleImage.Show();
                _circleImage.Geometry = Control.Geometry;
                _circleImage.PassEvents = true;
                Control.SetClip(_circleImage);
                Control.LayoutUpdated += OnLayoutUpdated;
                Control.Moved += Control_Moved;
                Control.Deleted += Control_Deleted;
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            _circleImage.Geometry = Control.Geometry;
            Control.SetClip(_circleImage);
        }

        private void Control_Deleted(object sender, EventArgs e)
        {
            _circleImage?.Unrealize();
        }

        private void Control_Moved(object sender, EventArgs e)
        {
            _circleImage.Geometry = Control.Geometry;
        }

        private void OnLayoutUpdated(object sender, Xamarin.Forms.Platform.Tizen.Native.LayoutEventArgs e)
        {
            _circleImage.Geometry = Control.Geometry;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _circleImage?.Unrealize();
            }
            base.Dispose(disposing);
        }
    }
}
