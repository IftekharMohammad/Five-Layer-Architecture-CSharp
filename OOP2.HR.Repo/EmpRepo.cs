using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OOP2.HR.Data;
using OOP2.HR.Entities;
using OOP2.HR.Framework;


namespace OOP2.HR.Repo
{
	public class EmpRepo
	{
		public Result<List<Emp>> GetAll(string key = "")
		{
			var result = new Result<List<Emp>>() { Data = new List<Emp>() };
			try
			{
				string query = "Select * from EMP";

				if (ValidationHelper.IsStringValid(key))
				{
					query += " where id='" + key + "' or fname like '%" + key + "%' or lname like '%" + key + "%'";

				}

				var dt = DataAccess.GetDataTable(query);
				for (int i = 0; i < dt.Rows.Count; i++)
				{
					Emp e = ConverToEntity(dt.Rows[i]);
					result.Data.Add(e);
				}
			}
			catch (Exception exc)
			{
				result.HasError = true;
				result.Message = exc.Message;
			}
			return result;
		}

		public Result<Emp> Save(Emp e)
		{
			var result = new Result<Emp>();

			try
			{
				string query = "select * from EMp where id = '" + e.id + "'";
				var dt = DataAccess.GetDataTable(query);

				if (dt == null || dt.Rows.Count == 0)
				{
					query = "Insert into EMP values ('" + e.id + "','" + e.password + "','" + e.fname + "','" + e.lname + "','" + e.dob.ToString("MM/dd/yyyy") + "','" + e.dept + "'," + e.salary + ")";
				}
				else
				{
					query = "Update Emp set password = '" + e.password + "',fname='" + e.fname + "',lname='" + e.lname + "',dob='" + e.dob.ToString("MM/dd/yyyy") + "',dept='" + e.dept + "',salary= " + e.salary + " where id = '" + e.id + "'";
				}

				if (!IsValidToSave(e, result))
					return result;

				result.HasError = DataAccess.ExecuteQuery(query) <= 0;

				if (result.HasError)
				{
					result.Message = "Wrong";
				}
				else
				{
					//execute query to get the autogenrated column's value if any
					query = "select * from EMp where id = '" + e.id + "'";
					dt = DataAccess.GetDataTable(query);
					result.Data = ConverToEntity(dt.Rows[0]);
				}
			}
			catch (Exception exception)
			{
				result.HasError = true;
				result.Message = exception.Message;
			}

			return result;
		}

		public Result<bool> Delete(string id)
		{
			var result = new Result<bool>();

			string query = "select * from EMp where id = '" + id + "'";
			var dt = DataAccess.GetDataTable(query);

			if (dt == null || dt.Rows.Count == 0)
			{
				result.HasError = true;
				result.Message = "Invalid ID";
				return result;
			}

			query = "delete from EMp where id = '" + id + "'";


			result.HasError = DataAccess.ExecuteQuery(query) <= 0;

			if (result.HasError)
			{
				result.Message = "Wrong";
			}
			else
			{
				result.Data = true;
			}

			return result;
		}

		private bool IsValidToSave(Emp emp, Result<Emp> result)
		{
			if (!ValidationHelper.IsStringValid(emp.fname))
			{
				result.HasError = true;
				result.Message = "Invalid Name";
				return false;
			}

			//more validation

			return true;
		}

		private Emp ConverToEntity(DataRow row)
		{
			if (row == null)
			{
				return null;
			}
			Emp e = new Emp();
			e.id = row["id"].ToString();
			e.password = row["password"].ToString();
			e.fname = row["fname"].ToString();
			e.lname = row["lname"].ToString();
			e.dob = DateTime.Parse(row["dob"].ToString());
			e.dept = row["dept"].ToString();
			e.salary = float.Parse(row["salary"].ToString());
			return e;
		}


	}
}
