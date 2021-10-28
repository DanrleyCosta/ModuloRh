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
    public class ContratoController : Controller
    {
        private readonly IConfiguration _configuration;
        public ContratoController(IConfiguration configuration)
        {

            this._configuration = configuration;
        }

        // GET: Contratos
        public IActionResult Index()
        {
            DataTable dt = new DataTable();
            using (SqlConnection sqlconnection = new SqlConnection(_configuration.GetConnectionString("Conexao")))
            {
                sqlconnection.Open();
                SqlCommand sqlCmd = new SqlCommand("ContratoCreateEdit", sqlconnection);
                SqlDataAdapter sqlDa = new SqlDataAdapter("ContratoAll", sqlconnection);
                sqlDa.SelectCommand.CommandType = CommandType.StoredProcedure;
                sqlDa.Fill(dt); // lembrar de chamar no front
            }
            return View();
        }


        // GET: Contratos/CreateEdit/5
        public IActionResult CreateEdit(int? id)
        {
            Contrato Contrato = new Contrato();
            if (id > 0)
            {
                Contrato = fetchContrato(id);
            }
            return View(Contrato);
        }

        // POST: Contratos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateEdit(int id, [Bind("IdContrato,DescricaoContrato,QuantHoraContratada,ValorHoraContratada,MatriculaPf,MatriculaPj")] Contrato Contrato)
        {
            //if (id != Contrato.IdContrato)
            //{
            //    return NotFound();
            //}

            if (ModelState.IsValid)
            {
                using (SqlConnection sqlconnection = new SqlConnection(_configuration.GetConnectionString("Conexao")))
                {
                    sqlconnection.Open();
                    SqlCommand sqlCmd = new SqlCommand("ContratoCreateEdit", sqlconnection);
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Parameters.AddWithValue("IdContrato", Contrato.IdContrato);
                    sqlCmd.Parameters.AddWithValue("DescricaoContrato", Contrato.DescricaoContrato);
                    sqlCmd.Parameters.AddWithValue("QuantHoraContratada", Contrato.QuantHoraContratada);
                    sqlCmd.Parameters.AddWithValue("ValorHoraContratada", Contrato.ValorHoraContratada);
                    sqlCmd.Parameters.AddWithValue("MatriculaPf", Contrato.MatriculaPf);
                    sqlCmd.Parameters.AddWithValue("MatriculaPj", Contrato.MatriculaPj);
                    sqlCmd.ExecuteNonQuery();
                }
                return RedirectToAction(nameof(Index));
            }
            return View(Contrato);
        }

        // GET: Contratos/Delete/5
        public IActionResult Delete(int? id)
        {
            Contrato Contrato = fetchContrato(id);
            return View(Contrato);
        }

        // POST: Contratos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            using (SqlConnection sqlconnection = new SqlConnection(_configuration.GetConnectionString("Conexao")))
            {
                sqlconnection.Open();
                SqlCommand sqlCmd = new SqlCommand("ContratoDelete", sqlconnection);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.AddWithValue("IdContrato", id);
                sqlCmd.ExecuteNonQuery();
            }
            return RedirectToAction(nameof(Index));
        }
        [NonAction]
        public Contrato fetchContrato(int? id)
        {
            Contrato Contrato = new Contrato();
            using (SqlConnection sqlconnection = new SqlConnection(_configuration.GetConnectionString("Conexao")))
            {
                DataTable dt = new DataTable();
                sqlconnection.Open();
                SqlDataAdapter sqlDa = new SqlDataAdapter("ContratoIdContrato", sqlconnection);
                sqlDa.SelectCommand.CommandType = CommandType.StoredProcedure;
                sqlDa.SelectCommand.Parameters.AddWithValue("IdContrato", id);
                sqlDa.Fill(dt); // lembrar de chamar no front

                if (dt.Rows.Count == 1)
                {
                    Contrato.IdContrato = Convert.ToInt32(dt.Rows[0]["IdContrato"].ToString());
                    Contrato.DescricaoContrato = dt.Rows[0]["DescricaoContrato"].ToString();
                    Contrato.QuantHoraContratada = Convert.ToInt32(dt.Rows[0]["QuantHoraContratada"].ToString());
                    Contrato.ValorHoraContratada = Convert.ToDecimal(dt.Rows[0]["ValorHoraContratada"].ToString());
                    Contrato.MatriculaPf = Convert.ToInt32(dt.Rows[0]["MatriculaPf"].ToString());
                    Contrato.MatriculaPj = Convert.ToInt32(dt.Rows[0]["MatriculaPj"].ToString());
                }
                return Contrato;
            }
        }
    }
}


