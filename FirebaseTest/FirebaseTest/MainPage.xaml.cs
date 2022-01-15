using Firebase.Database;
using Firebase.Database.Query;
using FirebaseTest.MVVM.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace FirebaseTest
{
    public partial class MainPage : ContentPage
    {
        public ObservableCollection<RecordData> RecordCollection { get; set; } = new ObservableCollection<RecordData>();
        private FirebaseClient client = new FirebaseClient("https://fir-test-7a840-default-rtdb.asia-southeast1.firebasedatabase.app/");
        public MainPage()
        {
            InitializeComponent();
            BindingContext = this;
            IDisposable collection = client.Child("Records").AsObservable<RecordData>().Subscribe((dbEvent) =>
            {
                if (dbEvent.Object != null)
                    RecordCollection.Add(dbEvent.Object);
            });
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            client.Child("Records").PostAsync(new RecordData 
            {
                Text = recordEntry.Text
            });
            recordEntry.Text = string.Empty;
        }
    }
}
