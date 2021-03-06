﻿using CoreGraphics;
using CoreLocation;
using Foundation;
using MapKit;
using ObjCRuntime;
using System.Collections.Generic;
using System.Diagnostics;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Maps.iOS;
using Xamarin.Forms.Platform.iOS;
using XFMapsSample;
using XFMapsSample.iOS.CustomRenderer;

[assembly: ExportRenderer(typeof(CustomMap), typeof(CustomMapRenderer))]
namespace XFMapsSample.iOS.CustomRenderer
{
    public class CustomMapRenderer : MapRenderer
    {
        MKPolylineRenderer PolylineRenderer;
        MKPolygonRenderer PolygonRenderer;
        CustomMap FormsMap;
        List<CustomPin> CustomPins;
        protected override void OnElementChanged(ElementChangedEventArgs<View> e)
        {
            base.OnElementChanged(e);
            if (e.NewElement != null)
            {
                FormsMap = (CustomMap)e.NewElement;
                CustomPins = FormsMap.RoutePins;
            }
        }

        protected override MKAnnotationView GetViewForAnnotation(MKMapView mapView, IMKAnnotation annotation)
        {
            if (annotation is MKUserLocation)
                return null;

            var customPin = GetCustomPin(annotation as MKPointAnnotation);
            if (customPin == null)
            {
                Debug.WriteLine("Custom pin not found");
                return null;
            }
            var annotationView = new MKAnnotationView(annotation, string.Empty);
            annotationView.Image = UIImage.FromFile(customPin.ImageUrl);
            annotationView.CalloutOffset = new CGPoint(0, 0);
            annotationView.LeftCalloutAccessoryView = new UIImageView(UIImage.FromFile(customPin.ImageUrl));
            annotationView.RightCalloutAccessoryView = UIButton.FromType(UIButtonType.DetailDisclosure);
            annotationView.CanShowCallout = true;

            var nativeMap = Control as MKMapView;
            if (nativeMap.Overlays != null)
            {
                nativeMap.RemoveOverlays(nativeMap.Overlays);
                nativeMap.OverlayRenderer = null;
                PolylineRenderer = null;
                PolygonRenderer = null;
            }
            if (FormsMap.RoutePins != null && FormsMap.RoutePins.Count > 2)
            {
                DrawRoutePolyLine(nativeMap);
            }
            else if (FormsMap.AvailableRegions != null)
            {
                DrawRegionBorderPolyGon(nativeMap);
            }
            return annotationView;
        }

        private void DrawRoutePolyLine(MKMapView nativeMap)
        {
            nativeMap.OverlayRenderer = GetOverlayRenderer;
            var coords = new CLLocationCoordinate2D[FormsMap.RoutePins.Count];
            int index = 0;
            foreach (var pins in FormsMap.RoutePins)
            {
                coords[index] = new CLLocationCoordinate2D(pins.Position.Latitude, pins.Position.Longitude);
                index++;
            }
            var routeOverlay = MKPolyline.FromCoordinates(coords);
            nativeMap.AddOverlay(routeOverlay);
        }
        private void DrawRegionBorderPolyGon(MKMapView nativeMap)
        {
            nativeMap.OverlayRenderer = GetBorderOverlayRenderer;
            var coords = new CLLocationCoordinate2D[FormsMap.AvailableRegions.Count];
            int index = 0;
            foreach (var borders in FormsMap.AvailableRegions)
            {
                coords[index] = new CLLocationCoordinate2D(borders.Latitude, borders.Longitude);
                index++;
            }
            var borderOverlay = MKPolygon.FromCoordinates(coords);
            nativeMap.AddOverlay(borderOverlay);
        }
        private MKOverlayRenderer GetOverlayRenderer(MKMapView mapView, IMKOverlay overlayWrapper)
        {
            if (PolylineRenderer == null && !Equals(overlayWrapper, null))
            {
                var overlay = Runtime.GetNSObject(overlayWrapper.Handle) as IMKOverlay;
                NSNumber[] dashValues = { 5, 5 };
                PolylineRenderer = new MKPolylineRenderer(overlay as MKPolyline)
                {
                    FillColor = Color.Gray.ToUIColor(),
                    StrokeColor = Color.Red.ToUIColor(),
                    LineDashPattern = dashValues,
                    LineWidth = 0.8f
                };
            }
            return PolylineRenderer;
        }
        private MKOverlayRenderer GetBorderOverlayRenderer(MKMapView mapView, IMKOverlay overlayWrapper)
        {
            if (PolygonRenderer == null && !Equals(overlayWrapper, null))
            {
                var overlay = Runtime.GetNSObject(overlayWrapper.Handle) as IMKOverlay;
                PolygonRenderer = new MKPolygonRenderer(overlay as MKPolygon)
                {
                    FillColor = Color.FromHex("#71cce7").ToUIColor(),
                    StrokeColor = Color.FromHex("#71cce7").ToUIColor(),
                    Alpha = 0.4f,
                    LineWidth = 1
                };
            }
            return PolygonRenderer;
        }
        private CustomPin GetCustomPin(MKPointAnnotation annotation)
        {
            CustomPin customPin = null;
            var position = new Position(annotation.Coordinate.Latitude, annotation.Coordinate.Longitude);
            foreach (var pin in CustomPins)
            {
                if (pin.Position == position)
                {
                    customPin = pin;
                }
            }
            return customPin;
        }
    }
}