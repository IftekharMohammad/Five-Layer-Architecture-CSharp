using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OOP2.HR.Entities;
using OOP2.HR.Framework;
using OOP2.HR.Repo;

namespace WFAlayer
{
    public partial class Home : Form
    {

		private EmpRepo _repo = new EmpRepo();
		private List<Emp> _currentDatas = new List<Emp>(); //caching data for further usage
		private Emp _selectedData;
	    private bool IsNew=false;

        public Home()
        {
            InitializeComponent();
        }

        private void Home_Load(object sender, EventArgs e)
        {
	        this.InitForm();
        }

	    private void InitForm()
	    {
		    txtSearch.Text = "";
		    this.LoadEmployees();
	    }

	    private void LoadEmployees()
	    {
		    var result = _repo.GetAll(txtSearch.Text);

		    if (result.HasError)
		    {
			    MessageBox.Show(result.Message);
				return;
		    }

		    _currentDatas = result.Data.ToList();

		    if (_currentDatas.Any())
		    {
			    _selectedData = _currentDatas[0];
			    this.PopulateData();
		    }
		    else
		    {
			    this.New();
		    }

			this.RefreshDgv();
	    }

	    private void RefreshDgv()
	    {
		    dgvEmployees.AutoGenerateColumns = false;
		    dgvEmployees.DataSource = _currentDatas.ToList();
			dgvEmployees.Refresh();
			dgvEmployees.ClearSelection();

			if(IsNew)
				return;

		    for (int i = 0; i < dgvEmployees.Rows.Count; i++)
		    {
			    if (dgvEmployees.Rows[i].Cells[0].Value.ToString().Equals(_selectedData.id))
				    dgvEmployees.Rows[i].Selected = true;
		    }
	    }

	    private void PopulateData()
	    {
		    IsNew = !ValidationHelper.IsStringValid(_selectedData.id);
		    txtID.Enabled = IsNew;
		    txtID.Text = _selectedData.id + "";
		    txtFName.Text = _selectedData.fname + "";
		    txtLName.Text = _selectedData.lname + "";
		    dtpDOB.Text = _selectedData.dob + "";
		    txtDept.Text = _selectedData.dept + "";
		    txtSalary.Text = _selectedData.salary + "";
	    }

	    private void New()
	    {
			dgvEmployees.ClearSelection();
			_selectedData = new Emp(){dob = DateTime.Now};
		    this.PopulateData();
	    }

		private void button1_Click(object sender, EventArgs e)
		{
			this.LoadEmployees();
		}

		private void button2_Click(object sender, EventArgs e)
		{
			this.InitForm();
		}

		private void button4_Click(object sender, EventArgs e)
		{
			this.New();
		}

		private void button3_Click(object sender, EventArgs e)
		{
			if (IsNew)
			{
				MessageBox.Show("Please Select A Row First");
				return;
			}

			if(MessageBox.Show("Are You Sure?","Confirmation",MessageBoxButtons.YesNo)==DialogResult.No)
				return;

			var result = _repo.Delete(_selectedData.id);

			if (result.HasError)
			{
				MessageBox.Show(result.Message);
				return;
			}

			var delObject = _currentDatas.FirstOrDefault(c => c.id == _selectedData.id);

			if (delObject != null)
				_currentDatas.Remove(delObject);

			this.New();
			this.RefreshDgv();

			MessageBox.Show("Operation Completed");
		}

		private void dgvEmployees_CellClick(object sender, DataGridViewCellEventArgs e)
		{
			if (e.RowIndex >= 0)
			{
				string id = dgvEmployees.Rows[e.RowIndex].Cells[0].Value.ToString();
				_selectedData = _currentDatas.FirstOrDefault(f => f.id == id);

				if(_selectedData==null)
					this.New();

				this.PopulateData();
			}
		}

		private void button5_Click(object sender, EventArgs e)
		{
			if(!IsValidToSave())
				return;

			this.FillData();

			var result = _repo.Save(_selectedData);

			if (result.HasError)
			{
				MessageBox.Show(result.Message);
				return;
			}

			if (IsNew)
			{
				_currentDatas.Add(result.Data);
			}
			else
			{
				int index = _currentDatas.FindIndex(a => a.id == result.Data.id);
				if (index < 0)
				{
					MessageBox.Show("Something Went Wrong.Please Click Refresh");
					return;
				}

				_currentDatas[index] = result.Data;
			}

			_selectedData = result.Data;
			this.PopulateData();
			this.RefreshDgv();
			MessageBox.Show("Operation Completed");
		}

	    private void FillData()
	    {
		    _selectedData.id = txtID.Text;
		    _selectedData.fname = txtFName.Text;
		    _selectedData.lname = txtLName.Text;
		    _selectedData.dob = Convert.ToDateTime(dtpDOB.Text);
		    _selectedData.dept = txtDept.Text;
		    _selectedData.salary = float.Parse(txtSalary.Text);
	    }

	    private bool IsValidToSave()
	    {
		    if (!ValidationHelper.IsFloatValid(txtSalary.Text))
		    {
			    MessageBox.Show("Invalid Salary");
			    txtSalary.Focus();
			    return false;
		    }

		    if (!ValidationHelper.IsDateTimeValid(dtpDOB.Text))
		    {
			    MessageBox.Show("Invalid DOB");
			    dtpDOB.Focus();
			    return false;
		    }

			//more validation
			return true;
	    }
    }
}
