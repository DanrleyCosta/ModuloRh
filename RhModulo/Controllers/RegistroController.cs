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
    public class RegistroController : Controller
    {
        private readonly IConfiguration _configuration;
        public RegistroController(IConfiguration configuration)
        {

            this._configuration = configuration;
        }

        // GET: Registros
        public IActionResult Index()
        {
            DataTable dt = new DataTable();
            using (SqlConnection sqlconnection = new SqlConnection(_configuration.GetConnectionString("Conexao")))
            {
                sqlconnection.Open();
                SqlCommand sqlCmd = new SqlCommand("RegistroCreateEdit", sqlconnection);
                SqlDataAdapter sqlDa = new SqlDataAdapter("RegistroAll", sqlconnection);
                sqlDa.SelectCommand.CommandType = CommandType.StoredProcedure;
                sqlDa.Fill(dt); // lembrar de chamar no front
            }
            return View();
        }


        // GET: Registros/CreateEdit/5
        public IActionResult CreateEdit(int? id)
        {
            Registro Registro = new Registro();
            if (id > 0)
            {
                Registro = fetchRegistro(id);
            }
            return View(Registro);
        }

        // POST: Registros/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateEdit(int id, [Bind("IdRegistro,NomeRegistro,LimiteHoraMes,ValorHora,Descricao")] Registro Registro)
        {
            //if (id != Registro.IdRegistro)
            //{
            //    return NotFound();
            //}

            if (ModelState.IsValid)
            {
                using (SqlConnection sqlconnection = new SqlConnection(_configuration.GetConnectionString("Conexao")))
                {
                    sqlconnection.Open();
                    SqlCommand sqlCmd = new SqlCommand("RegistroCreateEdit", sqlconnection);
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Parameters.AddWithValue("IdRegistro", Registro.IdRegistro);
                    sqlCmd.Parameters.AddWithValue("DataHoraEntrada", Registro.DataHoraEntrada);
                    sqlCmd.Parameters.AddWithValue("DataHoraEntradaAlmoco", Registro.DataHoraEntradaAlmoco);
                    sqlCmd.Parameters.AddWithValue("DataHoraSaidaAlmoco", Registro.DataHoraSaidaAlmoco);
                    sqlCmd.Parameters.AddWithValue("DataHoraSaida", Registro.DataHoraSaida);
                    sqlCmd.Parameters.AddWithValue("MatriculaPf", Registro.MatriculaPf);
                    sqlCmd.Parameters.AddWithValue("MatriculaPj", Registro.MatriculaPj);
                    sqlCmd.Parameters.AddWithValue("Observacao", Registro.Observacao);
                    sqlCmd.ExecuteNonQuery();
                }
                return RedirectToAction(nameof(Index));
            }
            return View(Registro);
        }

        // GET: Registros/Delete/5
        //public IActionResult Delete(int? id)
        //{
        //    Registro Registro = fetchRegistro(id);
        //    return View(Registro);
        //}

        // POST: Registros/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public IActionResult DeleteConfirmed(int id)
        //{
        //    using (SqlConnection sqlconnection = new SqlConnection(_configuration.GetConnectionString("Conexao")))
        //    {
        //        sqlconnection.Open();
        //        SqlCommand sqlCmd = new SqlCommand("RegistroDelete", sqlconnection);
        //        sqlCmd.CommandType = CommandType.StoredProcedure;
        //        sqlCmd.Parameters.AddWithValue("IdRegistro", id);
        //        sqlCmd.ExecuteNonQuery();
        //    }
        //    return RedirectToAction(nameof(Index));
        //}
        [NonAction]
        public Registro fetchRegistro(int? id)
        {
            Registro Registro = new Registro();
            using (SqlConnection sqlconnection = new SqlConnection(_configuration.GetConnectionString("Conexao")))
            {
                DataTable dt = new DataTable();
                sqlconnection.Open();
                SqlDataAdapter sqlDa = new SqlDataAdapter("RegistroIdRegistro", sqlconnection);
                sqlDa.SelectCommand.CommandType = CommandType.StoredProcedure;
                sqlDa.SelectCommand.Parameters.AddWithValue("IdRegistro", id);
                sqlDa.Fill(dt); // lembrar de chamar no front

                if (dt.Rows.Count == 1)
                {
                    Registro.IdRegistro = Convert.ToInt32(dt.Rows[0]["IdRegistro"].ToString());
                    Registro.DataHoraEntrada = Convert.ToDateTime(dt.Rows[0]["DataHoraEntrada"].ToString());
                    Registro.DataHoraEntradaAlmoco = Convert.ToDateTime(dt.Rows[0]["DataHoraEntradaAlmoco"].ToString());
                    Registro.DataHoraSaidaAlmoco = Convert.ToDateTime(dt.Rows[0]["DataHoraSaidaAlmoco"].ToString());
                    Registro.DataHoraSaida = Convert.ToDateTime(dt.Rows[0]["DataHoraSaida"].ToString());
                    Registro.MatriculaPf = Convert.ToInt32(dt.Rows[0]["MatriculaPf"].ToString());
                    Registro.MatriculaPj = Convert.ToInt32(dt.Rows[0]["MatriculaPj"].ToString());
                    Registro.Observacao = dt.Rows[0]["Observacao"].ToString();

                }
                return Registro;
            }
        }
    }
}

