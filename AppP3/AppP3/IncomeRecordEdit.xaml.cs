using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppP3 {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class IncomeRecordEdit : ContentPage {
        public IncomeRecordEdit(int idSelected) {
            InitializeComponent();
            id.Text = idSelected.ToString(); 
                                                                            
            var wsPayment = new ServiceReference3.IncomeRecordSoapClient();
            wsPayment.GetDetailsByIdAsync(idSelected);
            wsPayment.GetDetailsByIdAsync(idSelected);
            wsPayment.GetDetailsByIdCompleted += wsPaymentCompleted;
        }   
                                                                                                                      
        private async void wsPaymentCompleted(object sender, ServiceReference3.GetDetailsByIdCompletedEventArgs e) {
            var payment = e.Result;
            Device.BeginInvokeOnMainThread(() => {
                detail.Text = payment.detail;
                amount.Text = payment.amount.ToString();                      
                paymentDate.Text = payment.paymentDate.ToString();        
            });
        }     

        async void OnSubmitData(object sender, EventArgs args) {
            try {                                                                                          
                ServiceReference3.IncomeRecordModel payment = new ServiceReference3.IncomeRecordModel();

                payment.id = Int32.Parse(id.Text);
                payment.detail = detail.Text;
                payment.amount = Double.Parse(amount.Text);                        
                payment.paymentDate = DateTime.Parse(paymentDate.Text);           

                var wsPayment = new ServiceReference3.IncomeRecordSoapClient();
                wsPayment.UpdateDetailsAsync(payment);
                wsPayment.UpdateDetailsCompleted += wsPaymentUpdateCompleted; 

            } catch (Exception ex) {
                throw ex;
            }
        }                                                                                                                  

        private async void wsPaymentUpdateCompleted(object sender, ServiceReference3.UpdateDetailsCompletedEventArgs e) {
            var payment = e.Result;
            Device.BeginInvokeOnMainThread(() => {
                if (payment) {
                    Navigation.PushModalAsync(new IncomeRecordList());
                } else {
                    DisplayAlert("Error", "Ocurrio un error", "Ok");
                }
            });
        }

        async void OnDelete(object sender, EventArgs args) {
            try {                                                                                          
                ServiceReference3.IncomeRecordModel payment = new ServiceReference3.IncomeRecordModel();

                payment.id = Int32.Parse(id.Text);
                var wsPayment = new ServiceReference3.IncomeRecordSoapClient();
                wsPayment.DeleteAsync(payment.id);
                wsPayment.DeleteCompleted += wsPaymentDeleteCompleted;

            } catch (Exception ex) {
                throw ex;
            }
        }                                                                                                            

        private async void wsPaymentDeleteCompleted(object sender, ServiceReference3.DeleteCompletedEventArgs e) {
            var payment = e.Result;
            Device.BeginInvokeOnMainThread(() => {
                if (payment) {
                    Navigation.PushModalAsync(new IncomeRecordList());
                } else {
                    DisplayAlert("Error", "Ocurrio un error", "Ok");
                }
            });
        }
    }  
}