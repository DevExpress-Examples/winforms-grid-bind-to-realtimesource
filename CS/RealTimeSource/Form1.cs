using DevExpress.Data;
using DevExpress.XtraEditors.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace RealTimeSourceWinForms
{
    public partial class Form1 : Form
    {
        BindingList<Data> Persons;
        int Count = 50;
        Random Random = new Random();
        public Form1()
        {

            InitializeComponent();
            Persons = new BindingList<Data>();
            for (int i = 0; i < Count; i++)
                Persons.Add(new Data
                {
                    Id = i,
                    Text = "Text" + i,
                    Progress = GetNumber()
                });


            RealTimeSource rts = new RealTimeSource()
                        {
                            DataSource = Persons
                        };
            gridControl1.DataSource = rts;

            Timer timer = new Timer();
            timer.Interval = 10;
            timer.Tick += Tick;
            timer.Start();

            gridView1.Columns["Progress"].ColumnEdit = new RepositoryItemProgressBar();
        }
        private void Tick(object sender, EventArgs e)
        {
            int index = Random.Next(0, Count);
            Persons[index].Id = GetNumber();
            Persons[index].Text = "Text" + GetNumber();
            Persons[index].Progress = GetNumber();
        }
        int GetNumber()
        {
            return Random.Next(0, Count);
        }
    }
    public class Data : INotifyPropertyChanged
    {
        private int _Id;
        public string _Text;
        public double _Progress;

        public int Id
        {
            get
            {
                return _Id;
            }
            set
            {
                _Id = value;
                NotifyPropertyChanged("Id");
            }
        }
        public string Text
        {
            get
            {
                return _Text;
            }
            set
            {
                _Text = value;
                NotifyPropertyChanged("Text");
            }
        }
        public double Progress
        {
            get
            {
                return _Progress;
            }
            set
            {
                _Progress = value;
                NotifyPropertyChanged("Progress");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        void NotifyPropertyChanged(string name)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(name));
        }
    }
}
