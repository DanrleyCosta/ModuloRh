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
    public class FuncionarioPjsController : Controller
    {
        private readonly IConfiguration _configuration;
        public FuncionarioPjsController(IConfiguration configuration)
        {

            this._configuration = configuration;
        }

        // GET: FuncionarioPjs
        public IActionResult Index()
        {
            DataTable dt = new DataTable();
            using (SqlConnection sqlconnection = new SqlConnection(_configuration.GetConnectionString("Conexao")))
            {
                sqlconnection.Open();
                SqlCommand sqlCmd = new SqlCommand("FuncionarioPjCreateEdit", sqlconnection);
                SqlDataAdapter sqlDa = new SqlDataAdapter("FuncionarioPjAll", sqlconnection);
                sqlDa.SelectCommand.CommandType = CommandType.StoredProcedure;
                sqlDa.Fill(dt); // lembrar de chamar no front
            }
            return View();
        }


        // GET: FuncionarioPjs/CreateEdit/5
        public IActionResult CreateEdit(int? id)
        {
            FuncionarioPj FuncionarioPj = new FuncionarioPj();
            if (id > 0)
            {
                FuncionarioPj = fetchFuncionarioPj(id);
            }
            return View(FuncionarioPj);
        }

        // POST: FuncionarioPjs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateEdit(int id, [Bind("Matricula,Cnpj,Nome,DataAbertura,IdCargo,IdSetor")] FuncionarioPj FuncionarioPj)
        {
            //if (id != FuncionarioPj.Matricula)
            //{
            //    return NotFound();
            //}

            if (ModelState.IsValid)
            {
                using (SqlConnection sqlconnection = new SqlConnection(_configuration.GetConnectionString("Conexao")))
                {
                    sqlconnection.Open();
                    SqlCommand sqlCmd = new SqlCommand("FuncionarioPjCreateEdit", sqlconnection);
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Parameters.AddWithValue("Cnpj", FuncionarioPj.Cnpj);
                    sqlCmd.Parameters.AddWithValue("Nome", FuncionarioPj.Nome);
                    sqlCmd.Parameters.AddWithValue("DataAbertura", FuncionarioPj.DataAbertura);
                    sqlCmd.Parameters.AddWithValue("IdCargo", FuncionarioPj.IdCargo);
                    sqlCmd.Parameters.AddWithValue("IdSetor", FuncionarioPj.IdSetor);
                    sqlCmd.ExecuteNonQuery();
                }
                return RedirectToAction(nameof(Index));
            }
            return View(FuncionarioPj);
        }

        // GET: FuncionarioPjs/Delete/5
        public IActionResult Delete(int? id)
        {
            FuncionarioPj FuncionarioPj = fetchFuncionarioPj(id);
            return View(FuncionarioPj);
        }

        // POST: FuncionarioPjs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            using (SqlConnection sqlconnection = new SqlConnection(_configuration.GetConnectionString("Conexao")))
            {
                sqlconnection.Open();
                SqlCommand sqlCmd = new SqlCommand("FuncionarioPjDelete", sqlconnection);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.AddWithValue("Matricula", id);
                sqlCmd.ExecuteNonQuery();
            }
            return RedirectToAction(nameof(Index));
        }
        [NonAction]
        public FuncionarioPj fetchFuncionarioPj(int? id)
        {
            FuncionarioPj FuncionarioPj = new FuncionarioPj();
            using (SqlConnection sqlconnection = new SqlConnection(_configuration.GetConnectionString("Conexao")))
            {
                DataTable dt = new DataTable();
                sqlconnection.Open();
                SqlDataAdapter sqlDa = new SqlDataAdapter("FuncionarioPjMatricula", sqlconnection);
                sqlDa.SelectCommand.CommandType = CommandType.StoredProcedure;
                sqlDa.SelectCommand.Parameters.AddWithValue("Matricula", id);
                sqlDa.Fill(dt); // lembrar de chamar no front

                if (dt.Rows.Count == 1)
                {
                    FuncionarioPj.Matricula = Convert.ToInt32(dt.Rows[0]["Matricula"].ToString());
                    FuncionarioPj.Cnpj = dt.Rows[0]["Cnpj"].ToString();
                    FuncionarioPj.Nome = dt.Rows[0]["Nome"].ToString();
                    FuncionarioPj.DataAbertura = Convert.ToDateTime(dt.Rows[0]["DataAbertura"].ToString());
                    FuncionarioPj.IdCargo = Convert.ToInt32(dt.Rows[0]["IdCargo"].ToString());
                    FuncionarioPj.IdSetor = Convert.ToInt32(dt.Rows[0]["IdSetor"].ToString());
                }
                return FuncionarioPj;
            }
        }
    }
}
