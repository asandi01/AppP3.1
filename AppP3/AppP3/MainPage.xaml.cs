using AppP3.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AppP3 {
    public partial class MainPage : ContentPage {
        //IEnumerable<PaymentRecordModel> listPayment;// = new IEnumerable<PaymentRecordModel>();
        List<string> listPayment = new List<string>();
        public MainPage() {
            InitializeComponent();
            //lst.ItemsSource = new List<string>() { "item 1", "item 2", "Item 3" };  

            var wsPayment = new ServiceReference2.PaymentRecordSoapClient();
            wsPayment.GetDetailsAsync();
            wsPayment.GetDetailsCompleted += wsPaymentCompleted;
        }


        protected async override void OnAppearing() {
            base.OnAppearing();   
        }



        private async void wsPaymentCompleted(object sender, ServiceReference2.GetDetailsCompletedEventArgs e) {
            var results = e.Result;
            //listPayment = results.Cast<PaymentRecordModel>();

            foreach (var item in results) { 
                /*listPayment.Add(new PaymentRecordModel() => {
                      item.amount,
                      item.id,
                      item.paymentDate,
                      item.providerId,
                      item.recurrence
                }); */
                listPayment.Add(item.id +" => " +item.detail + " " + item.amount);
            }

            Device.BeginInvokeOnMainThread(() => {
                lst.ItemsSource = listPayment;    
            });
 

        }



        async void OnAddItemClicked(object sender, EventArgs e) {    
        }


        async void OnItemSelected(object sender, SelectedItemChangedEventArgs e) {
            string itemSelected = e.SelectedItem.ToString();
            string[] id = itemSelected.Split(' ');

            int idSelected = Int32.Parse(id[0]);
            await Navigation.PushModalAsync(new EditPayment(idSelected));
            //await Navigation.PushAsync(EditPayment);
        }


        Label label;
        int clickTotal = 0;

        /*public MainPage() {
            Label header = new Label {
                Text = "Button",
                Font = Font.BoldSystemFontOfSize(50),
                HorizontalOptions = LayoutOptions.Center
            };

            Button button = new Button {
                Text = "Click Me!",
                Font = Font.SystemFontOfSize(NamedSize.Large),
                BorderWidth = 1,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.CenterAndExpand
            };
            button.Clicked += OnButtonClicked; 
                    
            label = new Label {
                Text = "0 button clicks",
                Font = Font.SystemFontOfSize(NamedSize.Large),
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.CenterAndExpand
            };

            // Accomodate iPhone status bar.
            this.Padding = new Thickness(10, Device.OnPlatform(20, 0, 0), 10, 5);

            // Build the page.
            this.Content = new StackLayout {
                Children =
                {
                    header,
                    button,
                    label
                }
            };
        } */

        void OnButtonClicked(object sender, EventArgs e) {
            clickTotal += 1;
            detail.Text = String.Format("{0} button click{1}",
                                       clickTotal, clickTotal == 1 ? "" : "s");
        }

        void OnImageNameTapped(object sender, EventArgs args) {
            try {
                clickTotal += 1;
                detail.Text = String.Format("{0} button click{1}", clickTotal, clickTotal == 1 ? "" : "s");
            } catch (Exception ex) {
                throw ex;
            }
        }
    }
}
