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
        static string connString = Properties.Settings.Default.TriTabeleConnectionString;
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
                //SelectDataFromDatabase();
                //FillDataGrid();

                StudentView studentView = new StudentView();
                this.Close();
                studentView.ShowDialog();


                //txtName.Text = "";
                //txtSurname.Text = "";

            }
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            //try
            //{
            //    sqlConn.Open();
            //    //SqlCommand comm = new SqlCommand("UPDATE tblEmployee SET Name='" + txtBox1.Text + "',Salary=" + txtBox2.Text + ",YearOfService=" + txtBox3 + ",IsInManagement='" + txtBox4 + "'", sqlConn);
            //    string insertQuery = string.Format("UPDATE tblEmployee SET Name = '{0}', Salary = '{1}', YearOfService = '{2}', IsInManagement = '{3}' WHERE EmployeeID = '{4}'", txtBox1.Text, txtBox2.Text, txtBox3.Text, txtBox4.Text, txtBox5.Text);
            //    SqlCommand comm = new SqlCommand(insertQuery, sqlConn);
            //    comm.ExecuteNonQuery();
            //    MessageBox.Show("Employees succesfully updated!");
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message.ToString());
            //}
            //finally
            //{
            //    sqlConn.Close();
            //    BindMyData();
            //    txtBox1.Text = "";
            //    txtBox2.Text = "";
            //    txtBox3.Text = "";
            //    txtBox4.Text = "";
            //    txtBox5.Text = "";
            //}
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            //try
            //{
            //    sqlConn.Open();
            //    //SqlCommand comm = new SqlCommand("DELETE FROM tblEmployee WHERE EmployeeID=" + txtStudId.Text + "", sqlConn);
            //    //string insertQuery = string.Format("DELETE FROM tblEmployee WHERE Name = '{0}'", txtBox1.Text);
            //    string insertQuery = string.Format("DELETE FROM tblEmployee WHERE EmployeeID = '{0}'", txtBox5.Text);
            //    SqlCommand comm = new SqlCommand(insertQuery, sqlConn);
            //    comm.ExecuteNonQuery();
            //    MessageBox.Show("Employees succesfully deleted!");
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message.ToString());
            //}
            //finally
            //{
            //    sqlConn.Close();
            //    BindMyData();
            //    txtBox1.Text = "";
            //    txtBox2.Text = "";
            //    txtBox3.Text = "";
            //    txtBox4.Text = "";
            //    txtBox5.Text = "";
            //}
        }
    }
}

