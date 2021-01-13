using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices.WindowsRuntime;
using TraveloreFE.Model;
using TraveloreFE.ViewModel;
using Windows.Devices.Geolocation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Services.Maps;
using Windows.UI;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Maps;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Documents;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace TraveloreFE.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MapRoute : Page
    {
        public Travellist Travellist { get; set; }
        public ItineraryViewModel Ivm { get; set; }
        public Destination Destination { get; set; }

        public MapRoute()
        {
            this.InitializeComponent();
            MCMain.MapServiceToken = "lluMuufiscJFMpmflrJv~KjY89taCWnpm-1WZRMywhQ~Amf3NDXskt5PGqmDIPfBM8yBqQ6xGtH2xEoGx6XdMxujAkWoecW3havT41Cne3Cy";
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            Travellist = (Travellist)e.Parameter;
            Travellist.SortItineraryByDate();

            DataContext = new ItineraryViewModel(Travellist, null);
            Ivm = (ItineraryViewModel)DataContext;

            cmbDestinations.SelectedItem = determineNext();
            CalcRoute();
        }

        private async void btnGoBack_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(ItineraryView), Travellist);
        }

        private async void CalcRoute()
        {
            MCMain.MapElements.Clear();
            MCMain.Routes.Clear();
            if (Destination != null)
            {
                var nextLocation = Destination.Street + " " + Destination.Nr + ", " + Destination.City;

                var accessResult = await Geolocator.RequestAccessAsync();

                switch (accessResult)
                {
                    case GeolocationAccessStatus.Allowed:
                        var locator = new Geolocator { DesiredAccuracyInMeters = 100 };
                        var position = await locator.GetGeopositionAsync().AsTask();
                        var currentLocation = new BasicGeoposition()
                        {
                            Latitude = position.Coordinate.Point.Position.Latitude,
                            Longitude = position.Coordinate.Point.Position.Longitude
                        };
                        var gp = new Geopoint(new BasicGeoposition());
                        var res = await MapLocationFinder.FindLocationsAsync(nextLocation, gp);
                        if (res.Locations.Count == 0)
                        {
                            MessageDialog dialog = new MessageDialog("No destination found for this address.");
                            await dialog.ShowAsync();
                            break;
                        }
                        var location = res.Locations.First();
                        var endPoint = new Geopoint(new BasicGeoposition()
                        {
                            Latitude = location.Point.Position.Latitude,
                            Longitude = location.Point.Position.Longitude
                        });
                        var startPoint = new Geopoint(currentLocation);
                        AddPointToMap(startPoint, "Your position");
                        AddPointToMap(endPoint, nextLocation);
                        ShowRouteOnMap(startPoint, endPoint);
                        break;
                    default:
                        break;
                }
            }
        }

        private Destination determineNext()
        {
            if (Travellist.Itinerary.Count == 0)
            {
                return null;
            }
            Travellist.SortItineraryByDate();
            Destination destination = Travellist.Itinerary[0];
            DateTime now = DateTime.Now;
            foreach (Destination d in Travellist.Itinerary)
            {
                destination = d;
                if (now < d.VisitTime)
                {
                    break;
                }
            }
            Destination = destination;
            return destination;
        }

        private void cmbDestinations_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Destination destination = (Destination)(cmbDestinations.SelectedItem as Destination);
            if (destination != null)
            {
                Destination = destination;
                CalcRoute();
            }
        }

        private void AddPointToMap(Geopoint point, string name)
        {
            MapIcon icon = new MapIcon();
            icon.Location = point;
            icon.NormalizedAnchorPoint = new Point(0.5, 1.0);
            icon.Title = name;
            icon.ZIndex = 0;
            MCMain.MapElements.Add(icon);
        }

        private async void ShowRouteOnMap(Geopoint startPoint, Geopoint endPoint)
        {
            var routeResult = await MapRouteFinder.GetDrivingRouteAsync(
                startPoint,
                endPoint,
                MapRouteOptimization.Time,
                MapRouteRestrictions.None);
            if (routeResult.Status == MapRouteFinderStatus.Success)
            {
                MapRouteView routeView = new MapRouteView(routeResult.Route);
                routeView.RouteColor = Colors.Yellow;
                routeView.OutlineColor = Colors.Black;
                MCMain.Routes.Add(routeView);
            }

            if(routeResult.Route == null)
            {
                MessageDialog dialog = new MessageDialog("No route found to this destination.");
                await dialog.ShowAsync();
            }
            else
            {
                await MCMain.TrySetViewBoundsAsync(routeResult.Route.BoundingBox, null, MapAnimationKind.None);
            }
        }
    }
}
