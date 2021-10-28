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
    public class SetorController : Controller
    {
        private readonly IConfiguration _configuration;
        public SetorController(IConfiguration configuration)
        {

            this._configuration = configuration;
        }

        // GET: Setors
        public IActionResult Index()
        {
            DataTable dt = new DataTable();
            using (SqlConnection sqlconnection = new SqlConnection(_configuration.GetConnectionString("Conexao")))
            {
                sqlconnection.Open();
                SqlCommand sqlCmd = new SqlCommand("SetorCreateEdit", sqlconnection);
                SqlDataAdapter sqlDa = new SqlDataAdapter("SetorAll", sqlconnection);
                sqlDa.SelectCommand.CommandType = CommandType.StoredProcedure;
                sqlDa.Fill(dt); // lembrar de chamar no front
            }
            return View();
        }


        // GET: Setors/CreateEdit/5
        public IActionResult CreateEdit(int? id)
        {
            Setor Setor = new Setor();
            if (id > 0)
            {
                Setor = fetchSetor(id);
            }
            return View(Setor);
        }

        // POST: Setors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateEdit(int id, [Bind("IdSetor,Nome,DescSetor,IdOrganizacao")] Setor Setor)
        {
            //if (id != Setor.IdSetor)
            //{
            //    return NotFound();
            //}

            if (ModelState.IsValid)
            {
                using (SqlConnection sqlconnection = new SqlConnection(_configuration.GetConnectionString("Conexao")))
                {
                    sqlconnection.Open();
                    SqlCommand sqlCmd = new SqlCommand("SetorCreateEdit", sqlconnection);
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Parameters.AddWithValue("IdSetor", Setor.IdSetor);
                    sqlCmd.Parameters.AddWithValue("Nome", Setor.Nome);
                    sqlCmd.Parameters.AddWithValue("DescSetor", Setor.DescSetor);
                    sqlCmd.Parameters.AddWithValue("IdOrganizacao", Setor.IdOrganizacao);
                    sqlCmd.ExecuteNonQuery();
                }
                return RedirectToAction(nameof(Index));
            }
            return View(Setor);
        }

        // GET: Setors/Delete/5
        public IActionResult Delete(int? id)
        {
            Setor Setor = fetchSetor(id);
            return View(Setor);
        }

        // POST: Setors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            using (SqlConnection sqlconnection = new SqlConnection(_configuration.GetConnectionString("Conexao")))
            {
                sqlconnection.Open();
                SqlCommand sqlCmd = new SqlCommand("SetorDelete", sqlconnection);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.AddWithValue("IdSetor", id);
                sqlCmd.ExecuteNonQuery();
            }
            return RedirectToAction(nameof(Index));
        }
        [NonAction]
        public Setor fetchSetor(int? id)
        {
            Setor Setor = new Setor();
            using (SqlConnection sqlconnection = new SqlConnection(_configuration.GetConnectionString("Conexao")))
            {
                DataTable dt = new DataTable();
                sqlconnection.Open();
                SqlDataAdapter sqlDa = new SqlDataAdapter("SetorIdSetor", sqlconnection);
                sqlDa.SelectCommand.CommandType = CommandType.StoredProcedure;
                sqlDa.SelectCommand.Parameters.AddWithValue("IdSetor", id);
                sqlDa.Fill(dt); // lembrar de chamar no front

                if (dt.Rows.Count == 1)
                {
                    Setor.IdSetor = Convert.ToInt32(dt.Rows[0]["IdSetor"].ToString());
                    Setor.Nome = dt.Rows[0]["Nome"].ToString();
                    Setor.DescSetor = dt.Rows[0]["LimiteHoraMes"].ToString();
                    Setor.IdOrganizacao = Convert.ToInt32(dt.Rows[0]["ValorHora"].ToString());
                }
                return Setor;
            }
        }
    }
}


