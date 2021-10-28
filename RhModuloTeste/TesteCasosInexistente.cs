using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using RhModulo.Models;
using System.Linq;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace RhModuloTeste
{
    public class TesteCasosInexistente
    {
        public string conecxao = "Data Source=LAPTOP-HKBV533R\\SQLEXPRESS02;Initial Catalog =MODULORH; Integrated Security=True;";

        [Theory]
        [InlineData(1)]
        public void CargoInexistente(int id)
        {
            Cargo Cargo = new Cargo();
            using (SqlConnection sqlconnection = new SqlConnection(conecxao))
            {
                DataTable dt = new DataTable();
                sqlconnection.Open();
                SqlDataAdapter sqlDa = new SqlDataAdapter("CargoId", sqlconnection);
                sqlDa.SelectCommand.CommandType = CommandType.StoredProcedure;
                sqlDa.SelectCommand.Parameters.AddWithValue("IdCargo", id);
                sqlDa.Fill(dt); // lembrar de chamar no front

                Assert.Equal(id, dt.Rows[0]["IdCargo"]);
            }
        }

        [Theory]
        [InlineData(4)]
        public void SetorInexistente(int id)
        {
            Cargo Cargo = new Cargo();
            using (SqlConnection sqlconnection = new SqlConnection(conecxao))
            {
                DataTable dt = new DataTable();
                sqlconnection.Open();
                SqlDataAdapter sqlDa = new SqlDataAdapter("SetorId", sqlconnection);
                sqlDa.SelectCommand.CommandType = CommandType.StoredProcedure;
                sqlDa.SelectCommand.Parameters.AddWithValue("IdSetor", id);
                sqlDa.Fill(dt); // lembrar de chamar no front

                Assert.Equal(id, dt.Rows[0]["IdSetor"]);
            }
        }

        [Theory]
        [InlineData(4)]
        public void OrganizacaoInexistente(int id)
        {
            Cargo Cargo = new Cargo();
            using (SqlConnection sqlconnection = new SqlConnection(conecxao))
            {
                DataTable dt = new DataTable();
                sqlconnection.Open();
                SqlDataAdapter sqlDa = new SqlDataAdapter("OrganizacaoId", sqlconnection);
                sqlDa.SelectCommand.CommandType = CommandType.StoredProcedure;
                sqlDa.SelectCommand.Parameters.AddWithValue("IdOrganizacao", id);
                sqlDa.Fill(dt); // lembrar de chamar no front

                Assert.Equal(id, dt.Rows[0]["IdOrganizacao"]);
            }
        }

        [Theory]
        [InlineData(4)]
        public void RegistroInexistente(int id)
        {
            Cargo Cargo = new Cargo();
            using (SqlConnection sqlconnection = new SqlConnection(conecxao))
            {
                DataTable dt = new DataTable();
                sqlconnection.Open();
                SqlDataAdapter sqlDa = new SqlDataAdapter("RegistroId", sqlconnection);
                sqlDa.SelectCommand.CommandType = CommandType.StoredProcedure;
                sqlDa.SelectCommand.Parameters.AddWithValue("IdRegistro", id);
                sqlDa.Fill(dt); // lembrar de chamar no front

                Assert.Equal(id, dt.Rows[0]["IdRegistro"]);
            }
        }

        [Theory]
        [InlineData(4)]
        public void ContratoInexistente(int id)
        {
            Cargo Cargo = new Cargo();
            using (SqlConnection sqlconnection = new SqlConnection(conecxao))
            {
                DataTable dt = new DataTable();
                sqlconnection.Open();
                SqlDataAdapter sqlDa = new SqlDataAdapter("ContratoId", sqlconnection);
                sqlDa.SelectCommand.CommandType = CommandType.StoredProcedure;
                sqlDa.SelectCommand.Parameters.AddWithValue("IdContrato", id);
                sqlDa.Fill(dt); // lembrar de chamar no front

                Assert.Equal(id, dt.Rows[0]["IdContrato"]);
            }
        }

        [Theory]
        [InlineData(4)]
        public void FuncionarioPfInexistente(int id)
        {
            Cargo Cargo = new Cargo();
            using (SqlConnection sqlconnection = new SqlConnection(conecxao))
            {
                DataTable dt = new DataTable();
                sqlconnection.Open();
                SqlDataAdapter sqlDa = new SqlDataAdapter("FuncionarioPfMatricula", sqlconnection);
                sqlDa.SelectCommand.CommandType = CommandType.StoredProcedure;
                sqlDa.SelectCommand.Parameters.AddWithValue("Matricula", id);
                sqlDa.Fill(dt); // lembrar de chamar no front

                Assert.Equal(id, dt.Rows[0]["Matricula"]);
            }
        }

        [Theory]
        [InlineData(4)]
        public void FuncionarioPjInexistente(int id)
        {
            Cargo Cargo = new Cargo();
            using (SqlConnection sqlconnection = new SqlConnection(conecxao))
            {
                DataTable dt = new DataTable();
                sqlconnection.Open();
                SqlDataAdapter sqlDa = new SqlDataAdapter("FuncionarioPjMatricula", sqlconnection);
                sqlDa.SelectCommand.CommandType = CommandType.StoredProcedure;
                sqlDa.SelectCommand.Parameters.AddWithValue("Matricula", id);
                sqlDa.Fill(dt); // lembrar de chamar no front

                Assert.Equal(id, dt.Rows[0]["Matricula"]);
            }
        }
    }
}
