using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using RhModulo.Data;
using RhModulo.Models;

namespace RhModulo.Controllers
{
    public class FuncionarioPfsController : Controller
    {
        private readonly IConfiguration _configuration;
        public FuncionarioPfsController(IConfiguration configuration)
        {

            this._configuration = configuration;
        }

        // GET: FuncionarioPfs
        public IActionResult Index()
        {
            DataTable dt = new DataTable();
            using (SqlConnection sqlconnection = new SqlConnection(_configuration.GetConnectionString("Conexao")))
            {
                sqlconnection.Open();
                SqlCommand sqlCmd = new SqlCommand("FuncionarioPfCreateEdit", sqlconnection);
                SqlDataAdapter sqlDa = new SqlDataAdapter("FuncionarioPfAll", sqlconnection);
                sqlDa.SelectCommand.CommandType = CommandType.StoredProcedure;
                sqlDa.Fill(dt); // lembrar de chamar no front
            }
            return View();
        }


        // GET: FuncionarioPfs/CreateEdit/5
        public IActionResult CreateEdit(int? id)
        {
            FuncionarioPf funcionarioPf = new FuncionarioPf();
            if(id > 0)
            {
                funcionarioPf = fetchFuncionarioPf(id);
            }
            return View(funcionarioPf);
        }

        // POST: FuncionarioPfs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateEdit(int id, [Bind("Matricula,Cpf,Nome,DataNascimento,IdCargo,IdSetor")] FuncionarioPf funcionarioPf)
        {
            //if (id != funcionarioPf.Matricula)
            //{
            //    return NotFound();
            //}

            if (ModelState.IsValid)
            {
                using (SqlConnection sqlconnection = new SqlConnection(_configuration.GetConnectionString("Conexao")))
                {
                    sqlconnection.Open();
                    SqlCommand sqlCmd = new SqlCommand("FuncionarioPfCreateEdit", sqlconnection);
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Parameters.AddWithValue("Cpf", funcionarioPf.Cpf);
                    sqlCmd.Parameters.AddWithValue("Nome", funcionarioPf.Nome);
                    sqlCmd.Parameters.AddWithValue("DataNascimento", funcionarioPf.DataNascimento);
                    sqlCmd.Parameters.AddWithValue("IdCargo", funcionarioPf.IdCargo);
                    sqlCmd.Parameters.AddWithValue("IdSetor", funcionarioPf.IdSetor);
                    sqlCmd.ExecuteNonQuery();
                }
                return RedirectToAction(nameof(Index));
            }
            return View(funcionarioPf);
        }

        // GET: FuncionarioPfs/Delete/5
        public IActionResult Delete(int? id)
        {
            FuncionarioPf funcionarioPf = fetchFuncionarioPf(id);
            return View(funcionarioPf);
        }

        // POST: FuncionarioPfs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            using (SqlConnection sqlconnection = new SqlConnection(_configuration.GetConnectionString("Conexao")))
            {
                sqlconnection.Open();
                SqlCommand sqlCmd = new SqlCommand("FuncionarioPfDelete", sqlconnection);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.AddWithValue("Matricula", id);
                sqlCmd.ExecuteNonQuery();
            }
            return RedirectToAction(nameof(Index));
        }
        [NonAction]
        public FuncionarioPf fetchFuncionarioPf(int? id)
        {
            FuncionarioPf funcionarioPf = new FuncionarioPf();
            using (SqlConnection sqlconnection = new SqlConnection(_configuration.GetConnectionString("Conexao")))
            {
                DataTable dt = new DataTable();
                sqlconnection.Open();
                SqlDataAdapter sqlDa = new SqlDataAdapter("FuncionarioPfMatricula", sqlconnection);
                sqlDa.SelectCommand.CommandType = CommandType.StoredProcedure;
                sqlDa.SelectCommand.Parameters.AddWithValue("Matricula", id);
                sqlDa.Fill(dt); // lembrar de chamar no front

                if (dt.Rows.Count == 1)
                {
                    funcionarioPf.Matricula =Convert.ToInt32(dt.Rows[0]["Matricula"].ToString());
                    funcionarioPf.Cpf = dt.Rows[0]["Cpf"].ToString();
                    funcionarioPf.Nome = dt.Rows[0]["Nome"].ToString();
                    funcionarioPf.IdCargo = Convert.ToInt32(dt.Rows[0]["IdCargo"].ToString());
                    funcionarioPf.IdSetor = Convert.ToInt32(dt.Rows[0]["IdSetor"].ToString());
                }
                return funcionarioPf;
            }
        }
    }
}
