using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Net.NetworkInformation;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace Iphone
{
    public partial class Home : Form    {
        SqlConnection conn;
        public Home()
        {
            InitializeComponent();
            createConnection();
        }
        private void createConnection()
        {
            try
            {
                String stringConnection = "Server=DESKTOP-TMCDUUR\\LUCKDAT;Database=Iphone; Integrated Security = true";
                conn = new SqlConnection(stringConnection);
                MessageBox.Show(" Connection Successful");
            }
            catch (Exception ex)
            {
                MessageBox.Show(" Erorr createconnection " + ex.Message);
            }

        }

        private void DisplayDataSuppliers()
        {

            try
            {
                conn.Open();
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                string sql = "select * from Suppliers";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                DataTable data = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(data);
                dgv1.DataSource = data;
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erorr DisplayData Supplier" + ex.Message);
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }

        }


        private void btnview1_Click(object sender, EventArgs e)
        {
            DisplayDataSuppliers();
        }

        private void AddSuppliers()
        {
            try
            {
                conn.Open();
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandType = System.Data.CommandType.Text;

                String sql = " INSERT INTO Suppliers (SupplierName, ContactName, Email, Phone, Address) " +
                    "VALUES (@SupplierName, @ContactName,@Email, @Phone, @Address)";

                cmd.Parameters.Add("@SupplierName", SqlDbType.NVarChar);
                cmd.Parameters["@SupplierName"].Value = txtsupplierName.Text.ToString();

                cmd.Parameters.Add("@ContactName", SqlDbType.NVarChar);
                cmd.Parameters["@ContactName"].Value = txtcontactName.Text.ToString();

                cmd.Parameters.Add("@Email", SqlDbType.VarChar);
                cmd.Parameters["@Email"].Value = txtemail.Text.ToString();

                cmd.Parameters.Add("@Phone", SqlDbType.NVarChar);
                cmd.Parameters["@Phone"].Value = txtphone.Text.ToString();

                cmd.Parameters.Add("@Address", SqlDbType.NVarChar);
                cmd.Parameters["@Address"].Value = txtaddress.Text.ToString();

                cmd.CommandText = sql;

                cmd.ExecuteNonQuery();

                MessageBox.Show(" Create Successful Suppliers");
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(" Erorr Create Suppliers " + ex.Message);
            }
        }


        private void UpdateSuppliers()
        {
            try
            {
                conn.Open();

                SqlCommand cmd = conn.CreateCommand();

                cmd.CommandType = System.Data.CommandType.Text;
                string sql = "UPDATE Suppliers  SET SupplierName = @SupplierName, ContactName = @ContactName, " +
                    "Email = @Email, Phone= @Phone, Address = @Address WHERE SupplierID = @SupplierID";

                cmd.Parameters.Add("@SupplierID", SqlDbType.Int);
                cmd.Parameters["@SupplierID"].Value = Convert.ToInt32(txtsupplierID.Text.ToString());

                cmd.Parameters.Add("@SupplierName", SqlDbType.NVarChar);
                cmd.Parameters["@SupplierName"].Value = txtsupplierName.Text.ToString();

                cmd.Parameters.Add("@ContactName", SqlDbType.NVarChar);
                cmd.Parameters["@ContactName"].Value = txtcontactName.Text.ToString();

                cmd.Parameters.Add("@Email", SqlDbType.VarChar);
                cmd.Parameters["@Email"].Value = txtemail.Text.ToString();

                cmd.Parameters.Add("@Phone", SqlDbType.NVarChar);
                cmd.Parameters["@Phone"].Value = txtphone.Text.ToString();

                cmd.Parameters.Add("@Address", SqlDbType.NVarChar);
                cmd.Parameters["@Address"].Value = txtaddress.Text.ToString();

                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();

                MessageBox.Show(" Successful Update ");
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(" Erorr Update");
            }
        }


        private void btnupdateSupplierID_Click_1(object sender, EventArgs e)
        {
            DisplayDataSuppliers();
            UpdateSuppliers();
        }

        private void btnaddSupplier_Click(object sender, EventArgs e)
        {
            DisplayDataSuppliers();
            AddSuppliers();
        }

        private void btnexitSupplierID_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Do you want to exit ?", "Ok", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                //Application.Exit();
                Login LoginForm = new Login();
                LoginForm.Show();
                this.Hide();
            }
            else
            {
                return;
            }
        }

        private void DeleteSuppliers()
        {
            try
            {
                conn.Open();

                SqlCommand cmd = conn.CreateCommand();

                cmd.CommandType = System.Data.CommandType.Text;

                String sql = " DELETE FROM Suppliers WHERE SupplierID = @SupplierID";

                cmd.Parameters.Add("@SupplierID", SqlDbType.Int);
                cmd.Parameters["@SupplierID"].Value = Convert.ToInt32(txtsupplierID.Text.ToString());

                cmd.CommandText = sql;

                cmd.ExecuteNonQuery();
                MessageBox.Show(" Delete Successful");
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(" Erorr Delete Suppliers " + ex.Message);
            }
        }

        private void btndeleteSupplierID_Click(object sender, EventArgs e)
        {
            String supplierID = btndeleteSupplierID.Text.ToString();
            DialogResult re = MessageBox.Show(" Delete " + supplierID, "ok", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (re == DialogResult.Yes)
            {
                DisplayDataSuppliers();
                DeleteSuppliers();
            }
            DisplayDataSuppliers();
            DeleteSuppliers();
        }

        private void SearchSuppliers(string SupplierID)
        {
            try
            {
                conn.Open();

                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                string sql = "SELECT SupplierID, SupplierName, ContactName, Email, Phone, Address FROM Suppliers WHERE SupplierID = @SupplierID";
                cmd.CommandText = sql;
                cmd.Parameters.Add("@SupplierID", SqlDbType.Int);
                cmd.Parameters["@SupplierID"].Value = SupplierID;

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    string supplierIdResult = reader["SupplierID"].ToString();
                    string supplierNameResult = reader["SupplierName"].ToString();
                    string contactNameResult = reader["ContactName"].ToString();
                    string emailResult = reader["Email"].ToString();
                    string phoneResult = reader["Phone"].ToString();
                    string addressResult = reader["Address"].ToString();

                    MessageBox.Show("Supplier ID: " + supplierIdResult + "\n" +
                         "Supplier Name: " + supplierNameResult + "\n" +
                         "Contact Name: " + contactNameResult + "\n" +
                         "Email: " + emailResult + "\n" +
                         "Phone: " + phoneResult + "\n" +
                         "Address: " + addressResult);
                }


                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(" Erorr Search Staff: " + ex.Message);
            }
        }
        private void btnsearchSupplierID_Click(object sender, EventArgs e)
        {
            string SupplierID = txtsupplierID.Text;
            SearchSuppliers(SupplierID);
            DisplayDataSuppliers();
        }

        private void DisplayDataEmployees()
        {

            try
            {
                conn.Open();
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                string sql = "select * from Employees";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                DataTable data = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(data);
                dgv2.DataSource = data;
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erorr DisplayData Employee" + ex.Message);
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }

        }


        private void AddEmployees()
        {
            try
            {
                conn.Open();
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandType = System.Data.CommandType.Text;

                String sql = " INSERT INTO Employees (EmployeeName, Position, Username, Password, Authority) VALUES (@EmployeeName, @Position, @Username, @Password, @Authority)";

                cmd.Parameters.Add("@EmployeeName", SqlDbType.NVarChar);
                cmd.Parameters["@EmployeeName"].Value = txtemployeeName.Text.ToString();

                cmd.Parameters.Add("@Position", SqlDbType.NVarChar);
                cmd.Parameters["@Position"].Value = txtposition.Text.ToString();

                cmd.Parameters.Add("@Username", SqlDbType.NVarChar);
                cmd.Parameters["@Username"].Value = txtusername.Text.ToString();

                cmd.Parameters.Add("@Password", SqlDbType.NVarChar);
                cmd.Parameters["@Password"].Value = txtpassword.Text.ToString();

                cmd.Parameters.Add("@Authority", SqlDbType.NVarChar);
                cmd.Parameters["@Authority"].Value = txtauthority.Text.ToString();

                cmd.CommandText = sql;

                cmd.ExecuteNonQuery();

                MessageBox.Show(" Create Successful Employee");
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(" Erorr Create Employee " + ex.Message);
            }
        }

        private void btnaddEmloyee_Click(object sender, EventArgs e)
        {
            AddEmployees();
            DisplayDataEmployees();
        }

        private void btnview2_Click(object sender, EventArgs e)
        {
            DisplayDataEmployees();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Do you want to exit ?", "Ok", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                //Application.Exit();
                Login LoginForm = new Login();
                LoginForm.Show();
                this.Hide();
            }
            else
            {
                return;
            }
        }

        private void UpdateEmployees()
        {
            try
            {
                conn.Open();

                SqlCommand cmd = conn.CreateCommand();

                cmd.CommandType = System.Data.CommandType.Text;
                string sql = "UPDATE Employees  SET EmployeeName = @EmployeeName, Position= @Position, Username= @Username, Password= @Password, Authority= @Authority WHERE EmployeeID = @EmployeeID";

                cmd.Parameters.Add("@EmployeeID", SqlDbType.Int);
                cmd.Parameters["@EmployeeID"].Value = Convert.ToInt32(txtemployeeID.Text.ToString());

                cmd.Parameters.Add("@EmployeeName", SqlDbType.NVarChar);
                cmd.Parameters["@EmployeeName"].Value = txtemployeeName.Text.ToString();

                cmd.Parameters.Add("@Position", SqlDbType.NVarChar);
                cmd.Parameters["@Position"].Value = txtposition.Text.ToString();

                cmd.Parameters.Add("@Username", SqlDbType.NVarChar);
                cmd.Parameters["@Username"].Value = txtusername.Text.ToString();

                cmd.Parameters.Add("@Password", SqlDbType.NVarChar);
                cmd.Parameters["@Password"].Value = txtpassword.Text.ToString();

                cmd.Parameters.Add("@Authority", SqlDbType.NVarChar);
                cmd.Parameters["@Authority"].Value = txtauthority.Text.ToString();

                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();

                MessageBox.Show(" Successful Update ");
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(" Erorr Update");
            }
        }


        private void btnupdateEmloyeeID_Click(object sender, EventArgs e)
        {
            DisplayDataEmployees();
            UpdateEmployees();

        }

        private void DeleteEmployees()
        {
            try
            {
                conn.Open();

                SqlCommand cmd = conn.CreateCommand();

                cmd.CommandType = System.Data.CommandType.Text;

                String sql = " DELETE FROM Employees WHERE EmployeeID = @EmployeeID";

                cmd.Parameters.Add("@EmployeeID", SqlDbType.Int);
                cmd.Parameters["@EmployeeID"].Value = Convert.ToInt32(txtemployeeID.Text.ToString());

                cmd.CommandText = sql;

                cmd.ExecuteNonQuery();
                MessageBox.Show(" Delete EmployeeID");
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(" Erorr Delete Employee " + ex.Message);
            }
        }

        private void btndeleteEmloyeeID_Click(object sender, EventArgs e)
        {
            String EmployeeID = btndeleteEmployeeID.Text.ToString();
            DialogResult re = MessageBox.Show(" Delete Employee " + EmployeeID, "ok", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (re == DialogResult.Yes)
            {
                DisplayDataEmployees();
                DeleteEmployees();
            }
            DisplayDataEmployees();
            DeleteEmployees();
        }

        private void SearchEmployees(string EmployeeID)
        {
            try
            {
                conn.Open();

                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                string sql = "SELECT EmployeeID, EmployeeName, Position, Username, Password, Authority FROM Employees WHERE EmployeeID = @EmployeeID";
                cmd.CommandText = sql;
                cmd.Parameters.Add("@EmployeeID", SqlDbType.Int);
                cmd.Parameters["@EmployeeID"].Value = EmployeeID;

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    string employeeIDResult = reader["EmployeeID"].ToString();
                    string employeeNameResult = reader["EmployeeName"].ToString();
                    string positionResult = reader["Position"].ToString();
                    string usernameResult = reader["Username"].ToString();
                    string passwordResult = reader["Password"].ToString();
                    string authorityResult = reader["Authority"].ToString();

                    MessageBox.Show("Employee ID: " + employeeIDResult + "\n" +
                        "Employee Name: " + employeeNameResult + "\n" +
                         "Position: " + positionResult + "\n" +
                         "Username: " + usernameResult + "\n" +
                         "Password: " + passwordResult + "\n" +
                         "Authority: " + authorityResult);
                }


                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(" Erorr Search Employee: " + ex.Message);
            }
        }
        private void btnsearchEmloyeeID_Click(object sender, EventArgs e)
        {
            string EmployeeID = txtemployeeID.Text;
            SearchEmployees(EmployeeID);
            DisplayDataEmployees();
        }

        private void btnimage_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();


            openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif|All Files|*.*";
            openFileDialog.Title = "Select a File";


            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {

                txtimage.Text = openFileDialog.FileName;
            }
        }

        private void btnexitProduct_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Do you want to exit ?", "Ok", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                //Application.Exit();
                Login LoginForm = new Login();
                LoginForm.Show();
                this.Hide();
            }
            else
            {
                return;
            }
        }

        private void DisplayDataProduct()
        {

            try
            {
                conn.Open();

                SqlCommand cmd = conn.CreateCommand();

                cmd.CommandType = System.Data.CommandType.Text;

                string sql = "select * from Products";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                DataTable data = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(data);
                dgv3.DataSource = data;
                MessageBox.Show("Successful");
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(" Erorr DisplayData Product " + ex.Message);
            }
            finally
            {

                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }

        }

        private void AddProduct()
        {
            try
            {
                conn.Open();

                SqlCommand cmd = conn.CreateCommand();

                cmd.CommandType = System.Data.CommandType.Text;

                String sql = " INSERT INTO Products (ProductName, SellingPrice, InventoryQuantity, ImportedQuantity, Cost, Image) VALUES (@ProductName, @SellingPrice, @InventoryQuantity, @ImportedQuantity, @Cost, @Image)";

                cmd.Parameters.Add("@ProductName", SqlDbType.NVarChar);
                cmd.Parameters["@ProductName"].Value = txtproductName.Text.ToString();

                cmd.Parameters.Add("@SellingPrice", SqlDbType.Decimal);
                cmd.Parameters["@SellingPrice"].Value = txtsellingPrice.Text.ToString();

                cmd.Parameters.Add("@InventoryQuantity", SqlDbType.Int);
                cmd.Parameters["@InventoryQuantity"].Value = Convert.ToInt32(txtinventoryQuantity.Text.ToString());

                cmd.Parameters.Add("@ImportedQuantity", SqlDbType.Int);
                cmd.Parameters["@ImportedQuantity"].Value = Convert.ToInt32(txtimportedQuantity.Text.ToString());

                cmd.Parameters.Add("@Cost", SqlDbType.Decimal);
                cmd.Parameters["@Cost"].Value = txtcost.Text.ToString();

                byte[] image = File.ReadAllBytes(txtimage.Text);
                cmd.Parameters.Add("@Image", SqlDbType.VarBinary).Value = image;


                cmd.CommandText = sql;

                cmd.ExecuteNonQuery();

                MessageBox.Show(" Successful Create product");
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(" Erorr Create Product " + ex.Message);
            }
        }
        private void btnaddProduct_Click(object sender, EventArgs e)
        {
            AddProduct();
            DisplayDataProduct();
        }

        private void btnview3_Click(object sender, EventArgs e)
        {
            DisplayDataProduct();
        }


        private void UpdateProduct()
        {
            try
            {
                conn.Open();

                SqlCommand cmd = conn.CreateCommand();

                cmd.CommandType = System.Data.CommandType.Text;
                string sql = "UPDATE Products  SET ProductName = @ProductName, SellingPrice = @SellingPrice, InventoryQuantity =@InventoryQuantity, ImportedQuantity= @ImportedQuantity, Cost = @Cost, Image= @Image WHERE ProductID = @ProductID";

                cmd.Parameters.Add("@ProductID", SqlDbType.Int);
                cmd.Parameters["@ProductID"].Value = Convert.ToInt32(txtproductID.Text.ToString());

                cmd.Parameters.Add("@ProductName", SqlDbType.NVarChar);
                cmd.Parameters["@ProductName"].Value = txtproductName.Text.ToString();

                cmd.Parameters.Add("@SellingPrice", SqlDbType.Decimal);
                cmd.Parameters["@SellingPrice"].Value = txtsellingPrice.Text.ToString();

                cmd.Parameters.Add("@InventoryQuantity", SqlDbType.Int);
                cmd.Parameters["@InventoryQuantity"].Value = Convert.ToInt32(txtinventoryQuantity.Text.ToString());

                cmd.Parameters.Add("@ImportedQuantity", SqlDbType.Int);
                cmd.Parameters["@ImportedQuantity"].Value = Convert.ToInt32(txtimportedQuantity.Text.ToString());

                cmd.Parameters.Add("@Cost", SqlDbType.Decimal);
                cmd.Parameters["@Cost"].Value = txtcost.Text.ToString();

                byte[] image = File.ReadAllBytes(txtimage.Text);
                cmd.Parameters.Add("@Image", SqlDbType.VarBinary).Value = image;


                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();

                MessageBox.Show(" Successful Edit ");
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(" Erorr Edit");
            }
        }

        private void btnupdateProduct_Click(object sender, EventArgs e)
        {
            UpdateProduct();
            DisplayDataProduct();
        }

        private void DeleteProduct()
        {
            try
            {
                conn.Open();

                SqlCommand cmd = conn.CreateCommand();

                cmd.CommandType = System.Data.CommandType.Text;

                String sql = " DELETE FROM Products WHERE ProductID = @ProductID";

                cmd.Parameters.Add("@ProductID", SqlDbType.Int);
                cmd.Parameters["@ProductID"].Value = Convert.ToInt32(txtproductID.Text.ToString());

                cmd.CommandText = sql;

                cmd.ExecuteNonQuery();
                MessageBox.Show(" Delete Product");
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(" Erorr Delete Product " + ex.Message);
            }
        }

        private void btndeleteProduct_Click(object sender, EventArgs e)
        {
            String ProductID = btndeleteProduct.Text.ToString();
            DialogResult re = MessageBox.Show(" Delete Product " + ProductID, "ok", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (re == DialogResult.Yes)
            {
                DisplayDataProduct();
                DeleteProduct();
            }
            DisplayDataProduct();
            DeleteProduct();
        }


        private void SearchProduct(string ProductID)
        {
            try
            {
                conn.Open();

                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                string sql = "SELECT ProductID,ProductName, SellingPrice, InventoryQuantity, ImportedQuantity, Cost, Image FROM Products WHERE ProductID = @ProductID";
                cmd.CommandText = sql;
                cmd.Parameters.Add("@ProductID", SqlDbType.Int);
                cmd.Parameters["@ProductID"].Value = ProductID;

                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    // Đọc dữ liệu đúng theo truy vấn SQL
                    string productIDResult = reader["ProductID"].ToString();
                    string productNameResult = reader["ProductName"].ToString();
                    string sellingPriceResult = reader["SellingPrice"].ToString();
                    string inventoryQuantityResult = reader["InventoryQuantity"].ToString();
                    string importedQuantityResult = reader["ImportedQuantity"].ToString();
                    string costResult = reader["Cost"].ToString();

                    // Xử lý hình ảnh từ cột 'Image'
                    if (!reader.IsDBNull(reader.GetOrdinal("Image")))
                    {
                        byte[] imageData = (byte[])reader["Image"];
                        using (MemoryStream ms = new MemoryStream(imageData))
                        {
                            pictureBoxProduct.Image = Image.FromStream(ms);
                        }
                    }
                    else
                    {
                        pictureBoxProduct.Image = null; // Nếu không có ảnh
                    }

                    // Hiển thị thông tin sản phẩm
                    MessageBox.Show("Product ID: " + productIDResult + "\n" +
                                     "Product Name: " + productNameResult + "\n" +
                                    "Selling Price: " + sellingPriceResult + "\n" +
                                    "Inventory Quantity: " + inventoryQuantityResult + "\n" +
                                    "Imported Quantity: " + importedQuantityResult + "\n" +
                                    "Cost: " + costResult);
                }

                else
                {
                    MessageBox.Show("No product found with this product Id.");
                }

                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lấy thông tin Product theo ProductId: " + ex.Message);
            }
        }

        private void btnsearchProduct_Click(object sender, EventArgs e)
        {
            string ProductID = txtproductID.Text;
            SearchProduct(ProductID);
            DisplayDataProduct();
        }

        private void btnexitCustomer_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Do you want to exit ?", "Ok", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                //Application.Exit();
                Login LoginForm = new Login();
                LoginForm.Show();
                this.Hide();
            }
            else
            {
                return;
            }
        }

        private void DisplayDataCustomer()
        {

            try
            {
                conn.Open();

                SqlCommand cmd = conn.CreateCommand();

                cmd.CommandType = System.Data.CommandType.Text;

                string sql = "select * from Customers";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                DataTable data = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(data);
                dgv4.DataSource = data;
                MessageBox.Show("Successful");
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(" Erorr DisplayData Customer" + ex.Message);
            }
            finally
            {

                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }

        }

        private void AddCustomer()
        {
            try
            {
                conn.Open();

                // Tạo một SqlCommand mới
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandType = System.Data.CommandType.Text;

                // Chuỗi SQL cho bảng Customers mới
                string sql = "INSERT INTO Customers (CustomerName, Gender, Email, PhoneNumber, Address) VALUES (@CustomerName, @Gender, @Email, @PhoneNumber, @Address)";

                // Thêm tham số cho câu lệnh SQL
                cmd.Parameters.Add("@CustomerName", SqlDbType.NVarChar, 100);
                cmd.Parameters["@CustomerName"].Value = txtcustomerName.Text;

                cmd.Parameters.Add("@Gender", SqlDbType.VarChar, 20);
                cmd.Parameters["@Gender"].Value = cbbgender.SelectedItem?.ToString() ?? "Not Specified";

                cmd.Parameters.Add("@Email", SqlDbType.VarChar, 255);
                cmd.Parameters["@Email"].Value = txtemail1.Text;

                cmd.Parameters.Add("@PhoneNumber", SqlDbType.NVarChar, 15);
                cmd.Parameters["@PhoneNumber"].Value = txtphone1.Text;

                cmd.Parameters.Add("@Address", SqlDbType.NVarChar, 255);
                cmd.Parameters["@Address"].Value = txtaddress1.Text;


                // Gán chuỗi SQL vào SqlCommand
                cmd.CommandText = sql;

                // Thực thi câu lệnh SQL
                cmd.ExecuteNonQuery();

                MessageBox.Show("Customer created successfully.");
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error creating customer: " + ex.Message);
            }
        }

        private void btnview4_Click(object sender, EventArgs e)
        {
            DisplayDataCustomer();
        }

        private void btnaddCustomer_Click(object sender, EventArgs e)
        {
            DisplayDataCustomer();
            AddCustomer();
        }

        private void UpdateCustomer()
        {
            try
            {
                conn.Open();

                SqlCommand cmd = conn.CreateCommand();

                cmd.CommandType = System.Data.CommandType.Text;
                string sql = "UPDATE Customers  SET CustomerName = @CustomerName, Gender= @Gender, Email= @Email, PhoneNumber= @PhoneNumber, Address = @Address WHERE CustomerID = @CustomerID";

                cmd.Parameters.Add("@CustomerID", SqlDbType.Int);
                cmd.Parameters["@CustomerID"].Value = Convert.ToInt32(txtcustomerID.Text.ToString());

                cmd.Parameters.Add("@CustomerName", SqlDbType.NVarChar, 100);
                cmd.Parameters["@CustomerName"].Value = txtcustomerName.Text;

                cmd.Parameters.Add("@Gender", SqlDbType.VarChar, 20);
                cmd.Parameters["@Gender"].Value = cbbgender.SelectedItem?.ToString() ?? "Not Specified";

                cmd.Parameters.Add("@Email", SqlDbType.VarChar, 255);
                cmd.Parameters["@Email"].Value = txtemail1.Text;

                cmd.Parameters.Add("@PhoneNumber", SqlDbType.NVarChar, 15);
                cmd.Parameters["@PhoneNumber"].Value = txtphone1.Text;

                cmd.Parameters.Add("@Address", SqlDbType.NVarChar, 255);
                cmd.Parameters["@Address"].Value = txtaddress1.Text;


                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();

                MessageBox.Show(" Successful Edit ");
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(" Erorr Edit");
            }
        }
        private void btnupdateCustomer_Click(object sender, EventArgs e)
        {
            DisplayDataCustomer();
            UpdateCustomer();
        }

        private void DeleteCustomer()
        {
            try
            {
                conn.Open();

                SqlCommand cmd = conn.CreateCommand();

                cmd.CommandType = System.Data.CommandType.Text;

                String sql = " DELETE FROM Customers WHERE CustomerID = @CustomerID";

                cmd.Parameters.Add("@CustomerID", SqlDbType.Int);
                cmd.Parameters["@CustomerID"].Value = Convert.ToInt32(txtcustomerID.Text.ToString());

                cmd.CommandText = sql;

                cmd.ExecuteNonQuery();
                MessageBox.Show(" Delete Customer");
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(" Erorr Delete Customer " + ex.Message);
            }
        }
        private void btndeleteCustomer_Click(object sender, EventArgs e)
        {
            String CustomerID = btndeleteCustomer.Text.ToString();
            DialogResult re = MessageBox.Show(" Delete Customer " + CustomerID, "ok", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (re == DialogResult.Yes)
            {
                DisplayDataCustomer();
                DeleteCustomer();
            }
            DisplayDataCustomer();
            DeleteCustomer();
        }

        private void SearchCustomer(string CustomerID)
        {
            try
            {
                conn.Open();

                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                string sql = "SELECT CustomerID, CustomerName, Gender, Email, PhoneNumber, Address FROM Customers WHERE CustomerID = @CustomerID";
                cmd.CommandText = sql;
                cmd.Parameters.Add("@CustomerID", SqlDbType.Int);
                cmd.Parameters["@CustomerID"].Value = CustomerID;

                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    // Đọc dữ liệu đúng theo truy vấn SQL
                    string customerIdResult = reader["CustomerID"].ToString();
                    string customerNameResult = reader["CustomerName"].ToString();
                    string genderResult = reader["Gender"].ToString();
                    string emailResult = reader["Email"].ToString();
                    string phoneNumberResult = reader["PhoneNumber"].ToString();
                    string addressResult = reader["Address"].ToString();

                    // Hiển thị thông tin khách hàng
                    MessageBox.Show("Customer ID: " + customerIdResult + "\n" +
                                    "Customer Name: " + customerNameResult + "\n" +
                                    "Gender: " + genderResult + "\n" +
                                    "Email: " + emailResult + "\n" +
                                    "Phone Number: " + phoneNumberResult + "\n" +
                                    "Address: " + addressResult);
                }


                else
                {
                    MessageBox.Show("No product found with this product Id.");
                }

                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lấy thông tin Product theo ProductId: " + ex.Message);
            }
        }

        private void btnsearchCustomer_Click(object sender, EventArgs e)
        {
            string CustomerID = txtcustomerID.Text;
            SearchCustomer(CustomerID);
            DisplayDataCustomer();
        }




        private void DisplayDataSearchInvoices()
        {

            try
            {
                conn.Open();

                SqlCommand cmd = conn.CreateCommand();

                cmd.CommandType = System.Data.CommandType.Text;

                string sql = "select * from Invoices";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                DataTable data = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(data);
                dgv6.DataSource = data;
                MessageBox.Show("Successful");
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(" Erorr DisplayData Invoices" + ex.Message);
            }
            finally
            {

                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }

        }
        private DataTable GetFilteredInvoices(int? invoiceId, int? employeeId, DateTime? minDate, DateTime? maxDate)
        {
            if (conn.State == ConnectionState.Closed)
                conn.Open();

            StringBuilder query = new StringBuilder("SELECT * FROM Invoices WHERE 1 = 1");

            if (invoiceId.HasValue)
            {
                query.Append(" AND InvoiceID = @invoiceId");
            }
            if (employeeId.HasValue)
            {
                query.Append(" AND EmployeeID = @employeeId");
            }
            if (minDate.HasValue)
            {
                query.Append(" AND InvoiceDate >= @minDate");
            }
            if (maxDate.HasValue)
            {
                query.Append(" AND InvoiceDate <= @maxDate");
            }

            using (SqlCommand command = new SqlCommand(query.ToString(), conn))
            {
                if (invoiceId.HasValue)
                {
                    command.Parameters.Add(new SqlParameter("@invoiceId", SqlDbType.Int) { Value = invoiceId.Value });
                }
                if (employeeId.HasValue)
                {
                    command.Parameters.Add(new SqlParameter("@employeeId", SqlDbType.Int) { Value = employeeId.Value });
                }
                if (minDate.HasValue)
                {
                    command.Parameters.Add(new SqlParameter("@minDate", SqlDbType.DateTime) { Value = minDate.Value });
                }
                if (maxDate.HasValue)
                {
                    command.Parameters.Add(new SqlParameter("@maxDate", SqlDbType.DateTime) { Value = maxDate.Value });
                }

                DataTable dt = new DataTable();
                try
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    adapter.Fill(dt);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi truy vấn dữ liệu: " + ex.Message);
                }
                finally
                {
                    conn.Close();
                }

                return dt;
            }
        }

        private void btnsearchInvoice_Click(object sender, EventArgs e)
        {
             string invoiceIdText = txtinvoiceID.Text.Trim();
                string employeeIdText = txtemployeeID1.Text.Trim();
                string minDateText = txtMinDate.Text.Trim();
                string maxDateText = txtMaxDate.Text.Trim();

                int? invoiceId = string.IsNullOrWhiteSpace(invoiceIdText) ? (int?)null : Convert.ToInt32(invoiceIdText);
                int? employeeId = string.IsNullOrWhiteSpace(employeeIdText) ? (int?)null : Convert.ToInt32(employeeIdText);
                DateTime? minDate = string.IsNullOrWhiteSpace(minDateText) ? (DateTime?)null : Convert.ToDateTime(minDateText);
                DateTime? maxDate = string.IsNullOrWhiteSpace(maxDateText) ? (DateTime?)null : Convert.ToDateTime(maxDateText);

                DataTable filteredInvoices = GetFilteredInvoices(invoiceId, employeeId, minDate, maxDate);

                if (filteredInvoices.Rows.Count == 0)
                {
                    MessageBox.Show("Không tìm thấy hóa đơn nào phù hợp với tiêu chí tìm kiếm.");
                    dgv6.DataSource = null;
                }
                else
                {
                    dgv6.DataSource = filteredInvoices;
                }       

        }

        private void btnviewInvoices_Click(object sender, EventArgs e)
        {
            DisplayDataSearchInvoices();
        }

        private void btnexitInvoices_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Do you want to exit ?", "Ok", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                //Application.Exit();
                Login LoginForm = new Login();
                LoginForm.Show();
                this.Hide();
            }
            else
            {
                return;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Do you want to exit ?", "Ok", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                //Application.Exit();
                Login LoginForm = new Login();
                LoginForm.Show();
                this.Hide();
            }
            else
            {
                return;
            }
        }

        private void DisplayDataInvoices()
        {

            try
            {
                conn.Open();

                SqlCommand cmd = conn.CreateCommand();

                cmd.CommandType = System.Data.CommandType.Text;

                string sql = "SELECT InvoiceID, EmployeeID, EmployeeName, InvoiceDate, Amount, PaymentMethod\r\nFROM Invoices;\r\n";
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                DataTable data = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(data);
                dgv5.DataSource = data;
                MessageBox.Show("Successful");
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(" Erorr DisplayData Invoices" + ex.Message);
            }
            finally
            {

                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }

        }

        private void AddInvoices()
        {
            try
            {
                conn.Open();

                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandType = System.Data.CommandType.Text;

          
                string sql = "INSERT INTO Invoices (EmployeeID, EmployeeName, Amount, PaymentMethod) VALUES ( @EmployeeID, @EmployeeName, @Amount, @PaymentMethod)";


                cmd.Parameters.Add("@EmployeeID", SqlDbType.Int);
                cmd.Parameters["@EmployeeID"].Value = Convert.ToInt32(txtemployeeID11.Text.ToString());

                cmd.Parameters.Add("@EmployeeName", SqlDbType.NVarChar);
                cmd.Parameters["@EmployeeName"].Value = txtemployeeName11.Text;

                cmd.Parameters.Add("@Amount", SqlDbType.Decimal);
                cmd.Parameters["@Amount"].Value = Convert.ToDecimal(txtamount.Text);

                cmd.Parameters.Add("@PaymentMethod", SqlDbType.NVarChar);
                cmd.Parameters["@PaymentMethod"].Value = cbbpayment.SelectedItem?.ToString() ?? "Not Specified";

               
                cmd.CommandText = sql;

         
                cmd.ExecuteNonQuery();

                MessageBox.Show("Invoice created successfully.");
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error creating invoice: " + ex.Message);
            }
        }

        private void btnview5_Click(object sender, EventArgs e)
        {
            DisplayDataInvoices();
        }

        private void btnaddInvoices_Click(object sender, EventArgs e)
        {
            DisplayDataInvoices();
            AddInvoices();
        }

        private void DeleteInvoice(int invoiceID)
        {
            try
            {
                conn.Open();

                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandType = System.Data.CommandType.Text;

                string sql = "DELETE FROM Invoices WHERE InvoiceID = @InvoiceID";
                cmd.CommandText = sql;

                cmd.Parameters.Add("@InvoiceID", SqlDbType.Int).Value = invoiceID;

                cmd.ExecuteNonQuery();

                MessageBox.Show("Invoice deleted successfully.");
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error deleting invoice: " + ex.Message);
            }
        }

        private void btndeleteInvoice_Click(object sender, EventArgs e)
        {
            try
            {
              
                if (dgv5.SelectedRows.Count > 0)
                {
                    int selectedInvoiceID = Convert.ToInt32(dgv5.SelectedRows[0].Cells["InvoiceID"].Value);

                    DialogResult re = MessageBox.Show("Do you really want to delete InvoiceID " + selectedInvoiceID + "?",
                        "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (re == DialogResult.Yes)
                    {
                        DeleteInvoice(selectedInvoiceID);
                        DisplayDataInvoices(); 
                    }
                }
                else
                {
                    MessageBox.Show("Please select an invoice to delete.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
    }
}
