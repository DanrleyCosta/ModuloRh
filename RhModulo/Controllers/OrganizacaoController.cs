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
    public class OrganizacaoController : Controller
    {
        private readonly IConfiguration _configuration;
        public OrganizacaoController(IConfiguration configuration)
        {

            this._configuration = configuration;
        }

        // GET: Organizacaos
        public IActionResult Index()
        {
            DataTable dt = new DataTable();
            using (SqlConnection sqlconnection = new SqlConnection(_configuration.GetConnectionString("Conexao")))
            {
                sqlconnection.Open();
                SqlCommand sqlCmd = new SqlCommand("OrganizacaoCreateEdit", sqlconnection);
                SqlDataAdapter sqlDa = new SqlDataAdapter("OrganizacaoAll", sqlconnection);
                sqlDa.SelectCommand.CommandType = CommandType.StoredProcedure;
                sqlDa.Fill(dt); // lembrar de chamar no front
            }
            return View();
        }


        // GET: Organizacaos/CreateEdit/5
        public IActionResult CreateEdit(int? id)
        {
            Organizacao Organizacao = new Organizacao();
            if (id > 0)
            {
                Organizacao = fetchOrganizacao(id);
            }
            return View(Organizacao);
        }

        // POST: Organizacaos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateEdit(int id, [Bind("IdOrganizacao,Cnpj,RazaoSocial,RazaoSocialFantasia,NumEndereco,Cep,Uf")] Organizacao Organizacao)
        {
            //if (id != Organizacao.IdOrganizacao)
            //{
            //    return NotFound();
            //}

            if (ModelState.IsValid)
            {
                using (SqlConnection sqlconnection = new SqlConnection(_configuration.GetConnectionString("Conexao")))
                {
                    sqlconnection.Open();
                    SqlCommand sqlCmd = new SqlCommand("OrganizacaoCreateEdit", sqlconnection);
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Parameters.AddWithValue("Cnpj", Organizacao.Cnpj);
                    sqlCmd.Parameters.AddWithValue("RazaoSocial", Organizacao.RazaoSocial);
                    sqlCmd.Parameters.AddWithValue("NomeFantasia", Organizacao.NomeFantasia);
                    sqlCmd.Parameters.AddWithValue("NumEndereco", Organizacao.NumEndereco);
                    sqlCmd.Parameters.AddWithValue("Cep", Organizacao.Cep);
                    sqlCmd.Parameters.AddWithValue("Uf", Organizacao.Uf);
                    sqlCmd.ExecuteNonQuery();
                }
                return RedirectToAction(nameof(Index));
            }
            return View(Organizacao);
        }

        // GET: Organizacaos/Delete/5
        public IActionResult Delete(int? id)
        {
            Organizacao Organizacao = fetchOrganizacao(id);
            return View(Organizacao);
        }

        // POST: Organizacaos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            using (SqlConnection sqlconnection = new SqlConnection(_configuration.GetConnectionString("Conexao")))
            {
                sqlconnection.Open();
                SqlCommand sqlCmd = new SqlCommand("OrganizacaoDelete", sqlconnection);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.AddWithValue("IdOrganizacao", id);
                sqlCmd.ExecuteNonQuery();
            }
            return RedirectToAction(nameof(Index));
        }
        [NonAction]
        public Organizacao fetchOrganizacao(int? id)
        {
            Organizacao Organizacao = new Organizacao();
            using (SqlConnection sqlconnection = new SqlConnection(_configuration.GetConnectionString("Conexao")))
            {
                DataTable dt = new DataTable();
                sqlconnection.Open();
                SqlDataAdapter sqlDa = new SqlDataAdapter("OrganizacaoIdOrganizacao", sqlconnection);
                sqlDa.SelectCommand.CommandType = CommandType.StoredProcedure;
                sqlDa.SelectCommand.Parameters.AddWithValue("IdOrganizacao", id);
                sqlDa.Fill(dt); // lembrar de chamar no front

                if (dt.Rows.Count == 1)
                {
                    Organizacao.IdOrganizacao = Convert.ToInt32(dt.Rows[0]["IdOrganizacao"].ToString());
                    Organizacao.Cnpj = dt.Rows[0]["Cnpj"].ToString();
                    Organizacao.RazaoSocial = dt.Rows[0]["RazaoSocial"].ToString();
                    Organizacao.NomeFantasia = dt.Rows[0]["NomeFantasia"].ToString();
                    Organizacao.Cep = dt.Rows[0]["Cep"].ToString();
                    Organizacao.Uf = dt.Rows[0]["Uf"].ToString();
                }
                return Organizacao;
            }
        }
    }
}

