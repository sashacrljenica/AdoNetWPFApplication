using SasaCrljenica_AdoNetWPFApplication.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SasaCrljenica_AdoNetWPFApplication.View
{
    /// <summary>
    /// Interaction logic for StudentView.xaml
    /// </summary>
    public partial class StudentView : Window
    {
        static string connString = Properties.Settings.Default.TriTabeleConnectionString1;
        SqlConnection sqlConn = new SqlConnection(connString);

        public StudentView()
        {
            InitializeComponent();
            SelectDataFromDatabase();
            FillDataGrid();


        }

        private List<Student> listStudent = new List<Student>();

        internal List<Student> ListStudent
        {
            get
            {
                return listStudent;
            }

            set
            {
                listStudent = value;
            }
        }

        public void FillDataGrid()
        {
            try
            {
                //sqlConn.Open();
                //string query = string.Format("SELECT StudentName as Name, SurName as Surname FROM tblStudent");
                //SqlCommand comm = new SqlCommand(query, sqlConn);

                //// Premoscavanje podataka iz baze u memoriju
                //SqlDataAdapter da = new SqlDataAdapter(comm);

                ////In memory database
                //DataSet ds = new DataSet();
                //da.Fill(ds);

                //dataGridStudent.ItemsSource = ds.Tables[0].DefaultView;

                //DataTable t = new DataTable();
                //SqlDataAdapter a = new SqlDataAdapter(comm);

                //a.Fill(t);

                //DataSet set = new DataSet();

                //set.Tables.Add(t);

                //dataGridStudent.ItemsSource = set.Tables[0].DefaultView;

                dataGridStudent.ItemsSource = listStudent;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
            finally
            {
                sqlConn.Close();
            }
        }

        private void SelectDataFromDatabase()
        {
            string query = "select * from tblStudent";

            try
            {
                SqlCommand sqlComm = new SqlCommand(query, sqlConn);
                sqlConn.Open();

                SqlDataReader sqlDR = sqlComm.ExecuteReader();

                listStudent.Clear();

                while (sqlDR.Read())
                {
                    Student obj = new Student();
                    obj.StudentID = Convert.ToInt32(sqlDR["StudentID"]);
                    obj.Name = sqlDR["StudentName"].ToString();
                    obj.Surname = sqlDR["SurName"].ToString();

                    listStudent.Add(obj);
                }
                sqlDR.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                sqlConn.Close();
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            string query = string.Format("insert into tblStudent values ('{0}','{1}')", txtName.Text, txtSurname.Text);
            try
            {
                sqlConn.Open();

                SqlCommand comm = new SqlCommand(query, sqlConn);
                comm.ExecuteNonQuery();
                MessageBox.Show("Student succesfully added!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
            finally
            {
                sqlConn.Close();

                StudentView studentView = new StudentView();
                this.Close();
                studentView.ShowDialog();

            }
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            Student student = new Student();
            try
            {
                sqlConn.Open();

                string query1 = string.Format("select * from tblStudent where StudentName='{0}' and SurName='{1}';", txtOldName.Text, txtOldSurname.Text);
                SqlCommand comm1 = new SqlCommand(query1, sqlConn);
                SqlDataReader sqlDR = comm1.ExecuteReader();

                if (sqlDR.Read())
                {
                    student.StudentID = Convert.ToInt32(sqlDR["StudentID"]);
                    student.Name = sqlDR["StudentName"].ToString();
                    student.Surname = sqlDR["SurName"].ToString();
                }
                sqlDR.Close();

                string query2 = string.Format("update tblStudent set StudentName='{0}', SurName='{1}' where StudentID='{2}';", txtName.Text, txtSurname.Text, student.StudentID);
                SqlCommand comm2 = new SqlCommand(query2, sqlConn);
                comm2.ExecuteNonQuery();
                MessageBox.Show("Student succesfully updated!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
            finally
            {
                sqlConn.Close();

                StudentView studentView = new StudentView();
                this.Close();
                studentView.ShowDialog();
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            Student student = new Student();
            try
            {
                sqlConn.Open();

                string query1 = string.Format("select * from tblStudent where StudentName='{0}' and SurName='{1}';", txtOldName.Text, txtOldSurname.Text);
                SqlCommand comm1 = new SqlCommand(query1, sqlConn);
                SqlDataReader sqlDR = comm1.ExecuteReader();

                if (sqlDR.Read())
                {
                    student.StudentID = Convert.ToInt32(sqlDR["StudentID"]);
                    student.Name = sqlDR["StudentName"].ToString();
                    student.Surname = sqlDR["SurName"].ToString();
                }
                sqlDR.Close();

                string query2 = string.Format("delete from tblStudent where StudentID='{0}';", student.StudentID);
                SqlCommand comm2 = new SqlCommand(query2, sqlConn);
                comm2.ExecuteNonQuery();
                MessageBox.Show("Student deleted succesfully!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
            finally
            {
                sqlConn.Close();

                StudentView studentView = new StudentView();
                this.Close();
                studentView.ShowDialog();
            }
        }
    }
}

