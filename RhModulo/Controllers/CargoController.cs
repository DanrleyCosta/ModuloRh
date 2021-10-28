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
    public class CargoController : Controller
    {
        private readonly IConfiguration _configuration;
        public CargoController(IConfiguration configuration)
        {

            this._configuration = configuration;
        }

        // GET: Cargos
        public IActionResult Index()
        {
            DataTable dt = new DataTable();
            using (SqlConnection sqlconnection = new SqlConnection(_configuration.GetConnectionString("Conexao")))
            {
                sqlconnection.Open();
                SqlCommand sqlCmd = new SqlCommand("CargoCreateEdit", sqlconnection);
                SqlDataAdapter sqlDa = new SqlDataAdapter("CargoAll", sqlconnection);
                sqlDa.SelectCommand.CommandType = CommandType.StoredProcedure;
                sqlDa.Fill(dt); // lembrar de chamar no front
            }
            return View();
        }


        // GET: Cargos/CreateEdit/5
        public IActionResult CreateEdit(int? id)
        {
            Cargo Cargo = new Cargo();
            if (id > 0)
            {
                Cargo = fetchCargo(id);
            }
            return View(Cargo);
        }

        // POST: Cargos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateEdit(int id, [Bind("IdCargo,NomeCargo,LimiteHoraMes,ValorHora,Descricao")] Cargo Cargo)
        {
            //if (id != Cargo.IdCargo)
            //{
            //    return NotFound();
            //}

            if (ModelState.IsValid)
            {
                using (SqlConnection sqlconnection = new SqlConnection(_configuration.GetConnectionString("Conexao")))
                {
                    sqlconnection.Open();
                    SqlCommand sqlCmd = new SqlCommand("CargoCreateEdit", sqlconnection);
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Parameters.AddWithValue("IdCargo", Cargo.IdCargo);
                    sqlCmd.Parameters.AddWithValue("NomeCargo", Cargo.NomeCargo);
                    sqlCmd.Parameters.AddWithValue("LimiteHoraMes", Cargo.LimiteHoraMes);
                    sqlCmd.Parameters.AddWithValue("ValorHora", Cargo.ValorHora);
                    sqlCmd.Parameters.AddWithValue("Descricao", Cargo.Descricao);
                    sqlCmd.ExecuteNonQuery();
                }
                return RedirectToAction(nameof(Index));
            }
            return View(Cargo);
        }

        // GET: Cargos/Delete/5
        public IActionResult Delete(int? id)
        {
            Cargo Cargo = fetchCargo(id);
            return View(Cargo);
        }

        // POST: Cargos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            using (SqlConnection sqlconnection = new SqlConnection(_configuration.GetConnectionString("Conexao")))
            {
                sqlconnection.Open();
                SqlCommand sqlCmd = new SqlCommand("CargoDelete", sqlconnection);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.AddWithValue("IdCargo", id);
                sqlCmd.ExecuteNonQuery();
            }
            return RedirectToAction(nameof(Index));
        }
        [NonAction]
        public Cargo fetchCargo(int? id)
        {
            Cargo Cargo = new Cargo();
            using (SqlConnection sqlconnection = new SqlConnection(_configuration.GetConnectionString("Conexao")))
            {
                DataTable dt = new DataTable();
                sqlconnection.Open();
                SqlDataAdapter sqlDa = new SqlDataAdapter("CargoIdCargo", sqlconnection);
                sqlDa.SelectCommand.CommandType = CommandType.StoredProcedure;
                sqlDa.SelectCommand.Parameters.AddWithValue("IdCargo", id);
                sqlDa.Fill(dt); // lembrar de chamar no front

                if (dt.Rows.Count == 1)
                {
                    Cargo.IdCargo = Convert.ToInt32(dt.Rows[0]["IdCargo"].ToString());
                    Cargo.NomeCargo = dt.Rows[0]["NomeCargo"].ToString();
                    Cargo.LimiteHoraMes = Convert.ToInt32(dt.Rows[0]["LimiteHoraMes"].ToString());
                    Cargo.ValorHora = Convert.ToInt32(dt.Rows[0]["ValorHora"].ToString());
                    Cargo.Descricao = dt.Rows[0]["IdSetor"].ToString();
                }
                return Cargo;
            }
        }
    }
}

