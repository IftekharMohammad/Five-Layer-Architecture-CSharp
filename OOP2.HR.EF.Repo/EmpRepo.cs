using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OOP2.HR.Framework;
using OOP2.HR.EF;


namespace OOP2.HR.EF.Repo
{
	public class EMPRepo
	{
        HrDbContext context = new HrDbContext();

		public Result<List<EMP>> GetAll(string key = "")
		{
			var result = new Result<List<EMP>>() { Data = new List<EMP>() };
			try
			{
			    IQueryable<EMP> query = context.EMPs;

			    if (ValidationHelper.IsStringValid(key))
			    {
			        query = query.Where(q => q.fname.Contains(key) || q.lname.Contains(key));
			    }

			    result.Data = query.ToList();
			}
			catch (Exception exc)
			{
				result.HasError = true;
				result.Message = exc.Message;
			}
			return result;
		}

		public Result<EMP> Save(EMP emp)
		{
			var result = new Result<EMP>();
            
			try
			{
			    var objToSave = context.EMPs.FirstOrDefault(e => e.id == emp.id);
			    
                //insert
                if (objToSave == null)
			    {
			        objToSave = new EMP();
			        context.EMPs.Add(objToSave);
			    }

			    objToSave.fname = emp.fname;
			    objToSave.lname = emp.lname;
			    objToSave.dob = emp.dob;

			    context.SaveChanges();
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

			string query = "select * from EMP where id = '" + id + "'";
			var dt = DataAccess.GetDataTable(query);

			if (dt == null || dt.Rows.Count == 0)
			{
				result.HasError = true;
				result.Message = "Invalid ID";
				return result;
			}

			query = "delete from EMP where id = '" + id + "'";


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

		private bool IsValidToSave(EMP EMP, Result<EMP> result)
		{
			if (!ValidationHelper.IsStringValid(EMP.fname))
			{
				result.HasError = true;
				result.Message = "Invalid Name";
				return false;
			}

			//more validation

			return true;
		}

		private EMP ConverToEntity(DataRow row)
		{
			if (row == null)
			{
				return null;
			}
			EMP e = new EMP();
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
