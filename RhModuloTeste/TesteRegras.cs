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
    public class TesteRegras
    {
        public string conecxao = "Data Source=LAPTOP-HKBV533R\\SQLEXPRESS02;Initial Catalog =MODULORH; Integrated Security=True;";

        [Theory]
        [InlineData(1,220)]
        public void CargoRegraLimiteHora(int id, int LimiteHora)
        {
            Cargo Cargo = new Cargo();
            using (SqlConnection sqlconnection = new SqlConnection(conecxao))
            {
                DataTable dt = new DataTable();
                sqlconnection.Open();
                SqlDataAdapter sqlDa = new SqlDataAdapter("CargoId", sqlconnection);
                sqlDa.SelectCommand.CommandType = CommandType.StoredProcedure;
                sqlDa.SelectCommand.Parameters.AddWithValue("IdCargo", id);
                sqlDa.Fill(dt); 

                Assert.Equal(LimiteHora, dt.Rows[0]["LimiteHoraMes"]);
            }
        }

        [Theory]
        [InlineData(1,"Ti")]
        public void SetorQualSetor(int id, string setor)
        {
            Cargo Cargo = new Cargo();
            using (SqlConnection sqlconnection = new SqlConnection(conecxao))
            {
                DataTable dt = new DataTable();
                sqlconnection.Open();
                SqlDataAdapter sqlDa = new SqlDataAdapter("SetorId", sqlconnection);
                sqlDa.SelectCommand.CommandType = CommandType.StoredProcedure;
                sqlDa.SelectCommand.Parameters.AddWithValue("IdSetor", id);
                sqlDa.Fill(dt); 

                Assert.Equal(setor, dt.Rows[0]["Nome"]);
            }
        }
        // verificação na quantidade de caracteres do cnpj
        [Theory]
        [InlineData(1,"000000000000")]
        public void OrganizacaoCnpjCorreto(int id, string Cnpj)
        {
            Cargo Cargo = new Cargo();
            using (SqlConnection sqlconnection = new SqlConnection(conecxao))
            {
                DataTable dt = new DataTable();
                sqlconnection.Open();
                SqlDataAdapter sqlDa = new SqlDataAdapter("OrganizacaoId", sqlconnection);
                sqlDa.SelectCommand.CommandType = CommandType.StoredProcedure;
                sqlDa.SelectCommand.Parameters.AddWithValue("IdOrganizacao", id);
                sqlDa.Fill(dt); 


                Assert.Equal(Cnpj.Length, dt.Rows[0]["Cnpj"].ToString().Length);
            }
        }

        [Theory]
        [InlineData(1)]
        public void RegistroDemarcado(int id)
        {
            Cargo Cargo = new Cargo();
            using (SqlConnection sqlconnection = new SqlConnection(conecxao))
            {
                DataTable dt = new DataTable();
                sqlconnection.Open();
                SqlDataAdapter sqlDa = new SqlDataAdapter("RegistroId", sqlconnection);
                sqlDa.SelectCommand.CommandType = CommandType.StoredProcedure;
                sqlDa.SelectCommand.Parameters.AddWithValue("IdRegistro", id);
                sqlDa.Fill(dt); 

                Assert.Equal(id, dt.Rows[0]["IdRegistro"]);
            }
        }

        [Theory]
        [InlineData(1,220)]
        public void ContratoQuantHoraContratada(int id, int quant)
        {
            Cargo Cargo = new Cargo();
            using (SqlConnection sqlconnection = new SqlConnection(conecxao))
            {
                DataTable dt = new DataTable();
                sqlconnection.Open();
                SqlDataAdapter sqlDa = new SqlDataAdapter("ContratoId", sqlconnection);
                sqlDa.SelectCommand.CommandType = CommandType.StoredProcedure;
                sqlDa.SelectCommand.Parameters.AddWithValue("IdContrato", id);
                sqlDa.Fill(dt); 

                Assert.Equal(quant, dt.Rows[0]["QuantHoraContratada"]);
            }
        }

        [Theory]
        [InlineData(1)]
        public void FuncionarioPfValido(int id)
        {
            Cargo Cargo = new Cargo();
            using (SqlConnection sqlconnection = new SqlConnection(conecxao))
            {
                DataTable dt = new DataTable();
                sqlconnection.Open();
                SqlDataAdapter sqlDa = new SqlDataAdapter("FuncionarioPfMatricula", sqlconnection);
                sqlDa.SelectCommand.CommandType = CommandType.StoredProcedure;
                sqlDa.SelectCommand.Parameters.AddWithValue("Matricula", id);
                sqlDa.Fill(dt); 

                Assert.Equal(true, dt.Rows[0]["Cnpj"]);
            }
        }

        [Theory]
        [InlineData(1)]
        public void FuncionarioPjValido(int id)
        {
            Cargo Cargo = new Cargo();
            using (SqlConnection sqlconnection = new SqlConnection(conecxao))
            {
                DataTable dt = new DataTable();
                sqlconnection.Open();
                SqlDataAdapter sqlDa = new SqlDataAdapter("FuncionarioPjMatricula", sqlconnection);
                sqlDa.SelectCommand.CommandType = CommandType.StoredProcedure;
                sqlDa.SelectCommand.Parameters.AddWithValue("Matricula", id);
                sqlDa.Fill(dt); 

                Assert.Equal(true, dt.Rows[0]["Cpf"]);
            }
        }
    }
}
